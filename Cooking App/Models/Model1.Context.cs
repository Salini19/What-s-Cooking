﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cooking_App.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FoodReceipesEntities : DbContext
    {
        public FoodReceipesEntities()
            : base("name=FoodReceipesEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FeedBack> FeedBacks { get; set; }
        public virtual DbSet<Logged> Loggeds { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Receipe> Receipes { get; set; }
    }
}
