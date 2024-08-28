using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Domain.Models;

namespace SchoolManagement.Infrastructure.Repositories.Teacher;
public class TeacherRepository : ITeacherRepository
{
    private readonly SchoolManagementDbContext _context;

    public TeacherRepository(SchoolManagementDbContext context)
    {
        _context = context;
    }

    public async Task<TeacherEntity> Get(int id)
    {
        return await _context.Teachers
            .Include(t => t.Subjects)
            .FirstOrDefaultAsync(t => t.TeacherId == id);
    }

    public async Task<List<TeacherEntity>> Get()
    {
        return await _context.Teachers
            .Include(t => t.Subjects)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task Create(TeacherEntity teacher)
    {
        _context.Teachers.Add(teacher);
    }

    public async Task Update(TeacherEntity teacher)
    {
        var entity = await _context.Teachers
             .Include(t => t.Subjects)
             .FirstOrDefaultAsync(t => t.TeacherId == teacher.TeacherId);

        if (entity != null)
        {
            entity.Name = teacher.Name;
            entity.Subjects = teacher.Subjects;

            _context.Teachers.Update(entity);
        }
    }

    public async Task Delete(int id)
    {
        var entity = await _context.Teachers
            .Include(t => t.Subjects)
            .FirstOrDefaultAsync(t => t.TeacherId == id);

        if (entity != null)
        {
            _context.Teachers.Remove(entity);
        }
    }
}