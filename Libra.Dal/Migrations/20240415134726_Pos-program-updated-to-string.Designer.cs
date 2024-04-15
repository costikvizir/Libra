﻿// <auto-generated />
using System;
using Libra.Dal.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Libra.Dal.Migrations
{
    [DbContext(typeof(LibraContext))]
    [Migration("20240415134726_Pos-program-updated-to-string")]
    partial class Posprogramupdatedtostring
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Libra.Dal.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityName = "Chișinău"
                        },
                        new
                        {
                            Id = 2,
                            CityName = "Bălți"
                        },
                        new
                        {
                            Id = 3,
                            CityName = "Tiraspol"
                        },
                        new
                        {
                            Id = 4,
                            CityName = "Bender"
                        },
                        new
                        {
                            Id = 5,
                            CityName = "Cahul"
                        },
                        new
                        {
                            Id = 6,
                            CityName = "Comrat"
                        },
                        new
                        {
                            Id = 7,
                            CityName = "Orhei"
                        },
                        new
                        {
                            Id = 8,
                            CityName = "Ungheni"
                        },
                        new
                        {
                            Id = 9,
                            CityName = "Soroca"
                        },
                        new
                        {
                            Id = 10,
                            CityName = "Călărași"
                        });
                });

            modelBuilder.Entity("Libra.Dal.Entities.ConnectionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConnectType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ConnectionType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConnectType = "Remote"
                        },
                        new
                        {
                            Id = 2,
                            ConnectType = "Physical"
                        });
                });

            modelBuilder.Entity("Libra.Dal.Entities.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AssignedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("AssignedId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Memo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PosId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Priority")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProblemId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Solution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StatusId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("SubTypeId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("TypeId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("UserCreatedId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssignedId");

                    b.HasIndex("PosId");

                    b.HasIndex("ProblemId");

                    b.HasIndex("StatusId");

                    b.HasIndex("SubTypeId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserCreatedId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("Libra.Dal.Entities.IssueType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IssueLevel")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParrentIssue")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("IssueTypes");
                });

            modelBuilder.Entity("Libra.Dal.Entities.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Action")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IssueId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.HasIndex("UserId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Libra.Dal.Entities.Pos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AfternoonClosing")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AfternoonOpening")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cellphone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("ConnectionTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MorningClosing")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MorningOpening")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("ConnectionTypeId");

                    b.ToTable("Pos");
                });

            modelBuilder.Entity("Libra.Dal.Entities.PosWeekDay", b =>
                {
                    b.Property<int>("PosId")
                        .HasColumnType("int");

                    b.Property<int>("WeekDayId")
                        .HasColumnType("int");

                    b.HasKey("PosId", "WeekDayId");

                    b.HasIndex("WeekDayId");

                    b.ToTable("PosWeekDay");
                });

            modelBuilder.Entity("Libra.Dal.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IssueStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IssueStatus = "New"
                        },
                        new
                        {
                            Id = 2,
                            IssueStatus = "Assigned"
                        },
                        new
                        {
                            Id = 3,
                            IssueStatus = "In progress"
                        },
                        new
                        {
                            Id = 4,
                            IssueStatus = "Pending"
                        });
                });

            modelBuilder.Entity("Libra.Dal.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserTypeId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@gmail.com",
                            IsDeleted = false,
                            Login = "admin",
                            Name = "admin",
                            Password = "admin",
                            UserTypeId = 1
                        });
                });

            modelBuilder.Entity("Libra.Dal.Entities.UserType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Role = "Administrator"
                        },
                        new
                        {
                            Id = 2,
                            Role = "Technical Group"
                        },
                        new
                        {
                            Id = 3,
                            Role = "User"
                        });
                });

            modelBuilder.Entity("Libra.Dal.Entities.WeekDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WeekDays");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Day = "Mon"
                        },
                        new
                        {
                            Id = 2,
                            Day = "Tue"
                        },
                        new
                        {
                            Id = 3,
                            Day = "Wed"
                        },
                        new
                        {
                            Id = 4,
                            Day = "Thu"
                        },
                        new
                        {
                            Id = 5,
                            Day = "Fri"
                        },
                        new
                        {
                            Id = 6,
                            Day = "Sat"
                        },
                        new
                        {
                            Id = 7,
                            Day = "Sun"
                        });
                });

            modelBuilder.Entity("Libra.Dal.Entities.Issue", b =>
                {
                    b.HasOne("Libra.Dal.Entities.UserType", "UserType")
                        .WithMany("Issues")
                        .HasForeignKey("AssignedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Libra.Dal.Entities.Pos", "Pos")
                        .WithMany("Issues")
                        .HasForeignKey("PosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Libra.Dal.Entities.IssueType", "IssueProblem")
                        .WithMany("IssuesProblems")
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Libra.Dal.Entities.Status", "Status")
                        .WithMany("Issues")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Libra.Dal.Entities.IssueType", "IssueSubType")
                        .WithMany("IssueSubTypes")
                        .HasForeignKey("SubTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Libra.Dal.Entities.IssueType", "IssueType")
                        .WithMany("IssueTypes")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Libra.Dal.Entities.User", "User")
                        .WithMany("Issues")
                        .HasForeignKey("UserCreatedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Libra.Dal.Entities.Log", b =>
                {
                    b.HasOne("Libra.Dal.Entities.Issue", "Issue")
                        .WithMany("Logs")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Libra.Dal.Entities.User", "User")
                        .WithMany("Logs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Libra.Dal.Entities.Pos", b =>
                {
                    b.HasOne("Libra.Dal.Entities.City", "City")
                        .WithMany("PosList")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Libra.Dal.Entities.ConnectionType", "ConnectionType")
                        .WithMany("PosList")
                        .HasForeignKey("ConnectionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Libra.Dal.Entities.PosWeekDay", b =>
                {
                    b.HasOne("Libra.Dal.Entities.Pos", "Pos")
                        .WithMany("PosWeekDays")
                        .HasForeignKey("PosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Libra.Dal.Entities.WeekDay", "DayOfWeek")
                        .WithMany("PosWeekDays")
                        .HasForeignKey("WeekDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Libra.Dal.Entities.User", b =>
                {
                    b.HasOne("Libra.Dal.Entities.UserType", "UserType")
                        .WithMany("Users")
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
