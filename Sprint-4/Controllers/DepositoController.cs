using Microsoft.AspNetCore.Mvc;
using Sprint_4.Models;
using Sprint_4.Services;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Sprint_4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepositoController : ControllerBase
    {
        private readonly DepositoService _service;
        public DepositoController(DepositoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todos os depósitos com paginação.
        /// </summary>
        /// <param name="page">Número da página (inicia em 1)</param>
        /// <param name="pageSize">Quantidade de itens por página</param>
        [HttpGet]
        [SwaggerResponse(200, "Lista de depósitos.", typeof(IEnumerable<Deposito>))]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _service.GetAll();
            var total = query.Count();
            var itens = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var result = new
            {
                data = itens,
                links = new[]
                {
                    new { rel = "self", href = $"/api/deposito?page={page}&pageSize={pageSize}" },
                    new { rel = "next", href = $"/api/deposito?page={page+1}&pageSize={pageSize}" }
                },
                total
            };
            return Ok(result);
        }

        /// <summary>
        /// Obtém um depósito pelo ID.
        /// </summary>
        /// <param name="id">ID do depósito</param>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Depósito encontrado.", typeof(Deposito))]
        [SwaggerResponse(404, "Depósito não encontrado.")]
        public async Task<IActionResult> GetById(int id)
        {
            var deposito = await _service.GetByIdAsync(id);
            if (deposito == null) return NotFound();
            return Ok(new
            {
                data = deposito,
                links = new[]
                {
                    new { rel = "self", href = $"/api/deposito/{id}" },
                    new { rel = "update", href = $"/api/deposito/{id}" },
                    new { rel = "delete", href = $"/api/deposito/{id}" }
                }
            });
        }

        /// <summary>
        /// Cria um novo depósito.
        /// </summary>
        [HttpPost]
        [SwaggerRequestExample(typeof(Deposito), typeof(DepositoExample))]
        [SwaggerResponse(201, "Depósito criado.", typeof(Deposito))]
        public async Task<IActionResult> Create([FromBody] Deposito deposito)
        {
            var created = await _service.AddAsync(deposito);
            return Created($"/api/deposito/{created.Id}", created);
        }

        /// <summary>
        /// Atualiza um depósito existente.
        /// </summary>
        /// <param name="id">ID do depósito</param>
        [HttpPut("{id}")]
        [SwaggerRequestExample(typeof(Deposito), typeof(DepositoExample))]
        [SwaggerResponse(204, "Depósito atualizado.")]
        [SwaggerResponse(404, "Depósito não encontrado.")]
        public async Task<IActionResult> Update(int id, [FromBody] Deposito deposito)
        {
            var updated = await _service.UpdateAsync(id, deposito);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Remove um depósito pelo ID.
        /// </summary>
        /// <param name="id">ID do depósito</param>
        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Depósito removido.")]
        [SwaggerResponse(404, "Depósito não encontrado.")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }

    public class DepositoExample : IExamplesProvider<Deposito>
    {
        public Deposito GetExamples() => new Deposito { Nome = "Depósito Central", Endereco = "Rua das Flores, 123" };
    }
}
