using Microsoft.AspNetCore.Mvc;
using AlmacenSC.Core.Interfaces;
using AlmacenSC.Core.Mapeadores;

namespace AlmacenSC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargaProductoController : ControllerBase
    {
        private readonly ICargaProductoRepository _repo;

        public CargaProductoController(ICargaProductoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cargas = await _repo.GetAllAsync();
            return Ok(cargas);
        }
    }
}
