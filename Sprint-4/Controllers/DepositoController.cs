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
        /// Lista todos os dep�sitos.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, "Lista de dep�sitos.", typeof(IEnumerable<Deposito>))]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll().ToList());
        }

        /// <summary>
        /// Obt�m um dep�sito pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Dep�sito encontrado.", typeof(Deposito))]
        [SwaggerResponse(404, "Dep�sito n�o encontrado.")]
        public async Task<IActionResult> GetById(int id)
        {
            var deposito = await _service.GetByIdAsync(id);
            if (deposito == null) return NotFound();
            return Ok(deposito);
        }

        /// <summary>
        /// Cria um novo dep�sito.
        /// </summary>
        [HttpPost]
        [SwaggerRequestExample(typeof(Deposito), typeof(DepositoExample))]
        [SwaggerResponse(201, "Dep�sito criado.", typeof(Deposito))]
        public async Task<IActionResult> Create([FromBody] Deposito deposito)
        {
            var created = await _service.AddAsync(deposito);
            return Created($"/api/deposito/{created.Id}", created);
        }

        /// <summary>
        /// Atualiza um dep�sito existente.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerRequestExample(typeof(Deposito), typeof(DepositoExample))]
        [SwaggerResponse(204, "Dep�sito atualizado.")]
        [SwaggerResponse(404, "Dep�sito n�o encontrado.")]
        public async Task<IActionResult> Update(int id, [FromBody] Deposito deposito)
        {
            var updated = await _service.UpdateAsync(id, deposito);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Remove um dep�sito pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Dep�sito removido.")]
        [SwaggerResponse(404, "Dep�sito n�o encontrado.")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }

    public class DepositoExample : IExamplesProvider<Deposito>
    {
        public Deposito GetExamples() => new Deposito { Nome = "Dep�sito Central", Endereco = "Rua das Flores, 123" };
    }
}
