using Pokedex.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Pokedex.DAL
{
    public class DexContext : DbContext
    {
        public DexContext(): base("DexContext")
        {
        }

        public DbSet<PKMN> PKMNs { get; set; }
        public DbSet<TMHM> TMHMs { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CanLearn> CanLearns { get; set; }
        public DbSet<FoundAt> FoundAts { get; set; }
        public DbSet<Evolution> Evolutions { get; set; }
        public DbSet<Caught> Caughts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

           /* modelBuilder.Entity<PKMN>()
             .HasMany(c => c.Users).WithMany(i => i.PKMNs)
             .Map(t => t.MapLeftKey("PKMNID")
                 .MapRightKey("UserID")
                 .ToTable("Caught"));//*/
        }
       // public System.Data.Entity.DbSet<Pokedex.Models.User> Users { get; set; }
        //public System.Data.Entity.DbSet<Pokedex.Models.Caught> Caughts { get; set; }

    }
}