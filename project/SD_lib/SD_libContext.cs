using System;
using Microsoft.EntityFrameworkCore;
using SD_lib.Entity.Models;

namespace SD_lib.DB
{
    public class SD_libContext : DbContext
    {
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountSession> Sessions { get; set; }


        #region Constructor

        public SD_libContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        #endregion


        #region Creating connection

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("DataSource=SD.db;Cache=Shared");
        }

        #endregion


        #region Creating tables

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>();
            modelBuilder.Entity<Card>();
            modelBuilder.Entity<Account>();
            modelBuilder.Entity<AccountSession>();
        }

        #endregion
    }
}

