using colorsql.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace colorsql.Models
{
    public class ColorsContext : DbContext
    {
        public ColorsContext() { }
        public ColorsContext(DbContextOptions<ColorsContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Translation> Translations { get; set; }
    }
}