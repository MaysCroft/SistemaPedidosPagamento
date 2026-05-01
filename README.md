## 💳 API de Pagamentos

- Aluno: Maycon Siqueira
- Instrutor: Fred Aguiar
- Curso: Desenvolvimento de Sistemas

Esta API é responsável pelo gerenciamento do ciclo de vida de pagamentos, permitindo a criação, consulta, atualização e exclusão de registros. A base da URL para todos os endpoints é: `/api/v1/pagamentos`.

---

### 🚀 Tecnologias Utilizadas
*   **ASP.NET Core Web API**
*   **Entity Framework Core**
*   **SQLite/SQL Server** (Persistence)
*   **Padrão Service Layer e Controller**

---

### 📌 Endpoints

| Método     | Endpoint                  | Descrição                                    |
| :--------- | :------------------------ | :------------------------------------------- |
| **GET**    | */api/v1/pagamentos*      | Lista todos os pagamentos registrados.       |
| **GET**    | */api/v1/pagamentos/{id}* | Busca um pagamento específico pelo ID.       |
| **POST**   | */api/v1/pagamentos*      | Registra um novo pagamento no sistema.       |
| **PUT**    | */api/v1/pagamentos/{id}* | Atualiza os dados de um pagamento existente. |
| **DELETE** | */api/v1/pagamentos/{id}* | Remove um registro de pagamento.             |

---

### 📑 Detalhes dos Endpoints

#### 1. Listar Pagamentos
*   **URL:** *GET /api/v1/pagamentos*
*   **Resposta de Sucesso:**
    *   **Código:** 200 OK
    *   **Conteúdo:** *{ "mensagem": "Pagamentos encontrados!", "dados": [...] }*

#### 2. Buscar por ID
*   **URL:** *GET /api/v1/pagamentos/{id}*
*   **Parâmetros:** **id** (int positivo)
*   **Regra de Negócio:** O ID deve ser maior que zero.
*   **Respostas:**
    *   **200 OK:** Pagamento encontrado.
    *   **400 Bad Request:** ID inválido.
    *   **404 Not Found:** Registro não existe no banco.

#### 3. Criar Pagamento
*   **URL:** *POST /api/v1/pagamentos*
*   **Corpo da Requisição (JSON):**
```json
{
  "data_Pedido": "2026-04-30T12:00:00",
  "nome_Cliente": "João Silva",
  "doc_Cliente": "123.456.789-00",
  "produto": "Notebook Pro",
  "quantidade": 1,
  "valor": 4500.00,
  "statusPedido": "Processando",
  "formaPagamento": "Cartão de Crédito",
  "statusPagamento": "Pendente"
}
```
*   **Validações:**
    *   Verifica se a **quantidade** e o **valor** estão dentro dos limites configurados no sistema (*ApiConfig*).
    *   Todos os campos são obrigatórios.
*   **Respostas:**
    *   **201 Created:** Retorna o objeto criado e o local do recurso.
    *   **400 Bad Request:** Dados inválidos ou acima dos limites permitidos.

#### 4. Atualizar Pagamento
*   **URL:** *PUT /api/v1/pagamentos/{id}*
*   **Regra de Negócio:** O ID enviado na URL deve ser idêntico ao ID enviado no corpo do JSON.
*   **Respostas:**
    *   **200 OK:** Atualização realizada com sucesso.
    *   **400 Bad Request:** Inconsistência entre IDs.
    *   **404 Not Found:** Pagamento não localizado.

#### 5. Remover Pagamento
*   **URL:** *DELETE /api/v1/pagamentos/{id}*
*   **Respostas:**
    *   **200 OK:** Registro removido com sucesso.
    *   **404 Not Found:** Registro não encontrado.

---

### ⚠️ Tratamento de Erros
A API utiliza códigos de status HTTP padronizados. Em caso de erro interno (Exceções), a API retornará:
*   **Código:** 500 Internal Server Error
*   **Corpo:** *{ "mensagem": "Descrição do erro ocorrido" }*

---
