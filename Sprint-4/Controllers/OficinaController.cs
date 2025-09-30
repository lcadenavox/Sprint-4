using Microsoft.AspNetCore.Mvc;
using Sprint_4.Models;
using Sprint_4.Services;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Sprint_4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OficinaController : ControllerBase
    {
        private readonly OficinaService _service;
        public OficinaController(OficinaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todas as oficinas.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, "Lista de oficinas.", typeof(IEnumerable<Oficina>))]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll().ToList());
        }

        /// <summary>
        /// Obtém uma oficina pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Oficina encontrada.", typeof(Oficina))]
        [SwaggerResponse(404, "Oficina não encontrada.")]
        public async Task<IActionResult> GetById(int id)
        {
            var oficina = await _service.GetByIdAsync(id);
            if (oficina == null) return NotFound();
            return Ok(oficina);
        }

        /// <summary>
        /// Cria uma nova oficina.
        /// </summary>
        [HttpPost]
        [SwaggerRequestExample(typeof(Oficina), typeof(OficinaExample))]
        [SwaggerResponse(201, "Oficina criada.", typeof(Oficina))]
        public async Task<IActionResult> Create([FromBody] Oficina oficina)
        {
            var created = await _service.AddAsync(oficina);
            return Created($"/api/oficina/{created.Id}", created);
        }

        /// <summary>
        /// Atualiza uma oficina existente.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerRequestExample(typeof(Oficina), typeof(OficinaExample))]
        [SwaggerResponse(204, "Oficina atualizada.")]
        [SwaggerResponse(404, "Oficina não encontrada.")]
        public async Task<IActionResult> Update(int id, [FromBody] Oficina oficina)
        {
            var updated = await _service.UpdateAsync(id, oficina);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Remove uma oficina pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Oficina removida.")]
        [SwaggerResponse(404, "Oficina não encontrada.")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }

    public class OficinaExample : IExamplesProvider<Oficina>
    {
        public Oficina GetExamples() => new Oficina { Nome = "Oficina Central", Endereco = "Rua das Flores, 123" };
    }
}
