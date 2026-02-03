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
            var data = await _context.Logros
                .Where(x => x.Activo)
                .ToListAsync();

            return Ok(_mapper.Map<List<LogroReadDto>>(data));
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<LogroReadDto>> GetById(int id)
        {
            var logro = await _context.Logros.FindAsync(id);
            if (logro == null || !logro.Activo)
                return NotFound();

            return Ok(_mapper.Map<LogroReadDto>(logro));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<LogroReadDto>> Create(LogroCreateDto dto)
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

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LogroUpdateDto dto)
        {
            var logro = await _context.Logros.FindAsync(id);
            if (logro == null || !logro.Activo)
                return NotFound();

            _mapper.Map(dto, logro);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var logro = await _context.Logros.FindAsync(id);
            if (logro == null)
                return NotFound();

            logro.Activo = false;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("educacion/{educacionId}")]
        public async Task<IActionResult> GetLogrosPorEducacion(int educacionId)
        {
            var logros = await _context.Logros
                .Where(l => l.TipoEntidad == "Educacion" && l.EntidadId == educacionId)
                .OrderBy(l => l.Orden)
                .ToListAsync();

            var result = _mapper.Map<List<LogroReadDto>>(logros);
            return Ok(result);
        }

        [HttpGet("experiencia/{experienciaId}")]
        public async Task<IActionResult> GetLogrosPorExperiencia(int experienciaId)
        {
            var logros = await _context.Logros
                .Where(l => l.TipoEntidad == "Experiencia" && l.EntidadId == experienciaId)
                .OrderBy(l => l.Orden)
                .ToListAsync();

            var result = _mapper.Map<List<LogroReadDto>>(logros);
            return Ok(result);
        }
    }

}