﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Laboratory_2.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20241106014314_ICD")]
    partial class ICD
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Consultation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CommentId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("SpecialtyId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("inspectionId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("Consultations");
                });

            modelBuilder.Entity("Diagnosis", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("InspectionId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InspectionId");

                    b.ToTable("Diagnosises");
                });

            modelBuilder.Entity("Doctor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Gen")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("Speciality")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Inspection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Anamnesis")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("BaseInspectionId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Complaints")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Conclusion")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DeathDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("NextVisitDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PreviousInspectionId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Treatment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Inspections");
                });

            modelBuilder.Entity("InspectionCommentModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifyTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("InspectionCommentModel");
                });

            modelBuilder.Entity("InspectionConsultationModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("CommentsNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("IncpectionId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("InspectionId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RootCommentId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("SpecialityId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("InspectionId");

                    b.HasIndex("RootCommentId");

                    b.HasIndex("SpecialityId");

                    b.ToTable("InspectionConsultationModel");
                });

            modelBuilder.Entity("Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Gen")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Speciality", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Specialties");
                });

            modelBuilder.Entity("Consultation", b =>
                {
                    b.HasOne("Comment", "Comment")
                        .WithMany()
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Speciality", "Specialty")
                        .WithMany()
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("Specialty");
                });

            modelBuilder.Entity("Diagnosis", b =>
                {
                    b.HasOne("Inspection", null)
                        .WithMany("Diagnoses")
                        .HasForeignKey("InspectionId");
                });

            modelBuilder.Entity("Inspection", b =>
                {
                    b.HasOne("Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("InspectionCommentModel", b =>
                {
                    b.HasOne("Doctor", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("InspectionConsultationModel", b =>
                {
                    b.HasOne("Inspection", null)
                        .WithMany("Consultation")
                        .HasForeignKey("InspectionId");

                    b.HasOne("InspectionCommentModel", "RootComment")
                        .WithMany()
                        .HasForeignKey("RootCommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Speciality", "Speciality")
                        .WithMany()
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RootComment");

                    b.Navigation("Speciality");
                });

            modelBuilder.Entity("Inspection", b =>
                {
                    b.Navigation("Consultation");

                    b.Navigation("Diagnoses");
                });
#pragma warning restore 612, 618
        }
    }
}
