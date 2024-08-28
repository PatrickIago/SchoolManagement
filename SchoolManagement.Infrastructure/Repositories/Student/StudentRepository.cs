using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Domain.Models;

namespace SchoolManagement.Infrastructure.Data.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly SchoolManagementDbContext _context;

    public StudentRepository(SchoolManagementDbContext context)
    {
        _context = context;
    }

    public async Task<StudentEntity> Get(int id)
    {
        return await _context.Students
            .Include(s => s.Subjects)
            .FirstOrDefaultAsync(s => s.StudentId == id);
    }

    public async Task<List<StudentEntity>> Get()
    {
        return await _context.Students
            .Include(s => s.Subjects)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task Create(StudentEntity student)
    {
        await _context.Students.AddAsync(student);
    }

    public async Task Update(StudentEntity student)
    {
        var entity = await _context.Students
            .Include(s => s.Subjects)
            .FirstOrDefaultAsync(s => s.StudentId == student.StudentId);

        if (entity != null)
        {
            entity.Name = student.Name;
            entity.Age = student.Age;

            entity.Subjects = student.Subjects;

            _context.Students.Update(entity);
        }
    }

    public async Task Delete(int id)
    {
        var entity = await _context.Students
            .Include(s => s.Subjects)
            .FirstOrDefaultAsync(s => s.StudentId == id);

        if (entity != null)
        {
            _context.Students.Remove(entity);
        }
    }
}