using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OP_WebApi.Migrations
{
    public partial class op14000618A1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Action",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Time = table.Column<double>(nullable: false),
                    Cost_per_Hour = table.Column<long>(nullable: false),
                    Workers = table.Column<double>(nullable: false),
                    Enable = table.Column<bool>(nullable: false),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Need_Supervisor_Confirmation = table.Column<bool>(nullable: false),
                    Need_Manager_Confirmation = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    DateTime_sh = table.Column<string>(nullable: true),
                    PreviousLevel_Index = table.Column<long>(nullable: false),
                    CurrentLevel_Index = table.Column<long>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collection_Action",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Collection_Id = table.Column<long>(nullable: false),
                    Action_Id = table.Column<long>(nullable: false),
                    Action_Name = table.Column<string>(nullable: true),
                    Action_Time = table.Column<double>(nullable: false),
                    Action_Workers = table.Column<double>(nullable: false),
                    Action_TotalCost = table.Column<long>(nullable: false),
                    Action_Prerequisites = table.Column<string>(nullable: true),
                    OPC_Action_Priority = table.Column<int>(nullable: false),
                    ProgressPercent_Real = table.Column<int>(nullable: false),
                    ProgressPercent_Planning = table.Column<int>(nullable: false),
                    CurrentContractor_Id = table.Column<long>(nullable: false),
                    Confirm_Contractor = table.Column<bool>(nullable: false),
                    Confirm_LineManager = table.Column<bool>(nullable: false),
                    Confirm_QC = table.Column<bool>(nullable: false),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection_Action", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collection_Action_History",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Collection_Id = table.Column<long>(nullable: false),
                    Action_Id = table.Column<long>(nullable: false),
                    ProgressPercent_Real = table.Column<int>(nullable: false),
                    CurrentContractor_Id = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Confirm_Contractor = table.Column<bool>(nullable: false),
                    Confirm_LineManager = table.Column<bool>(nullable: false),
                    Confirm_QC = table.Column<bool>(nullable: false),
                    DateTime_sh = table.Column<string>(nullable: true),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection_Action_History", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collection_Item",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Collection_Id = table.Column<long>(nullable: false),
                    Item_Id = table.Column<long>(nullable: false),
                    Item_SmallCode = table.Column<string>(nullable: true),
                    Item_SmallName = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Real_Name = table.Column<string>(nullable: true),
                    OwnerName = table.Column<string>(nullable: true),
                    Domain = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    EMail = table.Column<string>(nullable: true),
                    DateTime_sh = table.Column<string>(nullable: true),
                    End_DateTime_sh = table.Column<string>(nullable: true),
                    Warehouse_AutomaticBooking = table.Column<bool>(nullable: false),
                    Warehouse_Booking_MaxHours = table.Column<long>(nullable: false),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostCenter",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Index_in_Company = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Index = table.Column<string>(nullable: true),
                    User_Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Content = table.Column<byte[]>(nullable: true),
                    OriginalFileName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateTime_sh = table.Column<string>(nullable: true),
                    Enable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Category_Id = table.Column<long>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    Enable = table.Column<bool>(nullable: false),
                    Name_Samll = table.Column<string>(nullable: true),
                    Code_Small = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Module = table.Column<bool>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Significance_Factor = table.Column<int>(nullable: false),
                    Depo_type = table.Column<int>(nullable: false),
                    Salable = table.Column<bool>(nullable: false),
                    FixedPrice = table.Column<long>(nullable: false),
                    SalesPrice = table.Column<long>(nullable: false),
                    Warehouse_Id = table.Column<long>(nullable: false),
                    Wh_OrderPoint = table.Column<double>(nullable: false),
                    Wh_OrderQuantity = table.Column<double>(nullable: false),
                    Wh_Quantity_Real = table.Column<double>(nullable: false),
                    Wh_Quantity_Booking = table.Column<double>(nullable: false),
                    Wh_Quantity_x = table.Column<double>(nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    Weight = table.Column<double>(nullable: false),
                    Name_Full = table.Column<string>(nullable: true),
                    Code_Full = table.Column<string>(nullable: true),
                    Need_QC_Confirmation = table.Column<bool>(nullable: false),
                    Need_Manager_Confirmation = table.Column<bool>(nullable: false),
                    Bookable = table.Column<bool>(nullable: false),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_D1 = table.Column<double>(nullable: false),
                    C_D2 = table.Column<double>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item_File",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Item_Code_Small = table.Column<string>(nullable: true),
                    File_Index = table.Column<long>(nullable: false),
                    Enable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item_File", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item_OPC",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    OPC_Id = table.Column<long>(nullable: false),
                    Item_Id = table.Column<long>(nullable: false),
                    Item_SmallCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item_OPC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item_Property",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Item_Id = table.Column<long>(nullable: false),
                    Item_Code_Small = table.Column<string>(nullable: true),
                    Property_Index = table.Column<long>(nullable: false),
                    DefaultValue = table.Column<string>(nullable: true),
                    ChangingValue = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item_Property", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginHistory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    User_Id = table.Column<long>(nullable: false),
                    User_RealName = table.Column<string>(nullable: true),
                    Date_sh = table.Column<string>(nullable: true),
                    Time = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Enable = table.Column<bool>(nullable: false),
                    Module_Code_Small = table.Column<string>(nullable: true),
                    Item_Id = table.Column<long>(nullable: false),
                    Item_Code_Small = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Unit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OL_Prerequisite",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    OL_Id = table.Column<long>(nullable: false),
                    Prerequisite_Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OL_Prerequisite", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OL_UL",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    OL_Id = table.Column<long>(nullable: false),
                    UL_Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OL_UL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OPC",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Enable = table.Column<bool>(nullable: false),
                    Time = table.Column<double>(nullable: false),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OPC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OPC_Acion",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    OPC_Id = table.Column<long>(nullable: false),
                    Action_Id = table.Column<long>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    Action_Prerequisites = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OPC_Acion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Index_in_Company = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    User_Id = table.Column<long>(nullable: false),
                    Index = table.Column<string>(nullable: true),
                    Customer_Index = table.Column<string>(nullable: true),
                    Customer_Name = table.Column<string>(nullable: true),
                    Date_sh = table.Column<string>(nullable: true),
                    Time = table.Column<string>(nullable: true),
                    type = table.Column<int>(nullable: false),
                    PreviousLevel_Id = table.Column<long>(nullable: false),
                    CurrentLevel_Id = table.Column<long>(nullable: false),
                    NextLevel_Id = table.Column<long>(nullable: false),
                    Level_Description = table.Column<string>(nullable: true),
                    Order_Description = table.Column<string>(nullable: true),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_Attachment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Order_Index = table.Column<string>(nullable: true),
                    File_Index = table.Column<long>(nullable: false),
                    Enable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Attachment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_Customer",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Customer_Index = table.Column<string>(nullable: true),
                    Order_Index = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_History",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    User_Id = table.Column<long>(nullable: false),
                    User_Name = table.Column<string>(nullable: true),
                    User_Level_Id = table.Column<long>(nullable: false),
                    Order_Index = table.Column<string>(nullable: true),
                    OrderLevel_Id = table.Column<long>(nullable: false),
                    OrderLevel_Description = table.Column<string>(nullable: true),
                    DateTime_sh = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_History", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_Item",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Order_Index = table.Column<string>(nullable: true),
                    Item_Id = table.Column<long>(nullable: false),
                    Item_Name_Samll = table.Column<string>(nullable: true),
                    Item_SmallCode = table.Column<string>(nullable: true),
                    Module_SmallCode = table.Column<string>(nullable: true),
                    Item_Module = table.Column<bool>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    FixedPrice = table.Column<long>(nullable: false),
                    SalesPrice = table.Column<long>(nullable: false),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_Item_Property",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    OI_Index = table.Column<long>(nullable: false),
                    Order_Index = table.Column<string>(nullable: true),
                    Item_Id = table.Column<long>(nullable: false),
                    Item_SmallCode = table.Column<string>(nullable: true),
                    ItemBatch_Counter = table.Column<int>(nullable: false),
                    ItemOrder_Counter = table.Column<int>(nullable: false),
                    Property_Index = table.Column<long>(nullable: false),
                    Property_Name = table.Column<string>(nullable: true),
                    Property_Description = table.Column<string>(nullable: true),
                    Property_Value = table.Column<string>(nullable: true),
                    Property_ChangingValue = table.Column<bool>(nullable: false),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Item_Property", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_Level",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    Sequence = table.Column<long>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    OrderCanChange = table.Column<bool>(nullable: false),
                    ReturningLevel = table.Column<bool>(nullable: false),
                    CancelingLevel = table.Column<bool>(nullable: false),
                    RemovingLevel = table.Column<bool>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Type_Description = table.Column<string>(nullable: true),
                    FirstLevel = table.Column<bool>(nullable: false),
                    LastLevel = table.Column<bool>(nullable: false),
                    MessageText = table.Column<string>(nullable: true),
                    Description2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Level", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_Level_on_Returning",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    OrderLevel_Id = table.Column<long>(nullable: false),
                    OL_Retruned_Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Level_on_Returning", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_OL",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Order_Index = table.Column<string>(nullable: true),
                    OrderLevel_Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_OL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_StockItem",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    Order_Index = table.Column<string>(nullable: true),
                    Top_Name = table.Column<string>(nullable: true),
                    Top_Code = table.Column<string>(nullable: true),
                    Item_Id = table.Column<long>(nullable: false),
                    Item_SmallCode = table.Column<string>(nullable: true),
                    Item_Name_Samll = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    WarehouseIndex = table.Column<long>(nullable: false),
                    Quantity_CanTake = table.Column<double>(nullable: false),
                    Quantity_Remained = table.Column<double>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_StockItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderPriority",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Order_Index = table.Column<string>(nullable: true),
                    Order_Title = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    TotalQuantity = table.Column<double>(nullable: false),
                    CanTakeQuantity = table.Column<double>(nullable: false),
                    RemainedQuantity = table.Column<double>(nullable: false),
                    ProgressPercent = table.Column<int>(nullable: false),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPriority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdersCollection",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Collection_Id = table.Column<long>(nullable: false),
                    Order_Index = table.Column<string>(nullable: true),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proforma",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Index = table.Column<string>(nullable: true),
                    Enable = table.Column<bool>(nullable: false),
                    User_Id = table.Column<long>(nullable: false),
                    Order_Index = table.Column<string>(nullable: true),
                    Order_Id = table.Column<long>(nullable: false),
                    Order_ProformaNo = table.Column<string>(nullable: true),
                    DateTime_sh = table.Column<string>(nullable: true),
                    Customer_Index = table.Column<string>(nullable: true),
                    Customer_Name = table.Column<string>(nullable: true),
                    TotalPrice_withoutDiscount = table.Column<int>(nullable: false),
                    Discount1 = table.Column<double>(nullable: false),
                    Discount2 = table.Column<double>(nullable: false),
                    Discount3 = table.Column<double>(nullable: false),
                    Discount4 = table.Column<double>(nullable: false),
                    Tax = table.Column<double>(nullable: true),
                    TotalPrice_NetPayable = table.Column<int>(nullable: false),
                    PaymentMethod_Description = table.Column<string>(nullable: true),
                    DiscountPayment_Factor = table.Column<double>(nullable: false),
                    RegSign_Index = table.Column<string>(nullable: true),
                    Confirmor10Sign_Index = table.Column<string>(nullable: true),
                    Confirmor20Sign_Index = table.Column<string>(nullable: true),
                    Confirmor30Sign_Index = table.Column<string>(nullable: true),
                    Confirmor90Sign_Index = table.Column<string>(nullable: true),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proforma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proforma_Row",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Proforma_Index = table.Column<string>(nullable: true),
                    User_Id = table.Column<long>(nullable: false),
                    Order_Index = table.Column<string>(nullable: true),
                    Order_Id = table.Column<long>(nullable: false),
                    Item_Id = table.Column<long>(nullable: false),
                    Item_SmallCode = table.Column<string>(nullable: true),
                    Item_SmallName = table.Column<string>(nullable: true),
                    Depo_type = table.Column<int>(nullable: true),
                    DontShow_in_Output = table.Column<bool>(nullable: false),
                    PriceUnit_withoutDiscount = table.Column<long>(nullable: false),
                    Discount_Quantity = table.Column<double>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    PriceRow_withoutDiscount = table.Column<int>(nullable: false),
                    PriceRow_withDiscount = table.Column<int>(nullable: false),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proforma_Row", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Enable = table.Column<bool>(nullable: false),
                    DefaultValue = table.Column<string>(nullable: true),
                    ChangingValue = table.Column<bool>(nullable: false),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRequest",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UL_Feature",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Unique_Phrase = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UL_Feature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UL_Request_Category",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    User_Level_Id = table.Column<long>(nullable: false),
                    Category_Id = table.Column<long>(nullable: false),
                    Supervisor_UL_Id = table.Column<long>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UL_Request_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UL_See_OL",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    UL_Id = table.Column<long>(nullable: false),
                    OL_Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UL_See_OL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UL_See_UL",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    MainUL_Id = table.Column<long>(nullable: false),
                    UL_Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UL_See_UL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Enable = table.Column<bool>(nullable: false),
                    Time = table.Column<double>(nullable: false),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit_Acion",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Unit_Id = table.Column<long>(nullable: false),
                    Action_Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit_Acion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Real_Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    EMail = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    UserLevel_Id = table.Column<long>(nullable: false),
                    UserLevel_Description = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    DateTime_sh = table.Column<string>(nullable: true),
                    User_Id_Creator = table.Column<long>(nullable: false),
                    End_DateTime_sh = table.Column<string>(nullable: true),
                    User_Domain = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_File",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    User_Id = table.Column<long>(nullable: false),
                    File_Id = table.Column<string>(nullable: true),
                    Enable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_File", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_Level",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Unit_Name = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Level", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_Level_UL_Feature",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    User_Level_Id = table.Column<long>(nullable: false),
                    UL_Feature_Id = table.Column<long>(nullable: false),
                    UL_Feature_Unique_Phrase = table.Column<string>(nullable: true),
                    UL_Feature_Enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Level_UL_Feature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_UL",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    User_Id = table.Column<long>(nullable: false),
                    UL_Id = table.Column<long>(nullable: false),
                    UL_Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_UL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse_Remittance",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Index_in_Company = table.Column<long>(nullable: false),
                    Warehouse_Id = table.Column<int>(nullable: false),
                    User_Id = table.Column<string>(nullable: true),
                    User_Name = table.Column<string>(nullable: true),
                    DateTime_sh = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    cost_center_Index = table.Column<long>(nullable: false),
                    cost_center_Descripiton = table.Column<string>(nullable: true),
                    Contractor_Id = table.Column<long>(nullable: false),
                    Contractor_Name = table.Column<string>(nullable: true),
                    Customer_Index = table.Column<string>(nullable: true),
                    Customer_Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    C_S1 = table.Column<string>(nullable: true),
                    C_S2 = table.Column<string>(nullable: true),
                    C_S3 = table.Column<string>(nullable: true),
                    C_L1 = table.Column<long>(nullable: false),
                    C_L2 = table.Column<long>(nullable: false),
                    C_I1 = table.Column<int>(nullable: false),
                    C_I2 = table.Column<int>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse_Remittance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse_Remittance_History",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Warehouse_Remittance_Id = table.Column<long>(nullable: false),
                    User_Id = table.Column<long>(nullable: false),
                    User_Name = table.Column<string>(nullable: true),
                    User_Level_Id = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DateTime_sh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse_Remittance_History", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse_Remittance_Row",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Warehouse_Remittance_Id = table.Column<long>(nullable: false),
                    Item_Id = table.Column<string>(nullable: true),
                    Item_SmallCode = table.Column<string>(nullable: true),
                    Item_Name = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Item_Unit = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Need_QC_Confirmation = table.Column<bool>(nullable: false),
                    QC_Confirmation_Type = table.Column<byte>(nullable: false),
                    Quantity_QC_Confirmed = table.Column<double>(nullable: false),
                    QC_DateTime_mi = table.Column<DateTime>(nullable: false),
                    QC_DateTime_sh = table.Column<string>(nullable: true),
                    Need_Manager_Confirmation = table.Column<bool>(nullable: false),
                    Manager_Confirmation_Type = table.Column<byte>(nullable: false),
                    Manager_DateTime_mi = table.Column<DateTime>(nullable: false),
                    Manager_DateTime_sh = table.Column<string>(nullable: true),
                    Quantity_Confirmed = table.Column<double>(nullable: false),
                    C_B1 = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse_Remittance_Row", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse_Request",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Order_Index = table.Column<string>(nullable: true),
                    Index_in_Company = table.Column<long>(nullable: false),
                    UserLevel_Id = table.Column<long>(nullable: false),
                    Unit_Name = table.Column<string>(nullable: true),
                    User_Id = table.Column<long>(nullable: false),
                    User_Name = table.Column<string>(nullable: true),
                    DateTime_sh = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status_Description = table.Column<string>(nullable: true),
                    Need_Supervisor_Confirmation = table.Column<bool>(nullable: false),
                    Request_Canceled = table.Column<bool>(nullable: false),
                    Sent_to_Warehouse = table.Column<bool>(nullable: false),
                    Request_Ready_to_Get = table.Column<bool>(nullable: false),
                    Request_Completed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse_Request", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse_Request_History",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Id = table.Column<long>(nullable: false),
                    Warehouse_Request_Id = table.Column<long>(nullable: false),
                    User_Id = table.Column<long>(nullable: false),
                    User_Name = table.Column<string>(nullable: true),
                    User_Level_Id = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Date_sh = table.Column<string>(nullable: true),
                    Time = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse_Request_History", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse_Request_Row",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    C_B1 = table.Column<bool>(nullable: false),
                    Company_Id = table.Column<long>(nullable: false),
                    Warehouse_Request_Id = table.Column<long>(nullable: false),
                    Warehouse_Request_Id_in_Company = table.Column<long>(nullable: false),
                    CostCenter_Id = table.Column<long>(nullable: false),
                    Item_Id = table.Column<long>(nullable: false),
                    Item_SmallCode = table.Column<string>(nullable: true),
                    Item_Name = table.Column<string>(nullable: true),
                    Item_Category_Id = table.Column<long>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Item_Unit = table.Column<string>(nullable: true),
                    Status_Description = table.Column<string>(nullable: true),
                    Reason_of_Cancelling = table.Column<string>(nullable: true),
                    Need_Supervisor_Confirmation = table.Column<bool>(nullable: false),
                    Supervisor_Confirmer_LevelIndex = table.Column<long>(nullable: false),
                    Canceled = table.Column<bool>(nullable: false),
                    Ready_to_Get = table.Column<bool>(nullable: false),
                    C_B2 = table.Column<bool>(nullable: false),
                    C_B3 = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse_Request_Row", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Action");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.DropTable(
                name: "Collection_Action");

            migrationBuilder.DropTable(
                name: "Collection_Action_History");

            migrationBuilder.DropTable(
                name: "Collection_Item");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "CostCenter");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Item_File");

            migrationBuilder.DropTable(
                name: "Item_OPC");

            migrationBuilder.DropTable(
                name: "Item_Property");

            migrationBuilder.DropTable(
                name: "LoginHistory");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "OL_Prerequisite");

            migrationBuilder.DropTable(
                name: "OL_UL");

            migrationBuilder.DropTable(
                name: "OPC");

            migrationBuilder.DropTable(
                name: "OPC_Acion");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Order_Attachment");

            migrationBuilder.DropTable(
                name: "Order_Customer");

            migrationBuilder.DropTable(
                name: "Order_History");

            migrationBuilder.DropTable(
                name: "Order_Item");

            migrationBuilder.DropTable(
                name: "Order_Item_Property");

            migrationBuilder.DropTable(
                name: "Order_Level");

            migrationBuilder.DropTable(
                name: "Order_Level_on_Returning");

            migrationBuilder.DropTable(
                name: "Order_OL");

            migrationBuilder.DropTable(
                name: "Order_StockItem");

            migrationBuilder.DropTable(
                name: "OrderPriority");

            migrationBuilder.DropTable(
                name: "OrdersCollection");

            migrationBuilder.DropTable(
                name: "Proforma");

            migrationBuilder.DropTable(
                name: "Proforma_Row");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "PurchaseRequest");

            migrationBuilder.DropTable(
                name: "UL_Feature");

            migrationBuilder.DropTable(
                name: "UL_Request_Category");

            migrationBuilder.DropTable(
                name: "UL_See_OL");

            migrationBuilder.DropTable(
                name: "UL_See_UL");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Unit_Acion");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "User_File");

            migrationBuilder.DropTable(
                name: "User_Level");

            migrationBuilder.DropTable(
                name: "User_Level_UL_Feature");

            migrationBuilder.DropTable(
                name: "User_UL");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "Warehouse_Remittance");

            migrationBuilder.DropTable(
                name: "Warehouse_Remittance_History");

            migrationBuilder.DropTable(
                name: "Warehouse_Remittance_Row");

            migrationBuilder.DropTable(
                name: "Warehouse_Request");

            migrationBuilder.DropTable(
                name: "Warehouse_Request_History");

            migrationBuilder.DropTable(
                name: "Warehouse_Request_Row");
        }
    }
}
