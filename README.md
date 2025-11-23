# Requalify – API .NET (Global Solution)

## 📌 Descrição do Projeto

O **Requalify** é uma API RESTful desenvolvida como parte da Global Solution, voltada para o tema **O Futuro do Trabalho**.

A API tem como objetivo fornecer uma plataforma moderna de requalificação profissional, permitindo:

- Cadastro e gestão de usuários  
- Cadastro de cursos e áreas profissionais  
- Associação entre usuários e cursos  
- Busca e cadastro de vagas de emprego  
- Exibição de notícias do mercado de trabalho  

---

## 👨‍💻 Integrantes

- Pedro Abrantes Andrade | RM558186  
- Ricardo Tavares de Oliveira Filho | RM556092  
- Victor Alves Carmona | RM555726

---

## 🚀 Funcionalidades Implementadas

A API inclui:

### 🔹 Arquitetura e Recursos
- Versionamento **v1**
- HATEOAS aplicado em todas as entidades
- Middleware de **API Key**
- **Health Check** em `/health`
- Paginação em endpoints de listagem

### 🔹 CRUD Completo
Para as entidades:
- Usuário  
- Curso  
- Vaga  
- Notícia  

### 🔹 Documentação
- **Swagger** com suporte ao envio de API Key

### 🔹 Testes Automatizados
- Testes **unitários** e **de integração** com xUnit

### 🔹 Banco de Dados
- **EF Core + Oracle**

---

## 🚀 Tecnologias Utilizadas

- **ASP.NET Core 8 – Web API**
- **C#**
- **Entity Framework Core 8**
- **Oracle EF Core**
- **xUnit**
- **WebApplicationFactory**
- **Swagger / OpenAPI**
- **Visual Studio 2022**
- **.NET 8**

## ✅ Funcionalidades da Entrega

### ✔ Health Check

Verifica se a API está ativa:
```
GET /health
```
### ✔ Middleware de API Key

Todos os endpoints (exceto **/health**) exigem o header:
```
X-API-KEY: requalify-2025
```

### ✔ Versionamento de API (v1)

A API utiliza versionamento por atributo:

```csharp
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/usuario")]
```

### ✔ Paginação

Todos os GETs principais aceitam:
```
?pageNumber=1&pageSize=10
```

### ✔ HATEOAS

Todas as entidades retornam links de navegação:

```json
"links": [
  { "rel": "self", "href": "...", "method": "GET" },
  { "rel": "update", "href": "...", "method": "PUT" },
  { "rel": "delete", "href": "...", "method": "DELETE" }
]
```

### ✔ Testes Automatizados

- Testes **unitários** com EF InMemory  
- Testes **de integração** com `WebApplicationFactory`  
- Teste de **Health Check**  
- Teste do endpoint de **Vagas**

## 📂 Instalação e Execução

### 🔧 Pré-requisitos

- **.NET 8**
- **Visual Studio 2022**
- **Banco Oracle ativo**

### 1️⃣ Clonar o repositório

```bash
git clone https://github.com/pdroandrad/requalify-dotnet-api
```

### 2️⃣ Ajustar o *appsettings.json*

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=oracle.fiap.com.br:1521/orcl;User Id=SEU_RM;Password=SUA_SENHA;"
},
"ApiKey": "requalify-2025"
```

### 3️⃣ Rodar a API

Basta pressionar **F5** no Visual Studio.

O Swagger abrirá automaticamente.

---

## 🔐 Autenticação no Swagger

1. Clique em **Authorize**
2. Digite a API Key:
```
requalify-2025
```

## 📡 Endpoints Principais (v1)

---

## 🔧 Usuários

| Método | Endpoint               | Descrição              |
|--------|-------------------------|-------------------------|
| GET    | /api/v1/usuario         | Lista usuários          |
| GET    | /api/v1/usuario/{id}    | Usuário com HATEOAS     |
| POST   | /api/v1/usuario         | Cria usuário            |
| PUT    | /api/v1/usuario/{id}    | Atualiza                |
| DELETE | /api/v1/usuario/{id}    | Remove                  |

---

## 🔧 Cursos

| Método | Endpoint               | Descrição              |
|--------|-------------------------|-------------------------|
| GET    | /api/v1/curso           | Lista cursos            |
| GET    | /api/v1/curso/{id}      | Curso com HATEOAS       |
| POST   | /api/v1/curso           | Cria curso              |
| PUT    | /api/v1/curso/{id}      | Atualiza                |
| DELETE | /api/v1/curso/{id}      | Remove                  |

---

## 🔧 Vagas

| Método | Endpoint               | Descrição              |
|--------|-------------------------|-------------------------|
| GET    | /api/v1/vaga            | Lista vagas             |
| GET    | /api/v1/vaga/{id}       | Vaga com HATEOAS        |
| POST   | /api/v1/vaga            | Cria vaga               |
| PUT    | /api/v1/vaga/{id}       | Atualiza                |
| DELETE | /api/v1/vaga/{id}       | Remove                  |

---

## 🔧 Notícias

| Método | Endpoint                 | Descrição               |
|--------|---------------------------|--------------------------|
| GET    | /api/v1/noticia           | Lista notícias           |
| GET    | /api/v1/noticia/{id}      | Detalhe + HATEOAS        |
| POST   | /api/v1/noticia           | Cria notícia             |
| PUT    | /api/v1/noticia/{id}      | Atualiza                 |
| DELETE | /api/v1/noticia/{id}      | Remove                   |

## 📦 Exemplos de JSON (POST)

### 🧑 Usuário

```json
{
  "nome": "Ana",
  "sobrenome": "Souza",
  "email": "ana@gmail.com",
  "senha": "123",
  "tipo": "candidato"
}
```

### 🎓 Curso (JSON de exemplo – POST)

```json
{
  "titulo": "Curso de C# para Iniciantes",
  "descricao": "Aprenda os fundamentos do C# e .NET.",
  "area": "Programação"
}
```
### 💼 Vaga — Exemplo de JSON (POST)

```json
{
  "usuarioId": 1,
  "titulo": "Desenvolvedor Jr",
  "descricao": "Vaga para iniciantes em desenvolvimento."
}
```
### 📰 Notícia

```json
{
  "titulo": "Mercado de TI em alta",
  "descricao": "Projeções indicam crescimento da área."
}
```

## 🧪 Testes Automatizados

### 📍 Estrutura
```
Requalify.Tests/
 ├── Services/
 │    ├── ServicoUsuariosTests.cs
 │    ├── ServicoCursosTests.cs
 │    ├── ServicoVagasTests.cs
 │    └── ServicoNoticiasTests.cs
 └── Integrations/
      ├── ApiHealthCheckTests.cs
      └── VagasEndpointIntegrationTests.cs
```
## 📌 Como rodar

No terminal, execute:

```bash
dotnet test
```

Todos os testes usam:

- **EF Core InMemory**
- **WebApplicationFactory**
- **Ambiente isolado**

---
