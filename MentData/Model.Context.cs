﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MentData
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MentDBEntities : DbContext
    {
        public MentDBEntities()
            : base("name=MentDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<RefRole> RefRoles { get; set; }
        public virtual DbSet<Reward> Rewards { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserInReward> UserInRewards { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Acc> Accs { get; set; }
        public virtual DbSet<AccRole> AccRoles { get; set; }
    }
}
