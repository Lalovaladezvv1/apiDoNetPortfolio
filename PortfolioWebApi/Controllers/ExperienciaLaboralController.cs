using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace PortfolioWebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ExperienciaLaboralController : ControllerBase
    {
        private readonly FreeSqlDb2016559Context _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ExperienciaLaboralController> _logger;

        public ExperienciaLaboralController(FreeSqlDb2016559Context context, IMapper mapper, ILogger<ExperienciaLaboralController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExperienciaLaboralReadDto>>> GetAll()
        {
            try
            {
                var data = await _context.ExperienciaLaborals.Where(x => x.Activo).ToListAsync();

                return Ok(_mapper.Map<List<ExperienciaLaboralReadDto>>(data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener experiencias laborales");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    Problem(
                        title: "Error interno del servidor",
                        detail: "Ocurrió un error al obtener las experiencias laborales.",
                        statusCode: 500
                    )
                );
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExperienciaLaboralReadDto>> GetById(int id)
        {
            try
            {
                var experiencia = await _context.ExperienciaLaborals.Where(x => x.Id == id && x.Activo).FirstOrDefaultAsync();

                if (experiencia == null || !experiencia.Activo)
                    return NotFound();

                return Ok(_mapper.Map<ExperienciaLaboralReadDto>(experiencia));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener experiencia con id {Id}", id);

                return StatusCode(500, Problem(
                    title: "Error interno del servidor",
                    detail: "No se pudo obtener la experiencia laboral.",
                    statusCode: 500
                ));
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ExperienciaLaboralCreateDto>> Create(ExperienciaLaboralCreateDto dto)
        {
            try
            {
                var experiencia = _mapper.Map<ExperienciaLaboral>(dto);

                experiencia.Activo = true;
                experiencia.FechaCreacion = DateTime.UtcNow;
                experiencia.FechaActualizacion = DateTime.UtcNow;

                _context.ExperienciaLaborals.Add(experiencia);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById),
                    new { id = experiencia.Id },
                    _mapper.Map<ExperienciaLaboralReadDto>(experiencia));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error al guardar experiencia laboral");

                return StatusCode(500, Problem(
                    title: "Error de base de datos",
                    detail: "No se pudo guardar la experiencia laboral.",
                    statusCode: 500
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear experiencia");

                return StatusCode(500, Problem(
                    title: "Error interno del servidor",
                    detail: "Ocurrió un error inesperado.",
                    statusCode: 500
                ));
            }

        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ExperienciaLaboralUpdateDto dto)
        {
            try
            {
                var experiencia = await _context.ExperienciaLaborals.FindAsync(id);

                if (experiencia == null || !experiencia.Activo)
                {
                    return NotFound(Problem(
                        title: "Recurso no encontrado",
                        detail: $"No existe experiencia laboral activa con id {id}.",
                        statusCode: 404
                    ));
                }

                _mapper.Map(dto, experiencia);
                experiencia.FechaActualizacion = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar experiencia con id {Id}", id);

                return StatusCode(500, Problem(
                    title: "Error interno del servidor",
                    detail: "No se pudo actualizar la experiencia laboral.",
                    statusCode: 500
                ));
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var experiencia = await _context.ExperienciaLaborals.FindAsync(id);

                if (experiencia == null)
                {
                    return NotFound(Problem(
                        title: "Recurso no encontrado",
                        detail: $"No existe experiencia laboral con id {id}.",
                        statusCode: 404
                    ));
                }

                experiencia.Activo = false;
                experiencia.FechaActualizacion = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar experiencia con id {Id}", id);

                return StatusCode(500, Problem(
                    title: "Error interno del servidor",
                    detail: "No se pudo eliminar la experiencia laboral.",
                    statusCode: 500
                ));
            }
        }


    }
}