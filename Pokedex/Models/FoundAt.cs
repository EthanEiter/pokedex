using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Pokedex.Models
{
    public class FoundAt
    {
        public int FoundAtID { get; set; }
        public int PKMNID { get; set; }
        public int MapID { get; set; }
        [DisplayFormat(NullDisplayText = "Random Battle")]
        public string Notes { get; set; }
        public virtual PKMN PKMN { get; set; }
        public virtual Map Map { get; set; }
    }
}