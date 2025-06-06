# ResgateAlerta
 API

## Descrição do Projeto

API RESTful desenvolvida em .NET para gerenciamento de  lacuna entre a detecção de um evento natural e a efetiva disseminação de um alerta acionável para a população específica em risco, utilizando arquitetura em camadas, banco de dados Oracle e documentação via Swagger.

---

## Rotas Disponíveis

### Moto
- `GET /api/moto` – Lista todas as Motos
- `GET /api/moto/{id}` – Buscar Moto por ID
- `PUT /api/moto/{id}` – Atualizar Moto
- `DELETE /api/moto/{id}` – Remover Moto

### Vaga
- `GET /api/vaga` – Lista todas as Vagas
- `GET /api/vaga/{id}` – Buscar Vaga por ID
- `POST /api/vaga` – Cadastrar nova Vaga
- `PUT /api/vaga/{id}` – Atualizar Vaga
- `DELETE /api/vaga/{id}` – Remover Vaga

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

## Instruções de Execução

1. Clone o projeto:
   ```bash
   git clone https://github.com/DGMMX/Challenger-MOTTU.git
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
   dotnet run --project Challenger-MOTTU

   ```

4. Acesse no navegador:
   - Swagger UI: https://localhost:7231/swagger/index.html

---

## 👥 Integrantes

- Diego Bassalo RM 558710 2TDSPG (Paulista)
- Lucas  RM 558506 2TDSR (Aclimação)
- Pedro Henrique Jorge De Paula RM 558833 2TDSPJ (Paulista)
