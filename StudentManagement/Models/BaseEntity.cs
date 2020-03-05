using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class BaseEntity
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
