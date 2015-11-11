using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel
{
    public class YearTerm
    {

        public int YearTermId { get; set; }

        [DataType(DataType.Text)]
        public int Year { get; set; }

        [DataType(DataType.Text)]
        [RegularExpression("(10|20|30)", ErrorMessage = "Invalid term")]
        public int Term { get; set; }

        public Boolean isDefault { get; set; }
    }
}