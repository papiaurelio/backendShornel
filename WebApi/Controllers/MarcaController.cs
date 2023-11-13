using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class MarcaController : BaseApiController
    {
        private readonly IGenericRepository<Marca> _marcaRepository;

        public MarcaController(IGenericRepository<Marca> marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Marca>>> GetMarcaAll()
        {
            return Ok(await _marcaRepository.GetAllAsync());
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}")]

        public async Task<ActionResult<Marca>> GetMarcaById(int id)
        {
            return await _marcaRepository.GetByIdAsync(id);
        }
    }
}
