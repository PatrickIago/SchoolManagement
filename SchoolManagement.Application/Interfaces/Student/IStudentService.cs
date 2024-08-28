using SchoolManagement.Application.Models;
namespace SchoolManagement.Application.Interfaces;
public interface IStudentService
{
    Task<StudentDto> Get(int id);
    Task<List<StudentDto>> Get();
    Task Create(StudentDto studentDto);
    Task Update(StudentDto studentDto);
    Task Delete(int id);
}