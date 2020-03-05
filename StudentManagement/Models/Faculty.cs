using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class Faculty :BaseEntity
    {
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Invalid Name")]
        public string Name { get; set; }


    }
}
