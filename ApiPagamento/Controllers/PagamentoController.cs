using ApiPagamento.Config;
using ApiPagamento.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared;

namespace ApiPagamento.Controllers
{
    [ApiController]
    [Route("api/v1/pagamentos")]
    public class PagamentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PagamentoController(AppDbContext context, IOptions<ApiConfig> config)
        {
            _context = context;
        }
    }
}
