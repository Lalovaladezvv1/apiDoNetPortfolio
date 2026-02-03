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
    public class EducacionController : ControllerBase
    {
        private readonly FreeSqlDb2016559Context _context;
        private readonly IMapper _mapper;

        public EducacionController(FreeSqlDb2016559Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EducacionReadDto>>> GetAll()
        {
            var data = await _context.Educacions
                .Where(x => x.Activo)
                .ToListAsync();

            return Ok(_mapper.Map<List<EducacionReadDto>>(data));
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<EducacionReadDto>> GetById(int id)
        {
            var educacion = await _context.Educacions.FindAsync(id);
            if (educacion == null || !educacion.Activo)
                return NotFound();

            return Ok(_mapper.Map<EducacionReadDto>(educacion));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<EducacionReadDto>> Create(EducacionCreateDto dto)
        {
            var educacion = _mapper.Map<Educacion>(dto);

            educacion.Activo = true;
            educacion.FechaCreacion = DateTime.Now;
            educacion.FechaActualizacion = DateTime.Now;

            _context.Educacions.Add(educacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = educacion.Id },
                _mapper.Map<EducacionReadDto>(educacion));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EducacionUpdateDto dto)
        {
            var educacion = await _context.Educacions.FindAsync(id);
            if (educacion == null || !educacion.Activo)
                return NotFound();

            _mapper.Map(dto, educacion);
            educacion.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var educacion = await _context.Educacions.FindAsync(id);
            if (educacion == null)
                return NotFound();

            educacion.Activo = false;
            educacion.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
