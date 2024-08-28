using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Application.Models;
using FluentValidation;
using FluentValidation.Results;

namespace SchoolManagement.API.Controllers;

/// <summary>
/// Controlador para gerenciar alunos.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly IValidator<StudentDto> _studentValidator;

    public StudentController(IStudentService studentService, IValidator<StudentDto> studentValidator)
    {
        _studentService = studentService;
        _studentValidator = studentValidator;
    }

    /// <summary>
    /// Obtém todos os alunos.
    /// </summary>
    /// <returns>Lista de alunos.</returns>
    [HttpGet("Retorna todos os alunos")]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
    {
        var students = await _studentService.Get();
        return Ok(students);
    }

    /// <summary>
    /// Obtém um aluno pelo ID.
    /// </summary>
    /// <param name="id">ID do aluno.</param>
    /// <returns>Aluno com o ID especificado.</returns>
    [HttpGet("Retorna um aluno por Id especifico")]
    public async Task<ActionResult<StudentDto>> GetStudent(int id)
    {
        var student = await _studentService.Get(id);

        if (student == null)
        {
            return NotFound("Aluno não encontrado.");
        }

        return Ok(student);
    }

    /// <summary>
    /// Adiciona um novo aluno.
    /// </summary>
    /// <param name="studentDto">Dados do novo aluno.</param>
    /// <returns>Aluno criado.</returns>
    [HttpPost("Adiciona um novo aluno")]
    public async Task<ActionResult<StudentDto>> CreateStudent([FromBody] StudentDto studentDto)
    {
        // Valida o DTO antes de processar a criação
        ValidationResult validationResult = await _studentValidator.ValidateAsync(studentDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        await _studentService.Create(studentDto);
        return CreatedAtAction(nameof(GetStudent), new { id = studentDto.StudentId }, studentDto);
    }

    /// <summary>
    /// Atualiza um aluno existente.
    /// </summary>
    /// <param name="id">ID do aluno a ser atualizado.</param>
    /// <param name="studentDto">Dados atualizados do aluno.</param>
    /// <returns>Confirmação de atualização.</returns>
    [HttpPut("Atualiza os dados de um aluno")]
    public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentDto studentDto)
    {
        if (id != studentDto.StudentId)
        {
            return BadRequest("O ID do aluno não corresponde.");
        }

        // Valida o DTO antes de processar a atualização
        ValidationResult validationResult = await _studentValidator.ValidateAsync(studentDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        await _studentService.Update(studentDto);
        return NoContent();
    }

    /// <summary>
    /// Remove um aluno pelo ID.
    /// </summary>
    /// <param name="id">ID do aluno a ser removido.</param>
    /// <returns>Confirmação de remoção.</returns>
    [HttpDelete("Remove um aluno")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _studentService.Get(id);
        if (student == null)
        {
            return NotFound("Aluno não encontrado.");
        }

        await _studentService.Delete(id);
        return NoContent();
    }
}