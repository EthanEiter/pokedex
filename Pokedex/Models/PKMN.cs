using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Pokedex.ViewModels;

namespace Pokedex.Models
{
    public class PKMN
    {
        [DisplayFormat(DataFormatString="{0:000}")]
        public int PKMNID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        [DisplayFormat(NullDisplayText = "No Subtype")]
        public string Subtype { get; set; }
        public virtual ICollection<CanLearn> CanLearns { get; set; }
        public virtual ICollection<FoundAt> FoundAts { get; set; }
        public virtual ICollection<Caught> Caughts { get; set; }
        public virtual ICollection<User> Users { get; set; }
        //public virtual EvoGroup Evo { get; set; }
    }
}