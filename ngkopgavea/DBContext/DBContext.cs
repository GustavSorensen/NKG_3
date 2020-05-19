using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ngkopgavea.DBContext
{
    public class DBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            ob.UseSqlServer(
                @"Data Source=(localdb)\MSSQLLocalDB;TrustServerCertificate=False;MultiSubnetFailover=False;database=NGK3_db;");
        }
    }
}


