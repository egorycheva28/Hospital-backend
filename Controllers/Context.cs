using System;
using Microsoft.EntityFrameworkCore;

public class Context: DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    { }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Consultation> Consultations { get; set; }
    public DbSet<Diagnosis> Diagnosises { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Inspection> Inspections { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Speciality> Specialties { get; set; }
    public DbSet<ICD10> ICD { get; set; }
    public DbSet<Token> Tokens { get; set; }
}
