using ApiPagamento.Data;
using ApiPagamento.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace ApiPagamento.Controllers
{
    [ApiController]
    [Route("api/v1/pagamentos")]
    public class PagamentoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IOptions<ApiConfig> _config;

        public PagamentoController(AppDbContext context, IOptions<ApiConfig> config)
        {
            _context = context;
            _config = config;
        }

        /// <summary>
        /// GET: api/v1/pagamentos: Retorna a lista de pagamentos registrados no banco de dados.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dados = await _context.Pagamentos.ToListAsync();
            return Ok(dados);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PedidosData pagamento)
        {
            if (pagamento == null ||
                pagamento.Data_Pedido == null ||
                string.IsNullOrWhiteSpace(pagamento.Nome_Cliente) ||
                string.IsNullOrWhiteSpace(pagamento.Doc_Cliente) ||
                string.IsNullOrWhiteSpace(pagamento.Produto) ||
                string.
            {
                return BadRequest("Dados de pagamento inválidos.");
            }

            try
            {
                _context.Pagamentos.Add(pagamento);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = pagamento.Id }, pagamento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar o pagamento: {ex.Message}");
            }
        }
    }
}
