using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PROG_POE_CMCS.Models;

namespace PROG_POE_CMCS.Data
{
    public class PROG_POE_CMCSContext : DbContext
    {
        public PROG_POE_CMCSContext (DbContextOptions<PROG_POE_CMCSContext> options)
            : base(options)
        {
        }

        public DbSet<PROG_POE_CMCS.Models.Claim> Claim { get; set; } = default!;
    }
}
