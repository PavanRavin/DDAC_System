using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DDAC_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon;
using System.Net;
using Amazon.S3.Transfer;

namespace DDAC_System.Controllers
{
    [Authorize(Roles="Teacher,Student")]
    public class AssignmentSubmissionController : Controller
    {
        private readonly System_ClassDBContext _context;

        const string bucketname = "ddacassignments";

        public AssignmentSubmissionController(System_ClassDBContext context)
        {
            _context = context;
        }
        private List<string> getAWSCredentials()
        {
            //1. Setup the appsettings.json file in the statement
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build(); //we build the link for debugging for appsettings json


            //2. Read key info fro JSON using config instance
            List<string> AWSKeys = new List<string>();
            AWSKeys.Add(config["AWSCredential:key1"]); //access key
            AWSKeys.Add(config["AWSCredential:key2"]); //session key
            AWSKeys.Add(config["AWSCredential:key3"]); //token key

            //3.Return the function when keys are required
            return AWSKeys;
        }
        // GET: AssignmentSubmission
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Index(int? id)
        {
            var submissions = _context.AssignmentSubmission
                .Where(a => a.AssignmentID == id)
                .OrderBy(a => a.SubmitTime);
            return View(await submissions.ToListAsync());
        }

        // GET: AssignmentSubmission/Create
        [Authorize(Roles = "Student")]
        public IActionResult Create(Assignment assignment)
        {
            ViewBag.Assignment = assignment;
            return View();
        }

        // POST: AssignmentSubmission/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile Document)
        {
            if (ModelState.IsValid)
            {
                //1. Get keys from app settings json
                List<string> AWSKeys = getAWSCredentials();

                //2. Setup Connection to S3 Bucket
                var s3clientobject = new AmazonS3Client(AWSKeys[0], AWSKeys[1], AWSKeys[2], RegionEndpoint.USEast1);

                //3. Send Document to S3 bucket
                //3.1 Input file validation
                if (Document.Length <= 0)
                {
                    return BadRequest(Document.FileName + "is empty! Document is not allowed to be uploaded to S3");
                }

                string filename = User.Identity.Name + "_" + ViewBag.assignment.AssignmentName;

                //3.2 Validation Passed
                try
                {
                    using (var newMemoryStream = new MemoryStream())
                    {
                        Document.CopyTo(newMemoryStream);

                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            InputStream = newMemoryStream,
                            Key = filename,
                            BucketName = bucketname + "/docs",
                            CannedACL = S3CannedACL.PublicRead
                        };

                        var fileTransferUtility = new TransferUtility(s3clientobject);
                        await fileTransferUtility.UploadAsync(uploadRequest);
                    }
                }
                catch (AmazonS3Exception ex)
                {
                    return BadRequest("Error in file of" + Document.FileName + ":" + ex.Message);
                }
                catch (Exception e2)
                {
                    return BadRequest("Error in file of" + Document.FileName + ":" + e2.Message);
                }
                AssignmentSubmission submission = new AssignmentSubmission();
                submission.SubmitName = filename;
                submission.AssignmentID = ViewBag.assignment.AssignmentID;
                submission.SubmitTime = DateTime.Now;
                _context.Add(submission);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Assignment");
            }
            return RedirectToAction("Index","Assignment");
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> DownloadSubmission(int? id)
        {
            var submission = _context.AssignmentSubmission
                .Where(a => a.SubmitID == id);

            List<string> AWSKeys = getAWSCredentials();

            //2. Setup Connection to S3 Bucket
            var s3clientobject = new AmazonS3Client(AWSKeys[0], AWSKeys[1], AWSKeys[2], RegionEndpoint.USEast1);

            string filename = submission.FirstOrDefault().SubmitName.ToString();

            try
            {
                TransferUtility transferUtility = new TransferUtility(s3clientobject);

                var response = await transferUtility.S3Client.GetObjectAsync(new GetObjectRequest()
                {
                    BucketName = bucketname+ "/docs",
                    Key = filename
                });

                if (response.ResponseStream == null)
                {
                    return NotFound();
                }
                return File(response.ResponseStream, response.Headers.ContentType, filename);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null && (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") || 
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new Exception("Please check the AWS Credentials.");
                }
                else
                {
                    throw new Exception(amazonS3Exception.Message);
                }
            }
        }

    }
}
