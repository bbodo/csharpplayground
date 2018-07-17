using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MVCWebApp.Models
{
    public class MVCWebAppContext : DbContext
    {
        public MVCWebAppContext (DbContextOptions<MVCWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<MVCWebApp.Models.Movie> Movie { get; set; }
    }
}
