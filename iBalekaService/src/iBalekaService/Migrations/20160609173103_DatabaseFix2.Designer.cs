using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using iBalekaService.Models;

namespace iBalekaService.Migrations
{
    [DbContext(typeof(IBalekaContext))]
    [Migration("20160609173103_DatabaseFix2")]
    partial class DatabaseFix2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("iBalekaService.Models.Athlete", b =>
                {
                    b.Property<int>("AthleteID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateJoined");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Firstname");

                    b.Property<char?>("Gender");

                    b.Property<double?>("Height");

                    b.Property<string>("LicenseNo");

                    b.Property<string>("Surname");

                    b.Property<double?>("Weight");

                    b.HasKey("AthleteID");

                    b.ToTable("Athlete");
                });

            modelBuilder.Entity("iBalekaService.Models.Checkpoint", b =>
                {
                    b.Property<int>("CheckpointID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted");

                    b.Property<string>("Latitude");

                    b.Property<string>("Longitude");

                    b.Property<int?>("RouteID");

                    b.HasKey("CheckpointID");

                    b.HasIndex("RouteID");

                    b.ToTable("Checkpoint");
                });

            modelBuilder.Entity("iBalekaService.Models.Club", b =>
                {
                    b.Property<int>("ClubID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<int>("UserID");

                    b.HasKey("ClubID");

                    b.HasIndex("UserID");

                    b.ToTable("Club");
                });

            modelBuilder.Entity("iBalekaService.Models.Club_Athlete", b =>
                {
                    b.Property<int>("MemberID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AthleteID");

                    b.Property<int>("ClubID");

                    b.Property<DateTime>("DateJoined");

                    b.Property<DateTime?>("DateLeft");

                    b.Property<bool>("IsaMember");

                    b.HasKey("MemberID");

                    b.HasIndex("AthleteID");

                    b.HasIndex("ClubID");

                    b.ToTable("ClubMember");
                });

            modelBuilder.Entity("iBalekaService.Models.Event", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClubID");

                    b.Property<DateTime>("DateAndTime");

                    b.Property<DateTime>("DateCreated");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<string>("Location");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("EventID");

                    b.HasIndex("ClubID");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("iBalekaService.Models.Event_Route", b =>
                {
                    b.Property<int>("EventRouteID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<int>("EventID");

                    b.Property<int>("RouteID");

                    b.HasKey("EventRouteID");

                    b.HasIndex("EventID");

                    b.HasIndex("RouteID");

                    b.ToTable("Event_Route");
                });

            modelBuilder.Entity("iBalekaService.Models.EventRegistration", b =>
                {
                    b.Property<int>("RegistrationID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Arrived");

                    b.Property<int>("AthleteID");

                    b.Property<DateTime>("DateRegistered");

                    b.Property<bool>("Deleted");

                    b.Property<int>("EventID");

                    b.Property<int>("SelectedRoute");

                    b.HasKey("RegistrationID");

                    b.HasIndex("AthleteID");

                    b.HasIndex("EventID");

                    b.ToTable("EventRegistration");
                });

            modelBuilder.Entity("iBalekaService.Models.Rating", b =>
                {
                    b.Property<int>("RatingID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<DateTime>("DateAdded");

                    b.Property<DateTime>("DateModified");

                    b.Property<bool>("Deleted");

                    b.Property<int?>("RouteID");

                    b.Property<int>("RunID");

                    b.Property<int>("Value");

                    b.HasKey("RatingID");

                    b.HasIndex("RouteID");

                    b.HasIndex("RunID");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("iBalekaService.Models.Route", b =>
                {
                    b.Property<int>("RouteID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateRecorded");

                    b.Property<bool>("Deleted");

                    b.Property<int>("Distance");

                    b.Property<string>("MapImage");

                    b.HasKey("RouteID");

                    b.ToTable("Route");
                });

            modelBuilder.Entity("iBalekaService.Models.Run", b =>
                {
                    b.Property<int>("RunID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AthleteID");

                    b.Property<string>("CaloriesBurnt");

                    b.Property<DateTime>("DateRecorded");

                    b.Property<bool>("Deleted");

                    b.Property<DateTime>("EndTime");

                    b.Property<int?>("EventID");

                    b.Property<int?>("RouteID");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("RunID");

                    b.HasIndex("AthleteID");

                    b.HasIndex("EventID");

                    b.HasIndex("RouteID");

                    b.ToTable("Run");
                });

            modelBuilder.Entity("iBalekaService.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<DateTime>("DateJoined");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.HasKey("UserID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("iBalekaService.Models.Checkpoint", b =>
                {
                    b.HasOne("iBalekaService.Models.Route")
                        .WithMany()
                        .HasForeignKey("RouteID");
                });

            modelBuilder.Entity("iBalekaService.Models.Club", b =>
                {
                    b.HasOne("iBalekaService.Models.User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaService.Models.Club_Athlete", b =>
                {
                    b.HasOne("iBalekaService.Models.Athlete")
                        .WithOne()
                        .HasForeignKey("iBalekaService.Models.Club_Athlete", "AthleteID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("iBalekaService.Models.Club")
                        .WithMany()
                        .HasForeignKey("ClubID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaService.Models.Event", b =>
                {
                    b.HasOne("iBalekaService.Models.Club")
                        .WithMany()
                        .HasForeignKey("ClubID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaService.Models.Event_Route", b =>
                {
                    b.HasOne("iBalekaService.Models.Event")
                        .WithMany()
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("iBalekaService.Models.Route")
                        .WithMany()
                        .HasForeignKey("RouteID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaService.Models.EventRegistration", b =>
                {
                    b.HasOne("iBalekaService.Models.Athlete")
                        .WithMany()
                        .HasForeignKey("AthleteID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("iBalekaService.Models.Event")
                        .WithMany()
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaService.Models.Rating", b =>
                {
                    b.HasOne("iBalekaService.Models.Route")
                        .WithMany()
                        .HasForeignKey("RouteID");

                    b.HasOne("iBalekaService.Models.Run")
                        .WithOne()
                        .HasForeignKey("iBalekaService.Models.Rating", "RunID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaService.Models.Run", b =>
                {
                    b.HasOne("iBalekaService.Models.Athlete")
                        .WithMany()
                        .HasForeignKey("AthleteID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("iBalekaService.Models.Event")
                        .WithMany()
                        .HasForeignKey("EventID");

                    b.HasOne("iBalekaService.Models.Route")
                        .WithMany()
                        .HasForeignKey("RouteID");
                });
        }
    }
}
