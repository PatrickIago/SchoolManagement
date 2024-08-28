namespace SchoolManagement.Application.Models;
public class TeacherDto
{
    public int TeacherId { get; set; }
    public string Name { get; set; }
    public ICollection<SubjectDto>? Subjects { get; set; }
}