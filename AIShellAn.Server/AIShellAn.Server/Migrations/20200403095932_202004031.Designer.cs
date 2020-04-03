﻿// <auto-generated />
using System;
using AIShellAn.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AIShellAn.Server.Migrations
{
    [DbContext(typeof(AIShellAnContext))]
    [Migration("20200403095932_202004031")]
    partial class _202004031
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("AIShellAn.Server.Entities.AnnotationResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnnotationResultType");

                    b.Property<Guid>("AnnotationTaskId");

                    b.Property<string>("Content")
                        .HasColumnType("jsonb");

                    b.Property<DateTime>("CreateOn");

                    b.Property<bool?>("Effective");

                    b.Property<Guid>("ItemId");

                    b.Property<float>("LoadingTime");

                    b.Property<float>("SpendTime");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("AnnotationResult");
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.AnnotationTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnnotationType");

                    b.Property<DateTime>("CreateOn");

                    b.Property<DateTime?>("FinshedTime");

                    b.Property<bool>("IsAnnotationTime");

                    b.Property<Guid>("ManagerId");

                    b.Property<string>("TaskCode")
                        .HasMaxLength(100);

                    b.Property<string>("TaskDescribe")
                        .HasMaxLength(5000);

                    b.Property<string>("TaskName")
                        .HasMaxLength(100);

                    b.Property<int>("TaskScope");

                    b.Property<int>("TaskStatus");

                    b.Property<int>("TaskType");

                    b.Property<Guid?>("TemplateId");

                    b.Property<int>("Urgency");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.HasIndex("TemplateId");

                    b.ToTable("AnnotationTask");
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.AnnotationTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateOn");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("AnnotationTemplate");
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.AnnotationTemplateItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AnnotationTemplateId");

                    b.Property<string>("Content")
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreateOn");

                    b.Property<string>("Default")
                        .HasMaxLength(200);

                    b.Property<int>("DisplayOrder");

                    b.Property<bool>("Effect");

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<bool>("Necessary");

                    b.Property<int>("TemplateType");

                    b.HasKey("Id");

                    b.HasIndex("AnnotationTemplateId");

                    b.ToTable("AnnotationTemplateItem");
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.DataPackage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AnnotationTaskId");

                    b.Property<Guid?>("AnnotationUserId");

                    b.Property<DateTime>("CreateOn");

                    b.Property<DateTime?>("FinishTime");

                    b.Property<Guid?>("InspectionUserId");

                    b.Property<string>("InspectionUser_Name");

                    b.Property<int>("Number");

                    b.Property<int>("PackageStatus");

                    b.Property<DateTime?>("RecevieTime");

                    b.Property<Guid?>("TeamId");

                    b.Property<string>("TeamName")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("AnnotationTaskId");

                    b.HasIndex("AnnotationUserId");

                    b.ToTable("DataPackage");
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.InspectionItemRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AnnotationResultId");

                    b.Property<Guid>("AnnotationTaskId");

                    b.Property<int>("ErrorType");

                    b.Property<Guid?>("InspectionPackageRecordId");

                    b.Property<int>("InspectionStatus");

                    b.Property<Guid>("InspectionTaskId");

                    b.Property<DateTime?>("InspectionTime");

                    b.Property<Guid?>("InspectionUserId");

                    b.Property<Guid>("ItemId");

                    b.Property<float>("SpendTime");

                    b.Property<string>("Suggestion")
                        .HasMaxLength(1000);

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("AnnotationResultId");

                    b.HasIndex("AnnotationTaskId");

                    b.HasIndex("InspectionPackageRecordId");

                    b.ToTable("InspectionItemRecord");
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.InspectionPackageRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AnnotationTaskId");

                    b.Property<DateTime?>("FinishTime");

                    b.Property<int>("InspectionPackageStatus");

                    b.Property<Guid>("InspectionTaskId");

                    b.Property<Guid?>("InspectionUserId");

                    b.Property<string>("InspectionUserName");

                    b.Property<Guid>("PackageId");

                    b.Property<int>("PackageNumber");

                    b.Property<Guid?>("TeamId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("InspectionTaskId");

                    b.HasIndex("PackageId");

                    b.ToTable("InspectionPackageRecord");
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.InspectionTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AnnotationTaskId");

                    b.Property<string>("AnnotationUserIds");

                    b.Property<DateTime>("CreateOn");

                    b.Property<Guid>("CreatorId");

                    b.Property<int?>("DrawPackageCount");

                    b.Property<int?>("DrawPackagePercent");

                    b.Property<DateTime?>("FinishTime");

                    b.Property<int>("InspectionTaskStatus");

                    b.Property<DateTime?>("InspectionTimeRangeEnd");

                    b.Property<DateTime?>("InspectionTimeRangeStart");

                    b.Property<int>("InspectionType");

                    b.Property<string>("InspectionUserIds");

                    b.Property<string>("ResultContent")
                        .HasMaxLength(1000);

                    b.Property<string>("TaskName")
                        .HasMaxLength(50);

                    b.Property<int>("Urgency");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("AnnotationTaskId");

                    b.HasIndex("CreatorId");

                    b.ToTable("InspectionTask");
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.LongSpeechItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AnnotationTaskId");

                    b.Property<DateTime?>("AnnotationTime");

                    b.Property<Guid?>("AnnotationUserId");

                    b.Property<DateTime>("CreateOn");

                    b.Property<float>("Duration");

                    b.Property<int>("ItemStatus");

                    b.Property<int>("Number");

                    b.Property<Guid>("PackageId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("AnnotationUserId");

                    b.HasIndex("PackageId");

                    b.ToTable("LongSpeechItem");
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.LongSpeechText", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("LongSpeechItemId");

                    b.Property<int>("Order");

                    b.Property<string>("Text")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("LongSpeechItemId");

                    b.ToTable("LongSpeechText");
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.ShortSpeechItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AnnotationTaskId");

                    b.Property<DateTime?>("AnnotationTime");

                    b.Property<Guid?>("AnnotationUserId");

                    b.Property<DateTime>("CreateOn");

                    b.Property<float>("Duration");

                    b.Property<int>("ItemStatus");

                    b.Property<int>("Number");

                    b.Property<Guid>("PackageId");

                    b.Property<string>("Url");

                    b.Property<string>("Urtexte");

                    b.HasKey("Id");

                    b.HasIndex("AnnotationUserId");

                    b.HasIndex("PackageId");

                    b.ToTable("ShortSpeechItem");
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateOn");

                    b.Property<Guid>("CreatorId");

                    b.Property<string>("Describe")
                        .HasMaxLength(1000);

                    b.Property<string>("TeamName")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.TeamAnnotationTask", b =>
                {
                    b.Property<Guid>("TeamId");

                    b.Property<Guid>("AnnotationTaskId");

                    b.Property<DateTime>("CreatedOn");

                    b.HasKey("TeamId", "AnnotationTaskId");

                    b.ToTable("TeamTask");
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.TeamUser", b =>
                {
                    b.Property<Guid>("TeamId");

                    b.Property<Guid>("UserId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsManager");

                    b.HasKey("TeamId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("TeamUser");
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTime?>("Birthday");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<Guid?>("CreatorId");

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<string>("LastLoginIP");

                    b.Property<DateTime?>("LastLoginTime");

                    b.Property<string>("NativePlace")
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .HasMaxLength(20);

                    b.Property<string>("RealName")
                        .HasMaxLength(20);

                    b.Property<string>("Role")
                        .HasMaxLength(50);

                    b.Property<int>("Sex");

                    b.Property<string>("UserName")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("640d9acf-5197-44f3-8f94-f08437ed9179"),
                            Active = true,
                            CreatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "7fef6171469e80d32c0559f88b377245",
                            RealName = "系统管理员",
                            Role = "系统管理员",
                            Sex = 0,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.AnnotationTask", b =>
                {
                    b.HasOne("AIShellAn.Server.Entities.User", "Manager")
                        .WithMany("AnnotationTasks")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AIShellAn.Server.Entities.AnnotationTemplate", "AnnotationTemplate")
                        .WithMany("AnnotationTasks")
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.AnnotationTemplateItem", b =>
                {
                    b.HasOne("AIShellAn.Server.Entities.AnnotationTemplate", "AnnotationTemplate")
                        .WithMany("TemplateItems")
                        .HasForeignKey("AnnotationTemplateId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.DataPackage", b =>
                {
                    b.HasOne("AIShellAn.Server.Entities.AnnotationTask", "AnnotationTask")
                        .WithMany("Packages")
                        .HasForeignKey("AnnotationTaskId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AIShellAn.Server.Entities.User", "AnnotationUser")
                        .WithMany("DataPackages")
                        .HasForeignKey("AnnotationUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.InspectionItemRecord", b =>
                {
                    b.HasOne("AIShellAn.Server.Entities.AnnotationResult", "AnnotationResult")
                        .WithMany()
                        .HasForeignKey("AnnotationResultId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AIShellAn.Server.Entities.AnnotationTask", "AnnotationTask")
                        .WithMany()
                        .HasForeignKey("AnnotationTaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AIShellAn.Server.Entities.InspectionPackageRecord", "InspectionPackageRecord")
                        .WithMany("InspectionItemRecords")
                        .HasForeignKey("InspectionPackageRecordId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AIShellAn.Server.Entities.User", "InspectionUser")
                        .WithMany("InspectionItemRecords")
                        .HasForeignKey("InspectionPackageRecordId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.InspectionPackageRecord", b =>
                {
                    b.HasOne("AIShellAn.Server.Entities.InspectionTask", "InspectionTask")
                        .WithMany("InspectionPackageRecords")
                        .HasForeignKey("InspectionTaskId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AIShellAn.Server.Entities.DataPackage", "Package")
                        .WithMany("InspectionPackageRecords")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.InspectionTask", b =>
                {
                    b.HasOne("AIShellAn.Server.Entities.AnnotationTask", "AnnotationTask")
                        .WithMany("InspectionTasks")
                        .HasForeignKey("AnnotationTaskId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AIShellAn.Server.Entities.User", "Creator")
                        .WithMany("InspectionTasks")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.LongSpeechItem", b =>
                {
                    b.HasOne("AIShellAn.Server.Entities.User", "AnnotationUser")
                        .WithMany()
                        .HasForeignKey("AnnotationUserId");

                    b.HasOne("AIShellAn.Server.Entities.DataPackage", "Package")
                        .WithMany("LongSpeechItems")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.LongSpeechText", b =>
                {
                    b.HasOne("AIShellAn.Server.Entities.LongSpeechItem", "LongSpeechItem")
                        .WithMany("Texts")
                        .HasForeignKey("LongSpeechItemId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.ShortSpeechItem", b =>
                {
                    b.HasOne("AIShellAn.Server.Entities.User", "AnnotationUser")
                        .WithMany()
                        .HasForeignKey("AnnotationUserId");

                    b.HasOne("AIShellAn.Server.Entities.DataPackage", "Package")
                        .WithMany("ShortSpeechItems")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.Team", b =>
                {
                    b.HasOne("AIShellAn.Server.Entities.User", "Creator")
                        .WithMany("Teams")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.TeamAnnotationTask", b =>
                {
                    b.HasOne("AIShellAn.Server.Entities.AnnotationTask", "AnnotationTask")
                        .WithMany("TeamTask")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AIShellAn.Server.Entities.Team", "Team")
                        .WithMany("TeamTask")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.TeamUser", b =>
                {
                    b.HasOne("AIShellAn.Server.Entities.Team", "Team")
                        .WithMany("TeamUser")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AIShellAn.Server.Entities.User", "User")
                        .WithMany("TeamUser")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AIShellAn.Server.Entities.User", b =>
                {
                    b.HasOne("AIShellAn.Server.Entities.User", "Creator")
                        .WithMany("Members")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
