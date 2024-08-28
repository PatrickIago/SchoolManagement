using SchoolManagement.Application.Interfaces;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Domain.Models;

namespace SchoolManagement.Infrastructure.Services.Subject;
public class SubjectService : ISubjectService
{
    private readonly IUnitOfWork _unitOfWork;

    public SubjectService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SubjectDto> Get(int id)
    {
        var subjectEntity = await _unitOfWork.Subjects.Get(id);

        if (subjectEntity == null) return null;

        return new SubjectDto
        {
            SubjectId = subjectEntity.SubjectId,
            Name = subjectEntity.Name
        };
    }

    public async Task<List<SubjectDto>> Get()
    {
        var subjectEntities = await _unitOfWork.Subjects.Get();
        return subjectEntities
            .Select(s => new SubjectDto
            {
                SubjectId = s.SubjectId,
                Name = s.Name
            }).ToList();
    }

    public async Task Create(SubjectDto subjectDto)
    {
        var subjectEntity = new SubjectEntity
        {
            Name = subjectDto.Name
        };

        await _unitOfWork.Subjects.Create(subjectEntity);
        await _unitOfWork.CompleteAsync();

        subjectDto.SubjectId = subjectEntity.SubjectId; ;
    }

    public async Task Update(SubjectDto subjectDto)
    {
        var subjectEntity = new SubjectEntity
        {
            SubjectId = subjectDto.SubjectId,
            Name = subjectDto.Name
        };

        await _unitOfWork.Subjects.Update(subjectEntity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task Delete(int id)
    {
        await _unitOfWork.Subjects.Delete(id);
        await _unitOfWork.CompleteAsync();
    }
}