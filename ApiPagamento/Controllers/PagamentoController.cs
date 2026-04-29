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

        /// <summary>
        /// POST api/v1/pagamentos: Recebe os dados de um pagamento, valida as informações e 
        /// salva no banco de dados. Retorna o pagamento criado ou mensagens de erro em caso de falhas.
        /// </summary>
        /// <param name="pagamento"></param>
        /// <returns></returns>
        /// <response code="201">Pagamento criado com sucesso!</response>
        /// <response code="400">Dados de pagamento inválidos!</response>
        /// <response code="500">Erro ao salvar pagamento!</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] PedidosData pagamento)
        {
            try
            {
                if (pagamento == null ||
                pagamento.Data_Pedido == null ||
                string.IsNullOrWhiteSpace(pagamento.Nome_Cliente) ||
                string.IsNullOrWhiteSpace(pagamento.Doc_Cliente) ||
                string.IsNullOrWhiteSpace(pagamento.Produto) ||
                pagamento.Quantidade <= 0 ||
                pagamento.Valor <= 0 ||
                string.IsNullOrWhiteSpace(pagamento.StatusPedido) ||
                string.IsNullOrWhiteSpace(pagamento.FormaPagamento) ||
                string.IsNullOrWhiteSpace(pagamento.StatusPagamento))
                {
                    return BadRequest("Dados de pagamento inválidos.");
                }

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
