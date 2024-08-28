using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Domain.Models;
public class SubjectEntity
{
    [Key]
    public int SubjectId { get; set; }
    public string Name { get; set; }

    public ICollection<StudentEntity> Students { get; set; }
    public ICollection<TeacherEntity> Teachers { get; set; }

    public SubjectEntity() { }
    public SubjectEntity(string name) => Name = name;
    public SubjectEntity(int id, string name)
    {
        SubjectId = id;
        Name = name;
    }
}