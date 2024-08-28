using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Domain.Models;

namespace SchoolManagement.Infrastructure.Data.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private readonly SchoolManagementDbContext _context;

    public SubjectRepository(SchoolManagementDbContext context)
    {
        _context = context;
    }

    public async Task<SubjectEntity> Get(int id)
    {
        return await _context.Subjects
            .FirstOrDefaultAsync(s => s.SubjectId == id);
    }

    public async Task<List<SubjectEntity>> Get()
    {
        return await _context.Subjects
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task Create(SubjectEntity subject)
    {
        _context.Subjects.Add(subject);
    }

    public async Task Update(SubjectEntity subject)
    {
        var entity = _context.Subjects
            .FirstOrDefault(s => s.SubjectId == subject.SubjectId);

        if (entity != null)
        {
            entity.Name = subject.Name;
        }
    }

    public async Task Delete(int id)
    {
        var entity = _context.Subjects
            .FirstOrDefault(s => s.SubjectId == id);

        if (entity != null)
        {
            _context.Subjects.Remove(entity);
        }
    }
}