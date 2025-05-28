# 🧾 StackFood API

Sistema Backend para gerenciamento de pedidos e produtos em uma lanchonete com autoatendimento, desenvolvido como parte do **Tech Challenge** da formação em Arquitetura de Software.

---

## 📋 Descrição do Projeto

O **StackFood API** visa solucionar o problema de desorganização no atendimento de uma lanchonete em expansão. Com foco no autoatendimento, o sistema permite que clientes realizem pedidos personalizados, paguem via QR Code (Mercado Pago) e acompanhem o status do pedido em tempo real, enquanto o administrador pode gerenciar produtos, clientes e acompanhar os pedidos em andamento.

---

## 🎯 Funcionalidades

- **Cadastro e Identificação de Clientes**
  - Cadastro com nome e e-mail
  - Identificação via CPF
  - Pedido anônimo (sem identificação)

- **Montagem de Combos**
  - Lanche
  - Acompanhamento
  - Bebida
  - Sobremesa

- **Gerenciamento Administrativo**
  - Cadastro/edição de produtos
  - Categorias fixas (lanche, acompanhamento, bebida, sobremesa)
  - Acompanhamento de pedidos e tempo de espera

- **Processamento de Pedidos**
  - Envio para cozinha com etapas:
    - Recebido
    - Em preparação
    - Pronto
    - Finalizado

- **Pagamento**
  - Integração com QR Code do Mercado Pago (fake checkout para MVP)

---

## 🛠️ Tecnologias Utilizadas

- **Linguagem:** C# (.NET)
- **Banco de Dados:** PostgreSQL
- **Arquitetura:** Hexagonal (Ports & Adapters)
- **Documentação de API:** Swagger / OpenAPI
- **Containerização:** Docker
- **Orquestração:** Docker Compose

---

## 🚀 Como Executar Localmente

### Pré-requisitos

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

### Passos

```bash
# Clone o repositório
git clone https://github.com/Stack-Food/stackfood-api.git
cd stackfood-api

# Suba o ambiente com Docker Compose
docker-compose up --build
```

## 📄 Documentação
A Documentação do sistema (DDD) com Event Storming, incluindo todos os passos/tipos de diagrama mostrados na aula 6 do módulo de DDD, pode ser acessada abaixo:

### Miro
https://miro.com/welcomeonboard/R1VpcjhVdnp5WkIyVmRjcjI1dlpyU2xVWGs1VjUzV1JBMW52RXovSnpUUFh1cE1TdndXTUtCUDhlZkNzbXo1K1N5ajRnUTUvelBQSVIveVpEOC84dWhDSnZtLzEyWUZ2UVoxSUkzV1loczdHU2FHVG9UYjYrM0dUNUphSy9lWHd0R2lncW1vRmFBVnlLcVJzTmdFdlNRPT0hdjE=?share_link_id=29384969431

### Trello
https://trello.com/invite/b/6811409dfb1a245ff6e5c82e/ATTI57c89a0ebf7c3b36c8f4d397bad187a4A6D78212/tech-challenge

## 📹 Vídeo Demonstrativo
### 🔗 Link para o vídeo: 
https://youtube.com

O vídeo mostra a arquitetura da aplicação, como subir os containers via Docker Compose e detalhes sobre os principais fluxos.

## 👥 Participantes
- Luiz 
- Leonardo Duarte
- Leonardo Lemos
- Rodrigo Rodrigues
- Vinicius Targa
