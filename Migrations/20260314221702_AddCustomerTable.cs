using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthWind.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[Customers]', N'U') IS NULL
BEGIN
    CREATE TABLE [Customers] (
        [CustomerId] int NOT NULL IDENTITY,
        [CompanyName] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NULL,
        [City] nvarchar(max) NULL,
        [Region] nvarchar(max) NULL,
        [PostalCode] nvarchar(max) NULL,
        [Country] nvarchar(max) NULL,
        [Phone] nvarchar(max) NULL,
        [Fax] nvarchar(max) NULL,
        CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerId])
    );
END
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[Discounts]', N'U') IS NULL
BEGIN
    CREATE TABLE [Discounts] (
        [DiscountId] int NOT NULL IDENTITY,
        [Code] int NOT NULL,
        [StartTime] datetime2 NOT NULL,
        [EndTime] datetime2 NOT NULL,
        [ProductId] int NOT NULL,
        [DiscountPercent] decimal(4,4) NOT NULL,
        [Title] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_Discounts] PRIMARY KEY ([DiscountId]),
        CONSTRAINT [FK_Discounts_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
    );
END
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[Discounts]', N'U') IS NOT NULL
   AND NOT EXISTS (SELECT 1 FROM sys.indexes WHERE [name] = N'IX_Discounts_ProductId' AND [object_id] = OBJECT_ID(N'[Discounts]'))
BEGIN
    CREATE INDEX [IX_Discounts_ProductId] ON [Discounts] ([ProductId]);
END
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[Customers]', N'U') IS NOT NULL
BEGIN
    DROP TABLE [Customers];
END
");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[Discounts]', N'U') IS NOT NULL
BEGIN
    DROP TABLE [Discounts];
END
");
        }
    }
}
