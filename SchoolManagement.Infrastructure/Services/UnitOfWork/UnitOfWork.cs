using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Infrastructure.Data.Repositories;
using SchoolManagement.Infrastructure.Repositories.Teacher;

namespace SchoolManagement.Infrastructure.Data;
public class UnitOfWork : IUnitOfWork
{
    private readonly SchoolManagementDbContext _context;

    public UnitOfWork(SchoolManagementDbContext context)
    {
        _context = context;
        Teachers = new TeacherRepository(_context);
        Subjects = new SubjectRepository(_context);
        Students = new StudentRepository(_context);
    }

    public ITeacherRepository Teachers { get; private set; }
    public ISubjectRepository Subjects { get; private set; }
    public IStudentRepository Students { get; private set; }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}