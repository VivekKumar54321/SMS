using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.ViewModel
{
    public class RegistrationViewModel : BaseEntity
    {

		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime IssuedDate { get; set; } = System.DateTime.Now;

		[ForeignKey("PaymentId")]
		public Payment Payment { get; set; }
		public string PaymentId { get; set; }
		public string Type { get; set; }
		public double Amount { get; set; }




		[ForeignKey("FacultyId")]
		public Faculty Faculty { get; set; }
		public string FacultyId { get; set; }



		[ForeignKey("StudentId")]
		public Student Student { get; set; }
		public string StudentId { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }

		public int  PhoneNo { get; set; }

	}

}
