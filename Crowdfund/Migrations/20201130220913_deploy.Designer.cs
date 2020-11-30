﻿// <auto-generated />
using System;
using Crowdfund.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Crowdfund.Migrations
{
    [DbContext(typeof(CrowdfundDbContext))]
    [Migration("20201130220913_deploy")]
    partial class deploy
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Crowdfund.model.Backer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Backers");
                });

            modelBuilder.Entity("Crowdfund.model.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Payload")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("Crowdfund.model.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CurrentBudget")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjectCreatorId")
                        .HasColumnType("int");

                    b.Property<decimal>("TargetBudget")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectCreatorId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Crowdfund.model.ProjectCreator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProjectCreators");
                });

            modelBuilder.Entity("Crowdfund.model.RewardPackage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Reward")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("RewardPackages");
                });

            modelBuilder.Entity("Crowdfund.model.StatusUpdate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Overload")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("StatusUpdate");
                });

            modelBuilder.Entity("Crowdfund.model.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("BackerId")
                        .HasColumnType("int");

                    b.Property<int?>("RewardPackageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BackerId");

                    b.HasIndex("RewardPackageId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Crowdfund.model.TransactionPackage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("RewardPackageId")
                        .HasColumnType("int");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RewardPackageId");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionPackages");
                });

            modelBuilder.Entity("Crowdfund.model.Media", b =>
                {
                    b.HasOne("Crowdfund.model.Project", "Project")
                        .WithMany("Medias")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("Crowdfund.model.Project", b =>
                {
                    b.HasOne("Crowdfund.model.ProjectCreator", "ProjectCreator")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectCreatorId");
                });

            modelBuilder.Entity("Crowdfund.model.RewardPackage", b =>
                {
                    b.HasOne("Crowdfund.model.Project", "Project")
                        .WithMany("RewardPackages")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("Crowdfund.model.StatusUpdate", b =>
                {
                    b.HasOne("Crowdfund.model.Project", "Project")
                        .WithMany("StatusUpdates")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("Crowdfund.model.Transaction", b =>
                {
                    b.HasOne("Crowdfund.model.Backer", "Backer")
                        .WithMany("Transactions")
                        .HasForeignKey("BackerId");

                    b.HasOne("Crowdfund.model.RewardPackage", "RewardPackage")
                        .WithMany()
                        .HasForeignKey("RewardPackageId");
                });

            modelBuilder.Entity("Crowdfund.model.TransactionPackage", b =>
                {
                    b.HasOne("Crowdfund.model.RewardPackage", "RewardPackage")
                        .WithMany()
                        .HasForeignKey("RewardPackageId");

                    b.HasOne("Crowdfund.model.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId");
                });
#pragma warning restore 612, 618
        }
    }
}