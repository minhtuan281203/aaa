using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectSem3.Models;

public partial class AbcCvContext : DbContext
{
    public AbcCvContext()
    {
    }

    public AbcCvContext(DbContextOptions<AbcCvContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<CandidateList> CandidateLists { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Interview> Interviews { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-FOVSN3D\\SQLEXPRESS;Database=abcCV;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC27513CAE23");

            entity.ToTable("Account");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(255);
        });

        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Candidat__3214EC2775A18902");

            entity.ToTable("Candidate");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Fullname).HasMaxLength(255);
            entity.Property(e => e.Graduate).HasMaxLength(255);
            entity.Property(e => e.Literacy).HasMaxLength(255);
            entity.Property(e => e.Majors).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Vacancy).HasMaxLength(255);

            entity.HasOne(d => d.Account).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Candidate__Accou__398D8EEE");
        });

        modelBuilder.Entity<CandidateList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Candidat__3214EC2768185F00");

            entity.ToTable("Candidate_list");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CanId).HasColumnName("can_id");
            entity.Property(e => e.InterviewId).HasColumnName("Interview_id");
            entity.Property(e => e.JobId).HasColumnName("job_id");

            entity.HasOne(d => d.Can).WithMany(p => p.CandidateLists)
                .HasForeignKey(d => d.CanId)
                .HasConstraintName("FK__Candidate__can_i__46E78A0C");

            entity.HasOne(d => d.Interview).WithMany(p => p.CandidateLists)
                .HasForeignKey(d => d.InterviewId)
                .HasConstraintName("FK__Candidate__Inter__47DBAE45");

            entity.HasOne(d => d.Job).WithMany(p => p.CandidateLists)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK__Candidate__job_i__45F365D3");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC2775695532");

            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Company__3214EC2785F75863");

            entity.ToTable("Company");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Interview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Intervie__3214EC270814A06A");

            entity.ToTable("Interview");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InterDate)
                .HasColumnType("date")
                .HasColumnName("Inter_Date");
            entity.Property(e => e.Static).HasMaxLength(255);
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Job__3214EC27E8E9D3A6");

            entity.ToTable("Job");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CatId).HasColumnName("CatID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.DateCreate)
                .HasColumnType("date")
                .HasColumnName("Date_create");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.JobDescription).HasColumnName("Job_description");
            entity.Property(e => e.Modification).HasColumnType("date");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Vacancy).HasMaxLength(255);

            entity.HasOne(d => d.Cat).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.CatId)
                .HasConstraintName("FK__Job__CatID__4222D4EF");

            entity.HasOne(d => d.Company).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Job__CompanyID__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
