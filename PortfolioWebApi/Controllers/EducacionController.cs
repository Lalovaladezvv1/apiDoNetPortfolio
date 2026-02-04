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
            try
            {
                var data = await _context.Educacions
                    .Where(x => x.Activo)
                    .ToListAsync();

                return Ok(_mapper.Map<List<EducacionReadDto>>(data));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al obtener la lista de educación",
                    detail = ex.Message
                });
            }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<EducacionReadDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "El id debe ser mayor a 0" });

            var educacion = await _context.Educacions.FindAsync(id);
            if (educacion == null || !educacion.Activo)
                return NotFound(new { message = $"No existe educación con id {id}" });

            return Ok(_mapper.Map<EducacionReadDto>(educacion));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<EducacionReadDto>> Create(EducacionCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
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
            catch (AutoMapperMappingException ex)
            {
                return BadRequest(new
                {
                    message = "Error al mapear los datos de educación",
                    detail = ex.Message
                });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al guardar en la base de datos",
                    detail = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EducacionUpdateDto dto)
        {
            if (id <= 0)
                return BadRequest(new { message = "Id inválido" });

            var educacion = await _context.Educacions.FindAsync(id);
            if (educacion == null || !educacion.Activo)
                return NotFound(new { message = "Registro no encontrado" });

            try
            {
                _mapper.Map(dto, educacion);
                educacion.FechaActualizacion = DateTime.Now;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al actualizar educación",
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

            var educacion = await _context.Educacions.FindAsync(id);
            if (educacion == null)
                return NotFound(new { message = "Registro no encontrado" });

            educacion.Activo = false;
            educacion.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Registro eliminado correctamente" });
        }

    }

}
