namespace SchoolManagement.Domain.Interfaces;
public interface IUnitOfWork : IDisposable
{
    ITeacherRepository Teachers { get; }
    ISubjectRepository Subjects { get; }
    IStudentRepository Students { get; }

    Task<int> CompleteAsync(); // Commit changes
}