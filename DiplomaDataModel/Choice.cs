using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DiplomaDataModel
{
    public class Choice
    {
        [HiddenInput(DisplayValue = false)]
        //[ScaffoldColumn(false)]
        public int ChoiceId { get; set; }

        [ScaffoldColumn(false)]
        public int YearTermId { get; set; }

        [ForeignKey("YearTermId")]
        public YearTerm YearTerm { get; set; }

        [Required]
        [Display(Name = "Student Id")]
        [RegularExpression("^(a00|A00)[0-9]{6}", ErrorMessage = "Invalid Student ID")]
        public string StudentId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MaxLength(40), MinLength(3)]
        public string StudentFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(40), MinLength(3)]
        public string StudentLastName { get; set; }

        [Display(Name = "First Choice")]
        [UIHint("_FirstChoice")]
        public int FirstChoiceOptionId { get; set; }
        [Display(Name = "Second Choice")]
        [UIHint("_SecondChoice")]
        public int SecondChoiceOptionId { get; set; }
        [Display(Name = "Third Choice")]
        [UIHint("_ThirdChoice")]
        public int ThirdChoiceOptionId { get; set; }
        [Display(Name = "Fourth Choice")]
        [UIHint("_FourthChoice")]
        public int FourthChoiceOptionId { get; set; }

        [ForeignKey("FirstChoiceOptionId")]
        public Options FirstChoiceOption { get; set; }

        [ForeignKey("SecondChoiceOptionId")]
        public Options SecondChoiceOption { get; set; }

        [ForeignKey("ThirdChoiceOptionId")]
        public Options ThirdChoiceOption { get; set; }

        [ForeignKey("FourthChoiceOptionId")]
        public Options FourthChoiceOption { get; set; }

        [ScaffoldColumn(false)]
        public DateTime SelectionDate { get; set; }

    }
}
