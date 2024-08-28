using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Domain.Models;

public class TeacherEntity
{
    [Key]
    public int TeacherId { get; set; }
    public string Name { get; set; }

    public ICollection<SubjectEntity>? Subjects { get; set; }
}