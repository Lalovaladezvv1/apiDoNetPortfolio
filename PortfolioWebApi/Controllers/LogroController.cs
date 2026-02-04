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
    public class LogroController : ControllerBase
    {
        private readonly FreeSqlDb2016559Context _context;
        private readonly IMapper _mapper;

        public LogroController(FreeSqlDb2016559Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogroReadDto>>> GetAll()
        {
            try
            {
                var data = await _context.Logros
                    .Where(x => x.Activo)
                    .ToListAsync();

                return Ok(_mapper.Map<List<LogroReadDto>>(data));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al obtener los logros",
                    detail = ex.Message
                });
            }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<LogroReadDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Id inválido" });

            var logro = await _context.Logros.FindAsync(id);
            if (logro == null || !logro.Activo)
                return NotFound(new { message = $"No existe logro con id {id}" });

            return Ok(_mapper.Map<LogroReadDto>(logro));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<LogroReadDto>> Create(LogroCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var logro = _mapper.Map<Logro>(dto);

                logro.Activo = true;
                logro.FechaCreacion = DateTime.Now;

                _context.Logros.Add(logro);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById),
                    new { id = logro.Id },
                    _mapper.Map<LogroReadDto>(logro));
            }
            catch (AutoMapperMappingException ex)
            {
                return BadRequest(new
                {
                    message = "Error al mapear el logro",
                    detail = ex.Message
                });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al guardar el logro en la base de datos",
                    detail = ex.InnerException?.Message ?? ex.Message
                });
            }
        }


        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LogroUpdateDto dto)
        {
            if (id <= 0)
                return BadRequest(new { message = "Id inválido" });

            var logro = await _context.Logros.FindAsync(id);
            if (logro == null || !logro.Activo)
                return NotFound(new { message = "Logro no encontrado" });

            try
            {
                _mapper.Map(dto, logro);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al actualizar el logro",
                    detail = ex.Message
                });
            }
        }


        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Id inválido" });

            var logro = await _context.Logros.FindAsync(id);
            if (logro == null)
                return NotFound(new { message = "Logro no encontrado" });

            logro.Activo = false;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Logro eliminado correctamente" });
        }


        [HttpGet("educacion/{educacionId}")]
        public async Task<IActionResult> GetLogrosPorEducacion(int educacionId)
        {
            if (educacionId <= 0)
                return BadRequest(new { message = "Id de educación inválido" });

            var logros = await _context.Logros
                .Where(l =>
                    l.TipoEntidad == "Educacion" &&
                    l.EntidadId == educacionId &&
                    l.Activo)
                .OrderBy(l => l.Orden)
                .ToListAsync();

            return Ok(_mapper.Map<List<LogroReadDto>>(logros));
        }


        [HttpGet("experiencia/{experienciaId}")]
        public async Task<IActionResult> GetLogrosPorExperiencia(int experienciaId)
        {
            if (experienciaId <= 0)
                return BadRequest(new { message = "Id de experiencia inválido" });

            var logros = await _context.Logros
                .Where(l =>
                    l.TipoEntidad == "Experiencia" &&
                    l.EntidadId == experienciaId &&
                    l.Activo)
                .OrderBy(l => l.Orden)
                .ToListAsync();

            return Ok(_mapper.Map<List<LogroReadDto>>(logros));
        }

    }

}