using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using iBalekaService.Data.Configurations;

namespace iBalekaService.Data.Migrations
{
    [DbContext(typeof(IBalekaContext))]
    partial class IBalekaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("iBalekaService.Domain.Models.Athlete", b =>
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

            modelBuilder.Entity("iBalekaService.Domain.Models.Checkpoint", b =>
                {
                    b.Property<int>("CheckpointID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<int?>("RouteID");

                    b.HasKey("CheckpointID");

                    b.HasIndex("RouteID");

                    b.ToTable("Checkpoint");
                });

            modelBuilder.Entity("iBalekaService.Domain.Models.Club", b =>
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

            modelBuilder.Entity("iBalekaService.Domain.Models.Club_Athlete", b =>
                {
                    b.Property<int>("MemberID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AthleteID");

                    b.Property<int>("ClubID");

                    b.Property<DateTime>("DateJoined");

                    b.Property<DateTime?>("DateLeft");

                    b.Property<bool>("IsaMember");

                    b.HasKey("MemberID");

                    b.HasIndex("AthleteID")
                        .IsUnique();

                    b.HasIndex("ClubID");

                    b.ToTable("ClubMember");
                });

            modelBuilder.Entity("iBalekaService.Domain.Models.Event", b =>
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

            modelBuilder.Entity("iBalekaService.Domain.Models.Event_Route", b =>
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

                    b.ToTable("EventRoute");
                });

            modelBuilder.Entity("iBalekaService.Domain.Models.EventRegistration", b =>
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

            modelBuilder.Entity("iBalekaService.Domain.Models.Rating", b =>
                {
                    b.Property<int>("RatingID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<DateTime>("DateAdded");

                    b.Property<bool>("Deleted");

                    b.Property<int?>("RouteID");

                    b.Property<int>("RunID");

                    b.Property<int>("Value");

                    b.HasKey("RatingID");

                    b.HasIndex("RouteID");

                    b.HasIndex("RunID")
                        .IsUnique();

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("iBalekaService.Domain.Models.Route", b =>
                {
                    b.Property<int>("RouteID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateRecorded");

                    b.Property<bool>("Deleted");

                    b.Property<double>("Distance");

                    b.Property<string>("MapImage");

                    b.HasKey("RouteID");

                    b.ToTable("Route");
                });

            modelBuilder.Entity("iBalekaService.Domain.Models.Run", b =>
                {
                    b.Property<int>("RunID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AthleteID");

                    b.Property<double>("CaloriesBurnt");

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

            modelBuilder.Entity("iBalekaService.Domain.Models.User", b =>
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

            modelBuilder.Entity("iBalekaService.Domain.Models.Checkpoint", b =>
                {
                    b.HasOne("iBalekaService.Domain.Models.Route", "Route")
                        .WithMany("Checkpoints")
                        .HasForeignKey("RouteID");
                });

            modelBuilder.Entity("iBalekaService.Domain.Models.Club", b =>
                {
                    b.HasOne("iBalekaService.Domain.Models.User", "User")
                        .WithMany("Clubs")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaService.Domain.Models.Club_Athlete", b =>
                {
                    b.HasOne("iBalekaService.Domain.Models.Athlete", "Athlete")
                        .WithOne("Club")
                        .HasForeignKey("iBalekaService.Domain.Models.Club_Athlete", "AthleteID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("iBalekaService.Domain.Models.Club", "Club")
                        .WithMany("Members")
                        .HasForeignKey("ClubID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaService.Domain.Models.Event", b =>
                {
                    b.HasOne("iBalekaService.Domain.Models.Club", "Club")
                        .WithMany("Events")
                        .HasForeignKey("ClubID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaService.Domain.Models.Event_Route", b =>
                {
                    b.HasOne("iBalekaService.Domain.Models.Event", "Event")
                        .WithMany("EventRoutes")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("iBalekaService.Domain.Models.Route", "Route")
                        .WithMany("EventRoutes")
                        .HasForeignKey("RouteID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaService.Domain.Models.EventRegistration", b =>
                {
                    b.HasOne("iBalekaService.Domain.Models.Athlete", "Athlete")
                        .WithMany("EventsRegistered")
                        .HasForeignKey("AthleteID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("iBalekaService.Domain.Models.Event", "Event")
                        .WithMany("Participants")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaService.Domain.Models.Rating", b =>
                {
                    b.HasOne("iBalekaService.Domain.Models.Route")
                        .WithMany("Ratings")
                        .HasForeignKey("RouteID");

                    b.HasOne("iBalekaService.Domain.Models.Run", "Run")
                        .WithOne("Rating")
                        .HasForeignKey("iBalekaService.Domain.Models.Rating", "RunID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("iBalekaService.Domain.Models.Run", b =>
                {
                    b.HasOne("iBalekaService.Domain.Models.Athlete", "Athlete")
                        .WithMany("Runs")
                        .HasForeignKey("AthleteID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("iBalekaService.Domain.Models.Event", "Event")
                        .WithMany("Runs")
                        .HasForeignKey("EventID");

                    b.HasOne("iBalekaService.Domain.Models.Route", "Route")
                        .WithMany("Runs")
                        .HasForeignKey("RouteID");
                });
        }
    }
}
