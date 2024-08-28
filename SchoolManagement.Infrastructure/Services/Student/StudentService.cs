using SchoolManagement.Application.Interfaces;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Domain.Models;

namespace SchoolManagement.Infrastructure.Data.Services;
public class StudentService : IStudentService
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<StudentDto> Get(int id)
    {
        var studentEntity = await _unitOfWork.Students.Get(id);

        if (studentEntity == null) return null;

        return new StudentDto
        {
            StudentId = studentEntity.StudentId,
            Name = studentEntity.Name,
            Age = studentEntity.Age,
            Subjects = studentEntity.Subjects.Select(s => new SubjectDto
            {
                SubjectId = s.SubjectId,
                Name = s.Name
            }).ToList()
        };
    }

    public async Task<List<StudentDto>> Get()
    {
        var studentEntities = await _unitOfWork.Students.Get();
        return studentEntities
            .Select(s => new StudentDto
            {
                StudentId = s.StudentId,
                Name = s.Name,
                Age = s.Age,
                Subjects = s.Subjects.Select(sub => new SubjectDto
                {
                    SubjectId = sub.SubjectId,
                    Name = sub.Name
                }).ToList()
            }).ToList();
    }

    public async Task Create(StudentDto studentDto)
    {
        var studentEntity = new StudentEntity
        {
            Name = studentDto.Name,
            Age = studentDto.Age,
            Subjects = studentDto.Subjects != null && studentDto.Subjects.Any()
                ? studentDto.Subjects.Select(s => new SubjectEntity
                {
                    SubjectId = s.SubjectId,
                    Name = s.Name
                }).ToList()
                : null 
        };

        await _unitOfWork.Students.Create(studentEntity);
        await _unitOfWork.CompleteAsync();
       
        studentDto.StudentId = studentEntity.StudentId;
    }

    public async Task Update(StudentDto student)
    {
        var studentEntity = new StudentEntity
        {
            StudentId = student.StudentId,
            Name = student.Name,
            Age = student.Age,
            Subjects = student.Subjects.Select(s => new SubjectEntity
            {
                SubjectId = s.SubjectId,
                Name = s.Name
            }).ToList()
        };
        await _unitOfWork.Students.Update(studentEntity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task Delete(int id)
    {
        await _unitOfWork.Students.Delete(id);
        await _unitOfWork.CompleteAsync();
    }
}