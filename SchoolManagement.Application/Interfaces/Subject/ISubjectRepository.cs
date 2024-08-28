using SchoolManagement.Domain.Models;

namespace SchoolManagement.Domain.Interfaces;
public interface ISubjectRepository
{
    Task<SubjectEntity> Get(int id);
    Task<List<SubjectEntity>> Get();
    Task Create(SubjectEntity subject);
    Task Update(SubjectEntity subject);
    Task Delete(int id);
}