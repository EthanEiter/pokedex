using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Pokedex.ViewModels
{
    public class TrainerPercentageGroup
    {

        public string UserName { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public double percentage { get; set; }
    }
}