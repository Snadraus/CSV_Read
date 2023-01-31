using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CSV_Read.Models;

namespace CSV_Read.Data
{
    public class CSVContext : DbContext
    {
        public CSVContext (DbContextOptions<CSVContext> options)
            : base(options)
        {
        }

        public DbSet<CSV_Read.Models.CSV_Headers> CSV_Headers { get; set; } = default!;
    }
}
