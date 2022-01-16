using Microsoft.EntityFrameworkCore;
using Restaurants57Blocks.Domain.Entities;

namespace Restaurants57Blocks.Infrastructure.DBContext
{
    public partial class Restaurants57BlocksDBContext: DbContext
    {
        /// <summary>
        /// Inicializador de <class>MillonInmobiliariaDBContext</class>
        /// </summary>
        public Restaurants57BlocksDBContext()
        {
        }

        /// <summary>
        /// Inicializador de <class>MillonInmobiliariaDBContext</class>
        /// </summary>
        /// <param name="options">options</param>
        public Restaurants57BlocksDBContext(DbContextOptions<Restaurants57BlocksDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Restaurant> Restaurant { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-K991GUOR;Database=Restaurants57Blocks;Trusted_Connection=False;User=sa;password=Complemento$0912;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
