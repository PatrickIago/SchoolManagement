using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Domain.Models;

public class StudentEntity
{
    [Key]
    public int StudentId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }

    public ICollection<SubjectEntity>? Subjects { get; set; }

    public StudentEntity() { }
    public StudentEntity(int id) => StudentId = id;
    public StudentEntity(string name, int age)
    {
        Name = name;
        Age = age;
    }
    public StudentEntity(int id, string name, int age)
    {
        StudentId = id;
        Name = name;
        Age = age;
    }
}