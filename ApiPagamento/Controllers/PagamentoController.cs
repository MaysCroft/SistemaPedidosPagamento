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
        /// GET api/v1/pagamentos/{id} - Retorna um pagamento específico pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Pagamento encontrado com sucesso</response>
        /// <response code="404">Pagamento não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);

            if (pagamento == null)
            {
                return NotFound("Pagamento não encontrado.");
            }

            return Ok(pagamento);
        }

        /// <summary>
        /// POST api/v1/pagamentos: Recebe os dados de um pagamento, valida as informações e 
        /// salva no banco de dados. Retorna o pagamento criado ou mensagens de erro em caso de falhas.
        /// </summary>
        /// <param name="pagamento"></param>
        /// <returns></returns>
        /// <response code="201">Pagamento criado com sucesso!</response>>
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

        /// <summary>
        /// PUT api/v1/pagamentos/{id} - Atualiza os dados de um pagamento existente.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pagamentoAtualizado"></param>
        /// <returns></returns>
        /// <response code="204">Pagamento atualizado com sucesso</response>
        /// <response code="400">Dados de pagamento inválidos</response>
        /// <response code="404">Pagamento não encontrado</response>
        /// <response code="500">Erro ao atualizar pagamento</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] PedidosData pagamentoAtualizado)
        {
            if (id != pagamentoAtualizado.Id)
            {
                return BadRequest("O ID da URL não corresponde ao ID do corpo da requisição.");
            }

            var pagamentoExistente = await _context.Pagamentos.FindAsync(id);

            if (pagamentoExistente == null)
            {
                return NotFound("Pagamento não encontrado.");
            }

            _context.Entry(pagamentoExistente).CurrentValues.SetValues(pagamentoAtualizado);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar o pagamento: {ex.Message}");
            }

            return NoContent();
        }

        /// <summary>
        /// DELETE api/v1/pagamentos/{id} - Remove um pagamento do banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Pagamento removido com sucesso</response>
        /// <response code="400">ID inválido</response>
        /// <response code="404">Pagamento não encontrado</response>
        /// <response code="500">Erro ao remover pagamento</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { mensagem = "ID inválido. O ID deve ser um número positivo." });

            var pagamento = await _context.Pagamentos.FindAsync(id);

            if (pagamento == null)
            {
                return NotFound("Pagamento não encontrado.");
            }

            _context.Pagamentos.Remove(pagamento);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { mensagem = "Pagamento removido com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao remover o pagamento: {ex.Message}");
            }

            return NoContent();
        }
    }
}
