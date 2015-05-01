using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Pokedex.Models
{
    public class Map
    {
        public int MapID { get; set; }
        public string Name { get; set; }
        [DisplayFormat(NullDisplayText = "N/A")]
        public string Region { get; set; }
        public virtual ICollection<FoundAt> FoundAts { get; set; }
    }
}