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
        /// Lista todas as oficinas com paginação.
        /// </summary>
        /// <param name="page">Número da página (inicia em 1)</param>
        /// <param name="pageSize">Quantidade de itens por página</param>
        [HttpGet]
        [SwaggerResponse(200, "Lista de oficinas.", typeof(IEnumerable<Oficina>))]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _service.GetAll();
            var total = query.Count();
            var itens = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var hasNext = page * pageSize < total;
            var hasPrev = page > 1;

            var links = new List<object>
            {
                new { rel = "self", href = $"/api/oficina?page={page}&pageSize={pageSize}" }
            };
            if (hasPrev) links.Add(new { rel = "prev", href = $"/api/oficina?page={page-1}&pageSize={pageSize}" });
            if (hasNext) links.Add(new { rel = "next", href = $"/api/oficina?page={page+1}&pageSize={pageSize}" });

            var result = new
            {
                data = itens,
                links,
                total
            };
            return Ok(result);
        }

        /// <summary>
        /// Obtém uma oficina pelo ID.
        /// </summary>
        /// <param name="id">ID da oficina</param>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Oficina encontrada.", typeof(Oficina))]
        [SwaggerResponse(404, "Oficina não encontrada.")]
        public async Task<IActionResult> GetById(int id)
        {
            var oficina = await _service.GetByIdAsync(id);
            if (oficina == null) return NotFound();
            return Ok(new
            {
                data = oficina,
                links = new[]
                {
                    new { rel = "self", href = $"/api/oficina/{id}" },
                    new { rel = "update", href = $"/api/oficina/{id}" },
                    new { rel = "delete", href = $"/api/oficina/{id}" }
                }
            });
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
        /// <param name="id">ID da oficina</param>
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
        /// <param name="id">ID da oficina</param>
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
        public Oficina GetExamples() => new Oficina {
            Nome = "Oficina Central",
            Endereco = "Rua das Flores, 123",
            Telefone = "+55 11 99999-9999",
            Especialidades = new List<string>{ "Motor", "Suspensão" }
        };
    }
}
