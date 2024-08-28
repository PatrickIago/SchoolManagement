using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Infrastructure.Data;
using SchoolManagement.Infrastructure.Data.Repositories;
using SchoolManagement.Infrastructure.Data.Services;
using SchoolManagement.Infrastructure.Repositories.Teacher;
using SchoolManagement.Infrastructure.Services.Subject;

namespace SchoolManagement.Infrastructure;
public class SqlInitializer
{
    public static void Initialize(IServiceCollection services, IConfiguration configuration)
    {
        //REPOSITORIOS
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();

        // SERVIÇOS
        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<IStudentService, StudentService>();

        // VALIDADOR

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<StudentValidator>();

    }
}
