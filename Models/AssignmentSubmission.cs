using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DDAC_System.Models
{
    public class AssignmentSubmission
    {
        [Key]
        public int SubmitID { get; set; }
        public string SubmitName { get; set; }
        public DateTime SubmitTime { get; set; }

        [ForeignKey("AssignmentID")]
        public int AssignmentID { get; set; }
        public Assignment Assignment { get; set; }
    }
}
