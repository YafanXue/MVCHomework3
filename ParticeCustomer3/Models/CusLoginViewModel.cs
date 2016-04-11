using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParticeCustomer3.Models
{
    public class CusLoginViewModel
    {
        [Required]
        public string ACCOUNT { get; set; }
        [Required]
        public string PASSWORD { get; set; }
    }
}