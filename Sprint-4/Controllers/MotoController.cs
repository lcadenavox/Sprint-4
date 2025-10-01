using Microsoft.AspNetCore.Mvc;
using Sprint_4.Models;
using Sprint_4.Services;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Sprint_4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoController : ControllerBase
    {
        private readonly MotoService _service;
        public MotoController(MotoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todas as motos com paginação.
        /// </summary>
        /// <param name="page">Número da página (inicia em 1)</param>
        /// <param name="pageSize">Quantidade de itens por página</param>
        [HttpGet]
        [SwaggerResponse(200, "Lista de motos.", typeof(IEnumerable<Moto>))]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _service.GetAll();
            var total = query.Count();
            var motos = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var hasNext = page * pageSize < total;
            var hasPrev = page > 1;

            var links = new List<object>
            {
                new { rel = "self", href = $"/api/moto?page={page}&pageSize={pageSize}" }
            };
            if (hasPrev) links.Add(new { rel = "prev", href = $"/api/moto?page={page-1}&pageSize={pageSize}" });
            if (hasNext) links.Add(new { rel = "next", href = $"/api/moto?page={page+1}&pageSize={pageSize}" });

            var result = new
            {
                data = motos,
                links,
                total
            };
            return Ok(result);
        }

        /// <summary>
        /// Obtém uma moto pelo ID.
        /// </summary>
        /// <param name="id">ID da moto</param>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Moto encontrada.", typeof(Moto))]
        [SwaggerResponse(404, "Moto não encontrada.")]
        public async Task<IActionResult> GetById(int id)
        {
            var moto = await _service.GetByIdAsync(id);
            if (moto == null) return NotFound();
            return Ok(new
            {
                data = moto,
                links = new[]
                {
                    new { rel = "self", href = $"/api/moto/{id}" },
                    new { rel = "update", href = $"/api/moto/{id}" },
                    new { rel = "delete", href = $"/api/moto/{id}" }
                }
            });
        }

        /// <summary>
        /// Cria uma nova moto.
        /// </summary>
        [HttpPost]
        [SwaggerRequestExample(typeof(Moto), typeof(MotoExample))]
        [SwaggerResponse(201, "Moto criada.", typeof(Moto))]
        public async Task<IActionResult> Create([FromBody] Moto moto)
        {
            var created = await _service.AddAsync(moto);
            return Created($"/api/moto/{created.Id}", created);
        }

        /// <summary>
        /// Atualiza uma moto existente.
        /// </summary>
        /// <param name="id">ID da moto</param>
        [HttpPut("{id}")]
        [SwaggerRequestExample(typeof(Moto), typeof(MotoExample))]
        [SwaggerResponse(204, "Moto atualizada.")]
        [SwaggerResponse(404, "Moto não encontrada.")]
        public async Task<IActionResult> Update(int id, [FromBody] Moto moto)
        {
            var updated = await _service.UpdateAsync(id, moto);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Remove uma moto pelo ID.
        /// </summary>
        /// <param name="id">ID da moto</param>
        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Moto removida.")]
        [SwaggerResponse(404, "Moto não encontrada.")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }

    public class MotoExample : IExamplesProvider<Moto>
    {
        public Moto GetExamples() => new Moto { Marca = "Honda", Modelo = "CG 160", Ano = 2022 };
    }
}
