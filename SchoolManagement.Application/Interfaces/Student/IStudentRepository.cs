using SchoolManagement.Domain.Models;
namespace SchoolManagement.Domain.Interfaces;
public interface IStudentRepository
{
    Task<StudentEntity> Get(int id);
    Task<List<StudentEntity>> Get();
    Task Create(StudentEntity student);
    Task Update(StudentEntity student);
    Task Delete(int id);
}