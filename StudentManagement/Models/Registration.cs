using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class Registration :BaseEntity
    {
		[ForeignKey("PaymentId")]
		public Payment Payment { get; set; }
		public string PaymentId { get; set; }



		[ForeignKey("FacultyId")]
		public Faculty Faculty { get; set; }
		public string FacultyId { get; set; }

	

		[ForeignKey("StudentId")]
		public Student Student { get; set; }
		public string StudentId { get; set; }



		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime IssuedDate { get; set; } = System.DateTime.Now;
    }
}
