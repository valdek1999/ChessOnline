namespace TestWebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelChessDB : DbContext
    {
        public ModelChessDB()
            : base("name=ModelChessDB")
        {
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Side> Sides { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .Property(e => e.FEN)
                .IsUnicode(false);

            modelBuilder.Entity<Game>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Sides)
                .WithRequired(e => e.Game)
                .HasForeignKey(e => e.GamesID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Sides)
                .WithRequired(e => e.Player)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Side>()
                .Property(e => e.YourColor)
                .IsUnicode(false);
        }
    }
}
