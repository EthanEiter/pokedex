using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pokedex.Models;

namespace Pokedex.ViewModels
{
    public class UserIndexData
    {
        public string userName { get; set; }
        public string pokeName { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Caught> Caughts { get; set; }
        public IEnumerable<CanLearn> CanLearns { get; set; }
    }
}