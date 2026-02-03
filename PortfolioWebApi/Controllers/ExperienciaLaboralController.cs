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
        
        public ExperienciaLaboralController(FreeSqlDb2016559Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExperienciaLaboralReadDto>>> GetAll()
        {
            var data = await _context.ExperienciaLaborals.Where(x => x.Activo).ToListAsync();

            return Ok(_mapper.Map<List<ExperienciaLaboralReadDto>>(data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExperienciaLaboralReadDto>> GetById(int id)
        {
            var experiencia = await _context.ExperienciaLaborals.Where(x => x.Id == id && x.Activo).FirstOrDefaultAsync();

            if (experiencia == null || !experiencia.Activo)
                return NotFound();

            return Ok(_mapper.Map<ExperienciaLaboralReadDto>(experiencia));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ExperienciaLaboralCreateDto>> Create(ExperienciaLaboralCreateDto dto)
        {
            var experiencia = _mapper.Map<ExperienciaLaboral>(dto);

            experiencia.Activo = true;
            experiencia.FechaCreacion = DateTime.Now;
            experiencia.FechaActualizacion = DateTime.Now;

            _context.ExperienciaLaborals.Add(experiencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = experiencia.Id },
                _mapper.Map<ExperienciaLaboralReadDto>(experiencia));

        }

        [Authorize]
        [HttpPut("id")]

        public async Task<IActionResult> Update(int id, ExperienciaLaboralUpdateDto dto)
        {
            var experiencia = await _context.ExperienciaLaborals.FindAsync(id);
            if (experiencia == null || !experiencia.Activo)
                return NotFound();

            _mapper.Map(dto, experiencia);
            experiencia.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var experiencia = await _context.ExperienciaLaborals.FindAsync(id);
            if (experiencia == null)
                return NotFound();

            experiencia.Activo = false;
            experiencia.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        
    }
}