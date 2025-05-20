using BibliotecaBusiness.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace BibliotecaData.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }

        public AppDbContext(DbContextOptions options): base(options) {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           

            base.OnModelCreating(modelBuilder);

            // Ajusta os mapeamentos para o Identity com Guid
            modelBuilder.Entity<IdentityUser<Guid>>(entity => entity.ToTable("AspNetUsers"));
            modelBuilder.Entity<IdentityRole<Guid>>(entity => entity.ToTable("AspNetRoles"));
            modelBuilder.Entity<IdentityUserRole<Guid>>(entity => entity.ToTable("AspNetUserRoles"));
            modelBuilder.Entity<IdentityUserClaim<Guid>>(entity => entity.ToTable("AspNetUserClaims"));
            modelBuilder.Entity<IdentityUserLogin<Guid>>(entity => entity.ToTable("AspNetUserLogins"));
            modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity => entity.ToTable("AspNetRoleClaims"));
            modelBuilder.Entity<IdentityUserToken<Guid>>(entity => entity.ToTable("AspNetUserTokens"));
        }
    }
}
