using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DDAC_System.Models
{
    public class AcademicClass
    {
        [Key]
        public int Class_ID { set; get; } //primary key of Class
        public string Class_Name { set; get; }
        public String Class_Lecturer { set; get; }
        public DateTime ClassStartTime { set; get; }
        public DateTime ClassEndTime { set; get; }
        public string Class_Location { set; get; }

    }
}
