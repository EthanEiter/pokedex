using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokedex.Models
{
    public class Evolution
    {

        public int EvolutionID { get; set; }
        public int PKMNID { get; set; }
        public int PKMNFromID { get; set; }
        public int Lvl { get; set; }
        public string Notes { get; set; }
        public virtual PKMN From { get; set; }

    }
}