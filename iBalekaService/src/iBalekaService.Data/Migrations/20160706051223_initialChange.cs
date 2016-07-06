using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iBalekaService.Data.Migrations
{
    public partial class initialChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberID",
                table: "Athlete");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberID",
                table: "Athlete",
                nullable: false,
                defaultValue: 0);
        }
    }
}
