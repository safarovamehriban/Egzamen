﻿using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public class AppDbContext : IdentityDbContext

    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)  { }
    }
}
