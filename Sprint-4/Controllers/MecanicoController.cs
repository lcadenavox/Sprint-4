using Microsoft.AspNetCore.Mvc;
using Sprint_4.Models;
using Sprint_4.Services;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Sprint_4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MecanicoController : ControllerBase
    {
        private readonly MecanicoService _service;
        public MecanicoController(MecanicoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todos os mec�nicos.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, "Lista de mec�nicos.", typeof(IEnumerable<Mecanico>))]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll().ToList());
        }

        /// <summary>
        /// Obt�m um mec�nico pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Mec�nico encontrado.", typeof(Mecanico))]
        [SwaggerResponse(404, "Mec�nico n�o encontrado.")]
        public async Task<IActionResult> GetById(int id)
        {
            var mecanico = await _service.GetByIdAsync(id);
            if (mecanico == null) return NotFound();
            return Ok(mecanico);
        }

        /// <summary>
        /// Cria um novo mec�nico.
        /// </summary>
        [HttpPost]
        [SwaggerRequestExample(typeof(Mecanico), typeof(MecanicoExample))]
        [SwaggerResponse(201, "Mec�nico criado.", typeof(Mecanico))]
        public async Task<IActionResult> Create([FromBody] Mecanico mecanico)
        {
            var created = await _service.AddAsync(mecanico);
            return Created($"/api/mecanico/{created.Id}", created);
        }

        /// <summary>
        /// Atualiza um mec�nico existente.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerRequestExample(typeof(Mecanico), typeof(MecanicoExample))]
        [SwaggerResponse(204, "Mec�nico atualizado.")]
        [SwaggerResponse(404, "Mec�nico n�o encontrado.")]
        public async Task<IActionResult> Update(int id, [FromBody] Mecanico mecanico)
        {
            var updated = await _service.UpdateAsync(id, mecanico);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Remove um mec�nico pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Mec�nico removido.")]
        [SwaggerResponse(404, "Mec�nico n�o encontrado.")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }

    public class MecanicoExample : IExamplesProvider<Mecanico>
    {
        public Mecanico GetExamples() => new Mecanico { Nome = "Jo�o Silva", Especialidade = "Motor" };
    }
}
