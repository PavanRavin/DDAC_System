using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DDAC_System.Models
{
    public class ViewAssignment
    {
        [Key]
        public int ViewAssignmentID { get; set; }
        public string ViewAssignmentName { get; set; }
        public string ViewAssignmentDesc { get; set; }
        public DateTime ViewAssignmentHandOut { get; set; }
        public DateTime ViewAssignmentDue { get; set; }
    }
}
