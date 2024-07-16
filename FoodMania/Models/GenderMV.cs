using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodMania.Models
{
    public class GenderMV
    {
        [Display(Name ="#Unique No")]
        public int GenderID { get; set; }
        [Display(Name = "Gender Title")]
        public string GenderTittle { get; set; }
    }
}