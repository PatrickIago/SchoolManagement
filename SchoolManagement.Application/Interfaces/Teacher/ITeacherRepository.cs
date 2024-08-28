using SchoolManagement.Domain.Models;
namespace SchoolManagement.Domain.Interfaces;
public interface ITeacherRepository
{
    Task<TeacherEntity> Get(int id);
    Task<List<TeacherEntity>> Get();
    Task Create(TeacherEntity teacher);
    Task Update(TeacherEntity teacher);
    Task Delete(int id);
}