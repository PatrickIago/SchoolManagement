namespace SchoolManagement.Application.Models;
public class StudentDto
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public List<SubjectDto>? Subjects { get; set; }
}