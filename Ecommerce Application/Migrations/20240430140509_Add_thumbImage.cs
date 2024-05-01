﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Application.Migrations
{
    /// <inheritdoc />
    public partial class Add_thumbImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbImage",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbImage",
                table: "ProductDetails");
        }
    }
}
