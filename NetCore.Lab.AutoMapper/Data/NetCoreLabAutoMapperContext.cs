using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCore.Lab.AutoMapper.Models;

namespace NetCore.Lab.AutoMapper.Data
{
    public class NetCoreLabAutoMapperContext : DbContext
    {
        public NetCoreLabAutoMapperContext (DbContextOptions<NetCoreLabAutoMapperContext> options)
            : base(options)
        {
        }

        public DbSet<NetCore.Lab.AutoMapper.Models.Todo> Todo { get; set; }

        public DbSet<NetCore.Lab.AutoMapper.Models.User> User { get; set; }
    }
}
