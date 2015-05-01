using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokedex.ViewModels
{
    public class AssignedPKMNData
    {
        public int PKMNID { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}