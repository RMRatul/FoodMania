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
    
    public partial class CartItemDetailTable
    {
        public int CartItemID { get; set; }
        public int CartID { get; set; }
        public int StockItemID { get; set; }
        public int Qty { get; set; }
    
        public virtual CartTable CartTable { get; set; }
        public virtual StockItemTable StockItemTable { get; set; }
    }
}
