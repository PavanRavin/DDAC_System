using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DDAC_System.Models
{
    public class Assignment
    {
        [Key]
        public int AssignmentID { get; set; }
        public string AssignmentName { get; set; }
        public string AssignmentDesc { get; set; }
        public DateTime AssignmentHandOut { get; set; }
        public DateTime AssignmentDue { get; set; }

        [ForeignKey("Class_Name")]
        public string Class_Name { get; set; }
        public AcademicClass AcademicClass { get; set; }

        public ICollection<AssignmentSubmission> AssignmentSubmission { get; } 
            = new List<AssignmentSubmission>();
    }
}
