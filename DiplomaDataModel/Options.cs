using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DiplomaDataModel
{
    public class Options
    {
       
        public int OptionsId { get; set; }

        [Required]
        [MaxLength(50), MinLength(3)]
        public string Title { get; set; }

        public bool isActive { get; set; }
    }
}
