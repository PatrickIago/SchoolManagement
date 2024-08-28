using FluentValidation;
using SchoolManagement.Application.Models;
public class StudentValidator : AbstractValidator<StudentDto>
{
    public StudentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("O Nome do aluno não deve estar vazio");

        RuleFor(x => x.Age)
            .NotEmpty()
            .WithMessage("A idade do aluno não deve estar vazia")
            .InclusiveBetween(7, 18)
            .WithMessage("A idade deve ter entre 7 e 18 anos.");
    }
}