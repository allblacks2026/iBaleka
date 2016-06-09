using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iBalekaService.Migrations
{
    public partial class ClubRelationshipFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubMember_Athlete_AthleteID",
                table: "ClubMember");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubMember_Club_ClubID",
                table: "ClubMember");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Route_RouteID",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClubMember",
                table: "ClubMember");

            migrationBuilder.DropIndex(
                name: "IX_ClubMember_AthleteID",
                table: "ClubMember");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "User",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "User",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "RouteID",
                table: "Rating",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RunID",
                table: "Rating",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rating_RunID",
                table: "Rating",
                column: "RunID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClubMember",
                table: "ClubMember",
                column: "MemberID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ClubMember_Club_ClubID",
                table: "ClubMember",
                column: "ClubID",
                principalTable: "Club",
                principalColumn: "ClubID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Route_RouteID",
                table: "Rating",
                column: "RouteID",
                principalTable: "Route",
                principalColumn: "RouteID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Run_RunID",
                table: "Rating",
                column: "RunID",
                principalTable: "Run",
                principalColumn: "RunID",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Route_RouteID",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Run_RunID",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_RunID",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClubMember",
                table: "ClubMember");

            migrationBuilder.DropIndex(
                name: "IX_Athlete_MemberID",
                table: "Athlete");

            migrationBuilder.DropColumn(
                name: "RunID",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "MemberID",
                table: "Athlete");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "User",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "User",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RouteID",
                table: "Rating",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClubMember",
                table: "ClubMember",
                column: "MemberID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ClubMember_Club_ClubID",
                table: "ClubMember",
                column: "ClubID",
                principalTable: "Club",
                principalColumn: "ClubID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Route_RouteID",
                table: "Rating",
                column: "RouteID",
                principalTable: "Route",
                principalColumn: "RouteID",
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
