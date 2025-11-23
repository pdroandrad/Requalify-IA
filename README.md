# Requalify – API .NET + IA Generativa (Deep Learning)

## 📌 Descrição do Projeto

O **Requalify** é uma API RESTful desenvolvida como parte da Global Solution, integrada ao tema **O Futuro do Trabalho**.

A aplicação possibilita que candidatos em busca de recolocação profissional se cadastrem, acessem cursos indicados, acompanhem notícias do mercado e visualizem vagas de interesse.

Nesta entrega, o sistema foi ampliado com uma **camada de IA Generativa**, permitindo que o usuário receba recomendações inteligentes de cursos compatíveis com sua área e seu nível profissional.

A IA é integrada diretamente à API e utiliza **OpenAI (GPT)** para gerar recomendações automáticas.

---

## 👨‍💻 Integrantes

- Pedro Abrantes Andrade | RM558186  
- Ricardo Tavares de Oliveira Filho | RM556092  
- Victor Alves Carmona | RM555726

---

## 🤖 Funcionalidades relacionadas à IA (Deep Learning / Generative AI)

### ✔ API dedicada à recomendação inteligente
Endpoint:

**POST /api/v1/ia/recomendacoes**

### ✔ Input do usuário:

```json
{
  "areaDeInteresse": "Tecnologia",
  "nivelExperiencia": "Iniciante"
}
```
✔ Output retornado pela IA:

- texto descritivo
- sugestões relevantes
- coerente com a área de atuação
- adaptado ao perfil do usuário

**Exemplo de retorno:**
```json
{
  "recomendacao": "Sugiro que você inicie com cursos de lógica de programação..."
}
```
✔ Integração direta com modelo OpenAI GPT

- Requisição via **API REST**
- Uso real de **modelo de linguagem**
- Resultado consumido diretamente pela aplicação

---

## 🚀 Funcionalidades Implementadas na API

### 🔹 Arquitetura

- **ASP.NET 8 Web API**
- **Versionamento v1/v2**
- Middleware de **API Key**
- **IA Generativa usando GPT**
- **Health Check**
- **Swagger interativo**
- **Paginação**
- **HATEOAS completo**

### 🔹 Entidades

- Usuário
- Curso
- Vaga
- Notícia

### 🔹 Banco de Dados

- EF Core 8
- Oracle DB

### 🔹 Testes

- Testes unitários
- Testes de integração

---

## 🧠 Fluxo resumido da IA

- Usuário informa área e nível
- API envia prompt ao GPT
- Modelo processa e gera recomendações
- API retorna resposta inteligente ao frontend

---

## 🧰 Tecnologias Utilizadas

- **ASP.NET Core 8**
- **C#**
- **EF Core**
- **Oracle Database**
- **Swagger**
- **xUnit**
- **OpenAI GPT**
- **Visual Studio 2022**

---

## 📡 Endpoint da IA

### 🔧 Recomendação de Cursos

| Método | Endpoint                    |
|:------:|-----------------------------|
| POST   | `/api/v1/ia/recomendacoes`  |

**Payload de requisição:**

```json
{
  "areaDeInteresse": "Tecnologia",
  "nivelExperiencia": "Intermediário"
}
```

**Header obrigatório:**

```
X-API-KEY: requalify-2025
```

## 📂 Instalação e Execução

### 1️⃣ Clonar o Repositório

```bash
git clone https://github.com/pdroandrad/Requalify-IA.git

```
### 2️⃣ Ajustar o arquivo `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=oracle.fiap.com.br:1521/orcl;User Id=SEU_RM;Password=SUA_SENHA;"
  },
  "ApiKey": "requalify-2025"
}
```

### 3️⃣ Configurar a IA

No arquivo:

`/Properties/launchSettings.json`

Adicionar:

```json
"OPENAI_API_KEY": "SUA_CHAVE_AQUI"
```

### 4️⃣ Rodar

Pressione **F5**

O Swagger abrirá automaticamente.

## 🔐 Autenticação

No Swagger:

Clique **Authorize**

Insira:

```
requalify-2025
```

