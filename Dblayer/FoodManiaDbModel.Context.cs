﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dblayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FoodManiaEntities : DbContext
    {
        public FoodManiaEntities()
            : base("name=FoodManiaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AddressTypeTable> AddressTypeTables { get; set; }
        public virtual DbSet<DiscountTable> DiscountTables { get; set; }
        public virtual DbSet<GenderTable> GenderTables { get; set; }
        public virtual DbSet<OrderDealDetailTable> OrderDealDetailTables { get; set; }
        public virtual DbSet<OrderItemDetailTable> OrderItemDetailTables { get; set; }
        public virtual DbSet<OrderStatusTable> OrderStatusTables { get; set; }
        public virtual DbSet<OrderTable> OrderTables { get; set; }
        public virtual DbSet<OrderTypeTable> OrderTypeTables { get; set; }
        public virtual DbSet<ReservationStatusTable> ReservationStatusTables { get; set; }
        public virtual DbSet<StockDealDetailTable> StockDealDetailTables { get; set; }
        public virtual DbSet<StockDealTable> StockDealTables { get; set; }
        public virtual DbSet<StockItemCategoryTable> StockItemCategoryTables { get; set; }
        public virtual DbSet<StockItemIngredientTable> StockItemIngredientTables { get; set; }
        public virtual DbSet<StockItemTable> StockItemTables { get; set; }
        public virtual DbSet<StockMenuCategoryTable> StockMenuCategoryTables { get; set; }
        public virtual DbSet<StockMenuItemTable> StockMenuItemTables { get; set; }
        public virtual DbSet<TableReservationTable> TableReservationTables { get; set; }
        public virtual DbSet<UserAddressTable> UserAddressTables { get; set; }
        public virtual DbSet<UserStatusTable> UserStatusTables { get; set; }
        public virtual DbSet<UserTable> UserTables { get; set; }
        public virtual DbSet<UserTypeTable> UserTypeTables { get; set; }
        public virtual DbSet<VisibleStatusTable> VisibleStatusTables { get; set; }
        public virtual DbSet<UserDetailTable> UserDetailTables { get; set; }
    }
}
