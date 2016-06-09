using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iBalekaService.Migrations
{
    public partial class TableFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Athlete_ClubMember_MemberID",
                table: "Athlete");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubMember_Club_ClubID",
                table: "ClubMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClubMember",
                table: "ClubMember");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClubMember",
                table: "ClubMember",
                column: "MemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Athlete_ClubMember_MemberID",
                table: "Athlete",
                column: "MemberID",
                principalTable: "ClubMember",
                principalColumn: "MemberID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubMember_Club_ClubID",
                table: "ClubMember",
                column: "ClubID",
                principalTable: "Club",
                principalColumn: "ClubID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_ClubMember_ClubID",
                table: "ClubMember",
                newName: "IX_ClubMember_ClubID");

            migrationBuilder.RenameTable(
                name: "ClubMember",
                newName: "ClubMember");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Athlete_ClubMember_MemberID",
                table: "Athlete");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubMember_Club_ClubID",
                table: "ClubMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClubMember",
                table: "ClubMember");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClubMember",
                table: "ClubMember",
                column: "MemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Athlete_ClubMember_MemberID",
                table: "Athlete",
                column: "MemberID",
                principalTable: "ClubMember",
                principalColumn: "MemberID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubMember_Club_ClubID",
                table: "ClubMember",
                column: "ClubID",
                principalTable: "Club",
                principalColumn: "ClubID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_ClubMember_ClubID",
                table: "ClubMember",
                newName: "IX_ClubMember_ClubID");

            migrationBuilder.RenameTable(
                name: "ClubMember",
                newName: "ClubMember");
        }
    }
}
