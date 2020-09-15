using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RecipesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipesApi.Data
{
    public class RecipeContext : DbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options) { }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Commenting> Commenting { get; set; }
        public static System.Collections.Specialized.NameValueCollection AppSettings { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .Property(p => p.RecipeId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Commenting>()
                .Property(p => p.CommentingId)
                .ValueGeneratedOnAdd();
        }
    }
}
