﻿using Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Context
{
    public class Database : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Settings.Configurations.ConnectionString);
        }
    }
}
