using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Interfaces;
public interface ISubjectService
{
    Task<SubjectDto> Get(int id);
    Task<List<SubjectDto>> Get();
    Task Create(SubjectDto subjectDto);
    Task Update(SubjectDto subjectDto);
    Task Delete(int id);
}