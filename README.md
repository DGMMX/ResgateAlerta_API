# ResgateAlerta
 API

## Descrição do Projeto

API RESTful desenvolvida em .NET para gerenciamento de  lacuna entre a detecção de um evento natural e a efetiva disseminação de um alerta acionável para a população específica em risco, utilizando arquitetura em camadas, banco de dados Oracle e documentação via Swagger.

---

## Tecnologias Utilizadas

- .NET 9
- ASP.NET Core
- C#
- Entity Framework Core
- Oracle Entity Framework Core (ODP.NET)
- AutoMapper
- Swagger (Swashbuckle.AspNetCore)
- Visual Studio 2022

---

## 🔗 Endpoints por Recurso
### 👤 Usuário
Método	Rota	Descrição
GET	/api/Usuario	Lista todos os usuários
POST	/api/Usuario	Cadastra um novo usuário
GET	/api/Usuario/{id}	Detalha um usuário por ID
PUT	/api/Usuario/{id}	Atualiza dados de um usuário
DELETE	/api/Usuario/{id}	Remove um usuário

### 🏛️ Órgão Público
Método	Rota	Descrição
GET	/api/OrgaoPublico	Lista todos os órgãos públicos
POST	/api/OrgaoPublico	Cadastra um novo órgão público
GET	/api/OrgaoPublico/{id}	Detalha um órgão por ID
PUT	/api/OrgaoPublico/{id}	Atualiza dados de um órgão público
DELETE	/api/OrgaoPublico/{id}	Remove um órgão público

### 📍 Localização
Método	Rota	Descrição
GET	/api/Localizacao	Lista todas as localizações
POST	/api/Localizacao	Registra uma nova localização
GET	/api/Localizacao/{id}	Detalha uma localização por ID
PUT	/api/Localizacao/{id}	Atualiza dados de uma localização
DELETE	/api/Localizacao/{id}	Remove uma localização

### 🗺️ Estado
Método	Rota	Descrição
GET	/api/Estado	Lista todos os estados
POST	/api/Estado	Registra um novo estado
GET	/api/Estado/{id}	Detalha um estado por ID
PUT	/api/Estado/{id}	Atualiza dados de um estado
DELETE	/api/Estado/{id}	Remove um estado

### 🧾 Denúncia
Método	Rota	Descrição
GET	/api/Denuncia	Lista todas as denúncias
POST	/api/Denuncia	Registra uma nova denúncia
GET	/api/Denuncia/{id}	Detalha uma denúncia por ID
PUT	/api/Denuncia/{id}	Atualiza dados de uma denúncia
DELETE	/api/Denuncia/{id}	Remove uma denúncia

### 🏙️ Cidade
Método	Rota	Descrição
GET	/api/Cidade	Lista todas as cidades
POST	/api/Cidade	Registra uma nova cidade
GET	/api/Cidade/{id}	Detalha uma cidade por ID
PUT	/api/Cidade/{id}	Atualiza dados de uma cidade
DELETE	/api/Cidade/{id}	Remove uma cidade

### 🏘️ Bairro
Método	Rota	Descrição
GET	/api/Bairro	Lista todos os bairros
POST	/api/Bairro	Registra um novo bairro
GET	/api/Bairro/{id}	Detalha um bairro por ID
PUT	/api/Bairro/{id}	Atualiza dados de um bairro
DELETE	/api/Bairro/{id}	Remove um bairro

### 📊 Acompanhamento de Denúncia
Método	Rota	Descrição
GET	/api/AcompanhamentoDenuncia	Lista todos os acompanhamentos
POST	/api/AcompanhamentoDenuncia	Cria novo acompanhamento
GET	/api/AcompanhamentoDenuncia/{id}	Detalha um acompanhamento por ID
PUT	/api/AcompanhamentoDenuncia/{id}	Atualiza dados de um acompanhamento
DELETE	/api/AcompanhamentoDenuncia/{id}	Remove um acompanhamento

## Instruções de Execução

1. Clone o projeto:
   ```bash
   git clone https://github.com/DGMMX/ResgateAlerta_API.git
   cd challenger-mottu
   ```

2. Configure a conexão Oracle no `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "Oracle": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=SEU_SERVIDOR"
   }
   ```

3. Execute os comandos:
   ```bash
   dotnet restore
   dotnet build
   dotnet run --project ResgateAlerta_API

   ```

4. Acesse no navegador:
   - Swagger UI: https://localhost:7231/swagger/index.html

---

## 👥 Integrantes

- Diego Bassalo RM 558710 2TDSPG (Paulista)
- Lucas  RM 558506 2TDSR (Aclimação)
- Pedro Henrique Jorge De Paula RM 558833 2TDSPJ (Paulista)
