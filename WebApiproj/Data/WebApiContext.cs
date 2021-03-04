using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApiproj.Models;

namespace WebApiproj.Data
{
    public class WebApiContext:DbContext
    {
        public WebApiContext(DbContextOptions<WebApiContext> opt) : base(opt)
        {

        }

        public DbSet<WebApiModel> WebApiTable { get; set; }
    }
}
