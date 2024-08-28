using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Models;
using SchoolManagement.Models;
public class SchoolManagementDbContext : IdentityDbContext<ApplicationUser>
{
    public SchoolManagementDbContext(DbContextOptions<SchoolManagementDbContext> options) : base(options) { }

    public DbSet<SubjectEntity> Subjects { get; set; }
    public DbSet<TeacherEntity> Teachers { get; set; }
    public DbSet<StudentEntity> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TeacherEntity>()
            .HasMany(t => t.Subjects)
            .WithMany(s => s.Teachers)
            .UsingEntity<Dictionary<string, object>>(
                "TeacherSubject",
                j => j
                    .HasOne<SubjectEntity>()
                    .WithMany()
                    .HasForeignKey("SubjectId")
                    .OnDelete(DeleteBehavior.Restrict),
                j => j
                    .HasOne<TeacherEntity>()
                    .WithMany()
                    .HasForeignKey("TeacherId")
                    .OnDelete(DeleteBehavior.Restrict));

        modelBuilder.Entity<StudentEntity>()
            .HasMany(s => s.Subjects)
            .WithMany(s => s.Students)
            .UsingEntity<Dictionary<string, object>>(
                "StudentSubject",
                j => j
                    .HasOne<SubjectEntity>()
                    .WithMany()
                    .HasForeignKey("SubjectId")
                    .OnDelete(DeleteBehavior.Restrict),
                j => j
                    .HasOne<StudentEntity>()
                    .WithMany()
                    .HasForeignKey("StudentId")
                    .OnDelete(DeleteBehavior.Restrict));
    }
}