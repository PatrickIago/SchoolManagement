using SchoolManagement.Application.Models;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Domain.Models;

namespace SchoolManagement.Infrastructure.Data.Services;
public class TeacherService : ITeacherService
{
    private readonly IUnitOfWork _unitOfWork;

    public TeacherService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TeacherDto> Get(int id)
    {
        var teacherEntity = await _unitOfWork.Teachers.Get(id);

        if (teacherEntity == null) return null;

        return new TeacherDto
        {
            TeacherId = teacherEntity.TeacherId,
            Name = teacherEntity.Name,
            Subjects = teacherEntity.Subjects.Select(s => new SubjectDto
            {
                SubjectId = s.SubjectId,
                Name = s.Name
            }).ToList(),
        };
    }

    public async Task<List<TeacherDto>> Get()
    {
        var teacherEntities = await _unitOfWork.Teachers.Get();
        return teacherEntities
            .Select(t => new TeacherDto
            {
                TeacherId = t.TeacherId,
                Name = t.Name,
                Subjects = t.Subjects.Select(s => new SubjectDto
                {
                    SubjectId = s.SubjectId,
                    Name = s.Name
                }).ToList()
            }).ToList();
    }

    public async Task Create(TeacherDto teacher)
    {
        var teacherEntity = new TeacherEntity
        {
            Name = teacher.Name,
            Subjects = teacher.Subjects != null && teacher.Subjects.Any()
                ? teacher.Subjects.Select(s => new SubjectEntity
                {
                    SubjectId = s.SubjectId,
                    Name = s.Name
                }).ToList()
                : null // Deixa como nula se não houver disciplinas
        };

        await _unitOfWork.Teachers.Create(teacherEntity);
        await _unitOfWork.CompleteAsync();

        teacher.TeacherId = teacherEntity.TeacherId;
    }

    public async Task Update(TeacherDto teacher)
    {
        var teacherEntity = new TeacherEntity
        {
            TeacherId = teacher.TeacherId,
            Name = teacher.Name,
            Subjects = teacher.Subjects.Select(t => new SubjectEntity
            {
                SubjectId = t.SubjectId,
                Name = t.Name,
            }).ToList(),
        };

        await _unitOfWork.Teachers.Update(teacherEntity);
        await _unitOfWork.CompleteAsync();

        teacher.TeacherId = teacherEntity.TeacherId;
    }

    public async Task Delete(int id)
    {
        await _unitOfWork.Teachers.Delete(id);
        await _unitOfWork.CompleteAsync();
    }
}