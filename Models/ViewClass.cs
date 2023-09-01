using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DDAC_System.Models
{
    public class ViewClass
    {
        [Key]
        public int ViewClass_ID { set; get; } //primary key of Class
        public string ViewClass_Name { set; get; }
        public String ViewClass_Lecturer { set; get; }
        public DateTime ViewClassStartTime { set; get; }
        public DateTime ViewClassEndTime { set; get; }
        public string ViewClass_Location { set; get; }
    }
}
