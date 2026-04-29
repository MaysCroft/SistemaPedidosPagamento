using ApiPagamento.Data;
using ApiPagamento.Config;
using ApiPagamento.Services;
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
        private readonly PagamentoService _service;
        private readonly IOptions<ApiConfig> _config;

        public PagamentoController(AppDbContext context, PagamentoService service, IOptions<ApiConfig> config)
        {
            _context = context;
            _service = service;
            _config = config;
        }

        /// <summary>
        /// GET: api/v1/pagamentos: Retorna a lista de pagamentos registrados no banco de dados.
        /// </summary>
        /// <remarks>
        /// GET: api/v1/pagamentos: Retorna a lista de pagamentos registrados no banco de dados.
        /// 
        /// Observação: 
        /// - O ID deve ser um número inteiro positivo. 
        /// Se um ID inválido for fornecido (como um número negativo ou zero), 
        /// a API retornará um erro 400 Bad Request com uma mensagem explicativa.
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Lista de pagamentos retornada com sucesso</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var dados = await _service.Listar();
                return Ok(new { mensagem = "Pagamentos encontrados!", dados });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro ao listar pagamentos : {ex.Message}" });
            }
        }

        /// <summary>
        /// GET api/v1/pagamentos/{id} - Retorna um pagamento específico pelo ID.
        /// </summary>
        /// <remarks>
        /// GET api/v1/pagamentos/{id} - Retorna um pagamento específico pelo ID.
        /// 
        /// Observação: 
        /// - O ID deve ser um número inteiro positivo. 
        /// Se um ID inválido for fornecido (como um número negativo ou zero), 
        /// a API retornará um erro 400 Bad Request com uma mensagem explicativa.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Pagamento encontrado com sucesso</response>
        /// <response code="400">ID inválido fornecido</response>
        /// <response code="404">Pagamento não encontrado</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new { mensagem = "O ID informado deve ser maior que zero" });

            try
            {
                var pagamento = await _service.ObterPorId(id);

                if (pagamento == null)
                {
                    return NotFound(new { mensagem = $"Pagamento com ID {id} não encontrado!" });
                }

                return Ok(new { mensagem = "Pagamento encontrado!", pagamento });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro ao buscar pagamento : {ex.Message}" });
            }
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
        public async Task<IActionResult> CriarPagamento([FromBody] PedidosData pagamento)
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
                    return BadRequest(new { mensagem = "Todos os campos são obrigatórios!" });
                }

                await _service.Criar(pagamento);
                return CreatedAtAction(nameof(GetById), new { id = pagamento.Id }, pagamento);
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
        /// <response code="200">Pagamento atualizado com sucesso</response>
        /// <response code="400">Dados de pagamento inválidos</response>
        /// <response code="404">Pagamento não encontrado</response>
        /// <response code="500">Erro ao atualizar pagamento</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarPagamento(int id, [FromBody] PedidosData pagamentoAtualizado)
        {
            try
            {
                if (id != pagamentoAtualizado.Id)
                {
                    return BadRequest(new { mensagem = "O ID da URL não corresponde ao ID do corpo" });
                }

                var pagamentoExistente = await _context.Pagamentos.FindAsync(id);

                if (pagamentoExistente == null)
                {
                    return NotFound(new { mensagem = "Pagamento não encontrado." });
                }

                _context.Entry(pagamentoExistente).CurrentValues.SetValues(pagamentoAtualizado);

                await _context.SaveChangesAsync();
                return Ok(new { mensagem = "Pagamento atualizado com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar o pagamento: {ex.Message}");
            }
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

            try
            {
                var pagamento = await _service.ObterPorId(id);

                if (pagamento == null)
                    return NotFound(new { mensagem = "Pagamento não encontrado." });

                await _service.Deletar(pagamento.Id);
                return Ok(new { mensagem = "Pagamento removido com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao remover o pagamento {id} : {ex.Message}");
            }
        }
    }
}
