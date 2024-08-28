using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.API.Controllers
{
    /// <summary>
    /// Controlador para gerenciar professores.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        /// <summary>
        /// Obtém todos os professores.
        /// </summary>
        /// <returns>Lista de professores.</returns>
        [HttpGet("Retorna todos os professores")]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetTeachers()
        {
            var teachers = await _teacherService.Get();
            return Ok(teachers);
        }

        /// <summary>
        /// Obtém um professor pelo ID.
        /// </summary>
        /// <param name="id">ID do professor.</param>
        /// <returns>Professor com o ID especificado.</returns>
        [HttpGet("Retorna um professor por Id")]
        public async Task<ActionResult<TeacherDto>> GetTeacher(int id)
        {
            var teacher = await _teacherService.Get(id);
            if (teacher == null)
            {
                return NotFound("Professor não encontrado");
            }

            return Ok(teacher);
        }

        /// <summary>
        /// Adiciona um novo professor.
        /// </summary>
        /// <param name="teacherDto">Dados do novo professor.</param>
        /// <returns>Professor criado.</returns>
        [HttpPost("Adiciona um novo professor")]
        public async Task<ActionResult<TeacherDto>> CreateTeacher(TeacherDto teacherDto)
        {
            await _teacherService.Create(teacherDto);
            return CreatedAtAction(nameof(GetTeacher), new { id = teacherDto.TeacherId }, teacherDto);
        }

        /// <summary>
        /// Atualiza um professor existente.
        /// </summary>
        /// <param name="id">ID do professor a ser atualizado.</param>
        /// <param name="teacherDto">Dados atualizados do professor.</param>
        /// <returns>Confirmação de atualização.</returns>
        [HttpPut("Atualiza um professor")]
        public async Task<IActionResult> UpdateTeacher(int id, TeacherDto teacherDto)
        {
            if (id != teacherDto.TeacherId)
            {
                return BadRequest("O ID do professor não corresponde.");
            }

            await _teacherService.Update(teacherDto);
            return Ok("Professor atualizado com sucesso");
        }

        /// <summary>
        /// Remove um professor pelo ID.
        /// </summary>
        /// <param name="id">ID do professor a ser removido.</param>
        /// <returns>Confirmação de remoção.</returns>
        [HttpDelete("Remove um professor")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _teacherService.Get(id);
            if (teacher == null)
            {
                return NotFound("Professor não encontrado.");
            }

            await _teacherService.Delete(id);
            return Ok("Professor removido com sucesso");
        }
    }
}