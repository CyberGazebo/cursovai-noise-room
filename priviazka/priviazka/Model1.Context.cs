﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace priviazka
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NoizeRoomEntities : DbContext
    {
        public NoizeRoomEntities()
            : base("name=NoizeRoomEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<client> clients { get; set; }
        public virtual DbSet<finance> finances { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<schedule> schedules { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}