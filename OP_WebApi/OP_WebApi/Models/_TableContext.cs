using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OP_WebApi.Models;

namespace OP_WebApi.Models
{
    public class TableContext : DbContext
    {
        public TableContext(DbContextOptions<TableContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<LoginHistory> LoginHistory { get; set; }
        public virtual DbSet<User_Level> User_Level { get; set; }
        public virtual DbSet<UL_See_UL> UL_See_UL { get; set; }
        public virtual DbSet<User_UL> User_UL { get; set; }
        public virtual DbSet<UL_Feature> UL_Feature { get; set; }
        public virtual DbSet<User_Level_UL_Feature> Level_UL_Feature { get; set; }
        public virtual DbSet<UL_See_OL> UL_See_OL { get; set; }
        public virtual DbSet<UL_Request_Category> UL_Request_Category { get; set; }
        public virtual DbSet<User_File> User_File { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrdersCollection> OrdersCollection { get; set; }
        public virtual DbSet<Collection> Collection { get; set; }
        public virtual DbSet<Collection_Item> Collection_Item { get; set; }
        public virtual DbSet<Collection_Action> Collection_Action { get; set; }
        public virtual DbSet<Collection_Action_History> Collection_Action_History { get; set; }
        public virtual DbSet<Order_Attachment> Order_Attachment { get; set; }
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order_Customer> Order_Customer { get; set; }
        public virtual DbSet<Order_Item> Order_Item { get; set; }
        public virtual DbSet<Order_StockItem> Order_StockItem { get; set; }
        public virtual DbSet<Order_Item_Property> Order_Item_Property { get; set; }
        public virtual DbSet<Order_Level> Order_Level { get; set; }
        public virtual DbSet<Order_OL> Order_OL { get; set; }
        public virtual DbSet<Order_Level_on_Returning> Order_Level_on_Returning { get; set; }
        public virtual DbSet<OL_Prerequisite> OL_Prerequisite { get; set; }
        public virtual DbSet<OL_UL> OL_UL { get; set; }
        public virtual DbSet<Order_History> Order_History { get; set; }
        public virtual DbSet<Proforma> Proforma { get; set; }
        public virtual DbSet<Proforma_Row> Proforma_Row { get; set; }
        public virtual DbSet<OrderPriority> OrderPriority { get; set; }
        public virtual DbSet<Property> Property { get; set; }
        //_db.DropTableAsync<Item> { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Item_File> Item_File { get; set; }
        public virtual DbSet<Item_Property> Item_Property { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Action> Action { get; set; }
        public virtual DbSet<OPC> OPC { get; set; }
        public virtual DbSet<Item_OPC> Item_OPC { get; set; }
        public virtual DbSet<OPC_Acions> OPC_Acion { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<Unit_Acion> Unit_Acion { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        public virtual DbSet<Warehouse_Request> Warehouse_Request { get; set; }
        public virtual DbSet<Warehouse_Request_Row> Warehouse_Request_Row { get; set; }
        public virtual DbSet<Warehouse_Request_History> Warehouse_Request_History { get; set; }
        public virtual DbSet<Warehouse_Remittance> Warehouse_Remittance { get; set; }
        public virtual DbSet<Warehouse_Remittance_History> Warehouse_Remittance_History { get; set; }
        public virtual DbSet<Warehouse_Remittance_Row> Warehouse_Remittance_Row { get; set; }
        public virtual DbSet<CostCenter> CostCenter { get; set; }
        public virtual DbSet<PurchaseRequest> PurchaseRequest { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite("DataSource=System.SQLite.DB.db3");
            }
        }



    }
}
