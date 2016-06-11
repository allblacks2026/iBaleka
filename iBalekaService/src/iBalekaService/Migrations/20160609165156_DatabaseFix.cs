using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iBalekaService.Migrations
{
    public partial class DatabaseFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Athlete_ClubMember_MemberID",
                table: "Athlete");

            migrationBuilder.DropIndex(
                name: "IX_Athlete_MemberID",
                table: "Athlete");

            migrationBuilder.DropColumn(
                name: "MemberID",
                table: "Athlete");

            migrationBuilder.CreateIndex(
                name: "IX_ClubMember_AthleteID",
                table: "ClubMember",
                column: "AthleteID");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubMember_Athlete_AthleteID",
                table: "ClubMember",
                column: "AthleteID",
                principalTable: "Athlete",
                principalColumn: "AthleteID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubMember_Athlete_AthleteID",
                table: "ClubMember");

            migrationBuilder.DropIndex(
                name: "IX_ClubMember_AthleteID",
                table: "ClubMember");

            migrationBuilder.AddColumn<int>(
                name: "MemberID",
                table: "Athlete",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Athlete_MemberID",
                table: "Athlete",
                column: "MemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Athlete_ClubMember_MemberID",
                table: "Athlete",
                column: "MemberID",
                principalTable: "ClubMember",
                principalColumn: "MemberID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
