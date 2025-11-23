using Microsoft.AspNetCore.Mvc;
using AlmacenSC.Core.Interfaces;
using AlmacenSC.Core.Mapeadores;

namespace AlmacenSC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioController : ControllerBase
    {
        private readonly IInventarioRepository _repo;

        public InventarioController(IInventarioRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inv = await _repo.GetAllAsync();
            return Ok(inv.ToInventarioDtos());
        }
    }
}
