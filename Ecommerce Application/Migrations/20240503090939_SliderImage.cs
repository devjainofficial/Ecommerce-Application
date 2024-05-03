﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Application.Migrations
{
    /// <inheritdoc />
    public partial class SliderImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SliderImage",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SliderImage",
                table: "Categories");
        }
    }
}
