//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class StockItemTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StockItemTable()
        {
            this.OrderItemDetailTables = new HashSet<OrderItemDetailTable>();
            this.StockDealDetailTables = new HashSet<StockDealDetailTable>();
            this.StockItemIngredientTables = new HashSet<StockItemIngredientTable>();
            this.StockMenuItemTables = new HashSet<StockMenuItemTable>();
        }
    
        public int StockItemID { get; set; }
        public int StockItemCategoryID { get; set; }
        public string ItemPhotoPath { get; set; }
        public string StockItemTitle { get; set; }
        public string ItemSize { get; set; }
        public double UnitPrice { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public int VisibleStatusID { get; set; }
        public int CreatedBy_UserID { get; set; }
        public int OrderTypeID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItemDetailTable> OrderItemDetailTables { get; set; }
        public virtual OrderTypeTable OrderTypeTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockDealDetailTable> StockDealDetailTables { get; set; }
        public virtual StockItemCategoryTable StockItemCategoryTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockItemIngredientTable> StockItemIngredientTables { get; set; }
        public virtual VisibleStatusTable VisibleStatusTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockMenuItemTable> StockMenuItemTables { get; set; }
    }
}
