using ApiPagamento.Repositories.Interfaces;
using Shared;

namespace ApiPagamento.Services
{
    public class PagamentoService
    {
        /// <summary>
        /// Repository de salas - Responsável por acessar os
        /// dados dos pagamentos no banco de dados
        /// </summary>
        private readonly IPagamentoRepository _repo;

        /// <summary>
        /// Construtor da classe - Recebe o repository de pagamentos
        /// via injeção de dependência
        /// </summary>
        /// <param name="repo"></param>
        public PagamentoService(IPagamentoRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Lista todos os pagamentos - Chama o método GetAll do
        /// repository para obter a lista de pagamentos do banco de dados
        /// </summary>
        /// <returns></returns>
        public async Task<List<PedidosData>> Listar()
            => await _repo.GetAll();

        /// <summary>
        /// Obter um pagamento por id - Chama o método GetById
        /// do repository para obter um pagamento específico
        /// do banco de dados com base no id fornecido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PedidosData> ObterPorId(int id)
            => await _repo.GetById(id);

        /// <summary>
        /// Criar um novo pagamento - Chama o método Add
        /// do repository para adicionar um novo pagamento ao banco de dados
        /// </summary>
        /// <param name="pagamento"></param>
        /// <returns></returns>
        public async Task Criar(PedidosData pagamento)
            => await _repo.Add(pagamento);

        /// <summary>
        /// Atualizar um pagamento existente - Chama 
        /// o método Update do repository
        /// </summary>
        /// <param name="pagamento"></param>
        /// <returns></returns>
        public async Task Atualizar(PedidosData pagamento)
            => await _repo.Update(pagamento);

        /// <summary>
        /// Deletar um pagamento por id -  Chama 
        /// o método Delete do repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Deletar(int id)
            => await _repo.Delete(id);
    }
}
