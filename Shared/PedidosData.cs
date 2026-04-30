using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Shared
{
    public class PedidosData
    {
        public int Id { get; set; }
        public DateTime Data_Pedido { get; set; }
        public required string Nome_Cliente { get; set; }
        public required string Doc_Cliente { get; set; }
        public required string Produto { get; set; }
        public required int Quantidade { get; set; }
        public required double Valor { get; set; }
        public required string StatusPedido { get; set; }
        public required string FormaPagamento { get; set; }
        public required string StatusPagamento { get; set; }
    }
}
