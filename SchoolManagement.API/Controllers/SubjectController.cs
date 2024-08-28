using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Application.Models;

namespace SchoolManagement.API.Controllers;

/// <summary>
/// Controlador para gerenciar disciplinas.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SubjectController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    /// <summary>
    /// Obtém todas as disciplinas.
    /// </summary>
    /// <returns>Lista de disciplinas.</returns>
    [HttpGet("Retorna todas as disciplinas")]
    public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubjects()
    {
        var subjects = await _subjectService.Get();
        return Ok(subjects);
    }

    /// <summary>
    /// Obtém uma disciplina pelo ID.
    /// </summary>
    /// <param name="id">ID da disciplina.</param>
    /// <returns>Disciplina com o ID especificado.</returns>
    [HttpGet("Retorna uma disciplina por Id")]
    public async Task<ActionResult<SubjectDto>> GetSubject(int id)
    {
        var subject = await _subjectService.Get(id);

        if (subject == null)
        {
            return NotFound();
        }

        return Ok(subject);
    }

    /// <summary>
    /// Adiciona uma nova disciplina.
    /// </summary>
    /// <param name="subjectDto">Dados da nova disciplina.</param>
    /// <returns>Disciplina criada.</returns>
    [HttpPost("Adiciona uma nova disciplina")]
    public async Task<ActionResult<SubjectDto>> CreateSubject(SubjectDto subjectDto)
    {
        await _subjectService.Create(subjectDto);
        return CreatedAtAction(nameof(GetSubject), new { id = subjectDto.SubjectId }, subjectDto);
    }

    /// <summary>
    /// Atualiza uma disciplina existente.
    /// </summary>
    /// <param name="id">ID da disciplina a ser atualizada.</param>
    /// <param name="subjectDto">Dados atualizados da disciplina.</param>
    /// <returns>Confirmação de atualização.</returns>
    [HttpPut("Atualiza uma disciplina")]
    public async Task<IActionResult> UpdateSubject(int id, SubjectDto subjectDto)
    {
        if (id != subjectDto.SubjectId)
        {
            return BadRequest("Disciplina não encontrada.");
        }

        await _subjectService.Update(subjectDto);
        return Ok("Disciplina atualizada com sucesso.");
    }

    /// <summary>
    /// Remove uma disciplina pelo ID.
    /// </summary>
    /// <param name="id">ID da disciplina a ser removida.</param>
    /// <returns>Confirmação de remoção.</returns>
    [HttpDelete("Remove uma disciplina")]
    public async Task<IActionResult> DeleteSubject(int id)
    {
        var subject = await _subjectService.Get(id);
        if (subject == null)
        {
            return BadRequest("Disciplina não encontrada.");
        }

        await _subjectService.Delete(id);
        return Ok("Disciplina removida com sucesso.");
    }
}