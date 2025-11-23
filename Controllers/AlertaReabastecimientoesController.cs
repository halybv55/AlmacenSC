using Microsoft.AspNetCore.Mvc;
using AlmacenSC.Core.Interfaces;
using AlmacenSC.Core.Mapeadores;

namespace AlmacenSC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertaReabastecimientoController : ControllerBase
    {
        private readonly IAlertaReabastecimientoRepository _repo;

        public AlertaReabastecimientoController(IAlertaReabastecimientoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var alertas = await _repo.GetAllAsync();
            return Ok(alertas.ToAlertaDtos());
        }

        [HttpPut("{id}/atender")]
        public async Task<IActionResult> Atender(int id)
        {
            var alerta = await _repo.GetByIdAsync(id);
            if (alerta == null) return NotFound();

            alerta.Atendida = true;
            await _repo.UpdateAsync(alerta);

            return Ok("Alerta atendida.");
        }
    }
}
