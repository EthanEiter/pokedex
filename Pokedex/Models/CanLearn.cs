using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokedex.Models
{
    public class CanLearn
    {
        public int CanLearnID { get; set; }
        public int PKMNID { get; set; }
        public string TMHMID { get; set; }
        public virtual PKMN PKMN { get; set; }
        public virtual TMHM TMHM { get; set; }
    }
}