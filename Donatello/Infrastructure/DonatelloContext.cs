using Donatello.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donatello.Infrastructure
{
    public class DonatelloContext : DbContext
    {
        public DonatelloContext(DbContextOptions<DonatelloContext> options)
            :base(options)
        {

        }

        public DbSet<Board> Boards { get; set; }
        public DbSet<Column> Columns{ get; set; }
        public DbSet<Card> Cards { get; set; }
    }
}
