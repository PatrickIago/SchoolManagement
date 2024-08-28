using SchoolManagement.Application.Models;
public interface ITeacherService
{
    Task<TeacherDto> Get(int id);
    Task<List<TeacherDto>> Get();
    Task Create(TeacherDto teacherDto);
    Task Update(TeacherDto teacherDto);
    Task Delete(int id);
}