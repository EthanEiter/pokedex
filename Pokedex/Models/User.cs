using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models
{
    public class User
    {

        public int UserID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string Name { get; set; }
        public virtual ICollection<Caught> Caughts { get; set; }
        public virtual ICollection<PKMN> PKMNs { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}