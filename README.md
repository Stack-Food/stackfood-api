# 🧾 StackFood API

Sistema Backend para gerenciamento de pedidos e produtos em uma lanchonete com autoatendimento, desenvolvido como parte do **Tech Challenge** da formação em Arquitetura de Software.

[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Stack-Food_stackfood-api&metric=coverage)](https://sonarcloud.io/summary/new_code?id=Stack-Food_stackfood-api)
[![Quality gate](https://sonarcloud.io/api/project_badges/quality_gate?project=Stack-Food_stackfood-api)](https://sonarcloud.io/summary/new_code?id=Stack-Food_stackfood-api)

---

## 📋 Descrição do Projeto

O **StackFood API** resolve o problema de desorganização no atendimento de uma lanchonete em expansão. Com foco no autoatendimento, o sistema permite que clientes realizem pedidos personalizados, paguem via QR Code (Mercado Pago) e acompanhem o status do pedido em tempo real, enquanto o administrador pode gerenciar produtos, clientes e acompanhar os pedidos em andamento.

---

## 🎯 Funcionalidades

- **Cadastro e Identificação de Clientes**
  - Cadastro com nome e e-mail
  - Identificação via CPF
  - Pedido anônimo (sem identificação)
- **Montagem de Combos**
  - Lanche, acompanhamento, bebida, sobremesa
- **Gerenciamento Administrativo**
  - Cadastro/edição de produtos
  - Categorias fixas (lanche, acompanhamento, bebida, sobremesa)
  - Acompanhamento de pedidos e tempo de espera
- **Processamento de Pedidos**
  - Envio para cozinha com etapas: Recebido, Em preparação, Pronto, Finalizado
- **Pagamento**
  - Integração com QR Code do Mercado Pago (checkout simulado para MVP)
- **Monitoramento de Pagamento**
  - Worker consulta status do pagamento e libera pedido para cozinha

## 🚨 Importante FAKE CHECKOUT

Para fins de fake checkout fizemos com que o nome do cliente identifique o status do pagamento ao criar o usuario colocar no nome PAGO/CANCELADO ou se não informar nada o pagamento ficará PENDENTE

- Exemplo de pagamento que com status pago:

![image](https://github.com/user-attachments/assets/c3bf7f61-91d4-4520-9841-f099472a2a62)

- Exemplo de pagamento que com status cancelado:

![image](https://github.com/user-attachments/assets/f092dda6-6903-426c-ba38-18af107b9989)

- Exemplo de pagamento que com status pendente:

![image](https://github.com/user-attachments/assets/00abe7a3-f430-4fe1-8d08-6ac1a2329c8a)

---

## 🛠️ Tecnologias Utilizadas

- **Linguagem:** C# (.NET 8)
- **Banco de Dados:** PostgreSQL 15.3
- **Arquitetura:** Clean Arquitecture
- **ORM:** Entity Framework Core
- **Integração de Pagamento:** Mercado Pago SDK
- **Documentação de API:** Swagger / OpenAPI
- **Containerização:** Docker
- **Orquestração:** Docker Compose

---

## 🗂️ Estrutura do Projeto

```
src/
├── Services/
│   ├── StackFood.API/                          # API REST (entrada principal)
│   └── StackFood.Worker/                       # Worker (consulta status de pagamento)
├── Domain/
│   ├── StackFood.Domain/                       # Entidades e regras de negócio
├── Application/
│   └── StackFood.Application/                  # Casos de uso e interfaces
├── Infrastructure/
│   └── StackFood.Infra/                        # Infraestrutura: banco, repositórios, serviços
|   └── StackFood.ExternalService.MercadoPago/  # Integração Mercado Pago
├── Tests/
│   └── StackFood.Tests/                        # Testes automatizados
├── docker-compose.yml
├── .env.example
└── README.md
```

---

## 🏛️ Clean Arquitecture

O projeto segue a Clean Arquitecture, separando regras de negócio (Domain) das implementações técnicas (infraestrutura e integrações externas).

- **Domain:** Entidades e regras de negócio puras (ex: Pedido, Cliente, Produto).
- **Application:** Casos de uso (ex: Criar Pedido, Gerar Pagamento) e interfaces (ports).
- **Infra:** Implementações técnicas (banco, repositórios, serviços externos).
- **Services:** Pontos de entrada (API REST, Worker).

**Vantagens:**

- Independência de frameworks e tecnologias externas.
- Facilidade para testes e manutenção.
- Troca de implementações sem afetar o core do sistema.

---

## 🚀 Como Executar Localmente

### Pré-requisitos

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- [Git](https://git-scm.com/)

### Passos

1. **Clone o repositório**

   ```bash
   git clone https://github.com/Stack-Food/stackfood-api.git
   cd stackfood-api
   ```

2. **Configure o ambiente**

   ```bash
   cp .env.example .env
   ```

   Edite o arquivo `.env` conforme necessário (principalmente a senha do banco e token do Mercado Pago).

3. **Suba o ambiente**
   ```bash
   docker-compose up --build
   ```

- API: [https://localhost:7189](http://localhost:7189)
- Swagger UI: [https://localhost:7189/swagger/index.html](http://localhost:7189/swagger/index.html)

---

## ⚙️ Variáveis de Ambiente

| Variável                 | Descrição               | Valor Padrão     |
| ------------------------ | ----------------------- | ---------------- |
| API_VERSION              | Versão da imagem da API | 1.0.0            |
| API_HTTP_PORT            | Porta HTTP da API       | 5039             |
| API_HTTPS_PORT           | Porta HTTPS da API      | 7189             |
| ENVIRONMENT              | Ambiente ASP.NET Core   | Development      |
| BUILD_CONFIGURATION      | Configuração de build   | Debug            |
| POSTGRES_DB              | Nome do banco de dados  | stackfood        |
| POSTGRES_USER            | Usuário do PostgreSQL   | postgres         |
| POSTGRES_PASSWORD        | Senha do PostgreSQL     | StrongP@ssw0rd!  |
| POSTGRES_PORT            | Porta do PostgreSQL     | 5432             |
| SEU_ACCESS_TOKEN_SANDBOX | Token Mercado Pago      | (defina no .env) |

---

## 🧩 Serviços Disponíveis

| Serviço          | Descrição                               | Porta |
| ---------------- | --------------------------------------- | ----- |
| stackfood.api    | API .NET 8 com Swagger UI               | 7189  |
| postgres         | Banco de dados PostgreSQL 15.3          | 5432  |
| stackfood-worker | Worker para monitoramento de pagamentos | -     |

---

## 🛠️ Fluxo de Desenvolvimento

1. Modifique o código.
2. O container da API recarrega automaticamente.
3. Teste via Swagger UI ou cliente de API.
4. Faça commit e push das alterações.

---

## 🗄️ Gerenciamento do Banco de Dados

- **Acessar via Docker:**
  ```bash
  docker exec -it stackfood-db psql -U postgres -d stackfood
  ```
- **Acessar via cliente externo:**

  - Host: `localhost`
  - Porta: `5432`
  - Banco: `stackfood`
  - Usuário: `postgres`
  - Senha: conforme `.env`

- **Backup:**
  ```bash
  docker exec stackfood-db pg_dump -U postgres -d stackfood > backup_$(date +%Y%m%d_%H%M%S).sql
  ```
  Backups ficam no volume `backup_data`.

---

## 🩺 Troubleshooting

- **API não inicia:**  
  Verifique logs:

  ```bash
  docker-compose logs stackfood.api
  ```

- **Problemas de conexão com o banco:**  
  Confirme se o serviço está rodando:

  ```bash
  docker-compose ps postgres
  ```

  Verifique as credenciais no `.env`.

- **Resetar ambiente:**

  ```bash
  docker-compose down -v
  docker-compose up -d
  ```

- **Migrations não aplicam:**  
  Confirme se as migrations existem em `src/Adapters/Driven/StackFood.Infra/Migrations` e se o comando `db.Database.Migrate()` está presente no `Program.cs` da API.

---

## 🏗️ Fluxo Principal da Aplicação

1. **Cliente faz pedido via API**  
   → Pedido é salvo no banco.

2. **Geração de pagamento (QR Code Mercado Pago)**  
   → API integra com Mercado Pago e retorna QR Code.

3. **Worker monitora status do pagamento**  
   → Ao ser aprovado, pedido é liberado para cozinha.

4. **Admin acompanha pedidos e gerencia produtos**  
   → Via endpoints protegidos.

---

## 🏛️ Detalhes da Arquitetura

- **Domain:**  
  Entidades como Pedido, Produto, Cliente, Pagamento.  
  Não dependem de nada externo.

- **Application:**  
  Casos de uso (ex: CriarPedido, GerarPagamento) e interfaces (ex: IOrderRepository).

- **Infra:**  
  Implementações dos repositórios, contexto do banco (AppDbContext), integrações externas (Mercado Pago).

- **Adapters Driving:**  
  API REST (controllers) e Worker (serviço background).

- **Adapters Driven:**  
  Banco de dados, Mercado Pago, outros serviços externos.

---

## 📄 Documentação

- **Swagger UI:**  
  [http://localhost:7189/swagger/index.html](http://localhost:7189/swagger/index.html)

- **Miro (Event Storming, DDD):**  
  [Acesse o Miro](https://miro.com/app/board/uXjVIFy-qVA=/?share_link_id=295945750126)

- **Trello (Kanban do projeto):**  
  [Acesse o Trello](https://trello.com/invite/b/6811409dfb1a245ff6e5c82e/ATTI57c89a0ebf7c3b36c8f4d397bad187a4A6D78212/tech-challenge)

---

## 📹 Vídeo Demonstrativo

- [Link para o vídeo](https://www.youtube.com/watch?v=5S64LJJtYUE)

O vídeo mostra a arquitetura da aplicação, como subir os containers via Docker Compose e detalhes sobre os principais fluxos.

---

## 👥 Participantes

| Nome                                           | RM       | E-mail                    | Discord          |
| ---------------------------------------------- | -------- | ------------------------- | ---------------- |
| Leonardo Duarte                                | RM364564 | leo.duarte.dev@gmail.com  | \_leonardoduarte |
| Luiz Felipe Maia                               | RM361928 | luiz.felipeam@hotmail.com | luiz_08          |
| Leonardo Luiz Lemos                            | RM364201 | leoo_lemos@outlook.com    | leoo_lemos       |
| Rodrigo Rodriguez Figueiredo de Oliveira Silva | RM362272 | rodrigorfig1@gmail.com    | lilroz           |
| Vinicius Targa Gonçalves                       | RM364425 | viniciustarga@gmail.com   | targa1765        |

---

## 💡 Observações Finais

- O projeto foi desenvolvido com foco em boas práticas de arquitetura, separação de responsabilidades e facilidade de manutenção.
- A arquitetura hexagonal permite fácil evolução e integração com novos serviços ou tecnologias.
- O uso de Docker e Docker Compose garante portabilidade e facilidade de setup para novos desenvolvedores.
