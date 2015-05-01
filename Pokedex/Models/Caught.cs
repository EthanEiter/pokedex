using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Pokedex.Models
{
    public class Caught
    {
        public int CaughtID { get; set; }
        [DisplayFormat(DataFormatString = "{0:000}")]
        public int PKMNID { get; set; }
        public int UserID { get; set; }
        public virtual PKMN PKMN { get; set; }
        public virtual User User { get; set; }
    }
}