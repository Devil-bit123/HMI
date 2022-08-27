using Microsoft.EntityFrameworkCore;
using WA_Interfaces.Models;

namespace WA_Interfaces.Context
{
    public class conexionSQL : DbContext
    {
        public conexionSQL(DbContextOptions<conexionSQL> options) : base (options)
        {
            //
        }

        public DbSet<sensores> sensores { get; set; }
    }
}
