using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class Payment : BaseEntity
    {
	
		public string Type { get; set; }

	
		public double Amount { get; set; }

	}
}
