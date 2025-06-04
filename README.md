
# EstudoPlanner 🧠📅

Sistema de planejamento de estudos desenvolvido com ASP.NET Core Web API, com autenticação via JWT, arquitetura em camadas e envio de notificações planejado para o futuro.

## 📂 Estrutura do Projeto

- **EstudoPlanner.API**: Camada de apresentação da aplicação (controllers e configuração de middlewares).
- **EstudoPlanner.BLL**: Camada de regras de negócio (serviços, autenticação, mapeamentos).
  - `Services/Auth`: Serviços de autenticação.
  - `Services/StudyPlan`: Serviços para planos de estudo.
  - `Mappings`: Configuração do AutoMapper.
- **EstudoPlanner.DAL**: Camada de acesso a dados (repos repositórios, contextos, migrations).
- **EstudoPlanner.Domain**: Entidades principais, enums e objetos de domínio.
- **EstudoPlanner.DTO**: Objetos de transferência de dados entre as camadas.
- **EstudoPlanner.Notifications**: Notificações que poderão ser usadas para alertas, erros, validações.
- **EstudoPlanner.Tasks**: Possível camada futura para tarefas agendadas (ex: lembretes por e-mail).

## 🔐 Autenticação

O projeto utiliza **JWT (JSON Web Token)** para autenticação e autorização segura dos usuários.

- Serviços:
  - `AuthService.cs`
  - `JwtTokenGenerateService.cs`
- Interfaces:
  - `IAuthService.cs`

## 📚 Funcionalidades

- Cadastro e login de usuários
- Criação de planos de estudo personalizados
- Definição de horários e disciplinas
- Arquitetura limpa e escalável com separação de responsabilidades

## 🛠️ Tecnologias Utilizadas

- ASP.NET Core Web API
- Entity Framework Core
- JWT para autenticação
- AutoMapper
- Bcrypt para criptografar senha
- C#
- Sqlite

## ▶️ Como Executar

1. Clone o repositório:
   ```bash
   git clone https://github.com/KaiqueTech/EstudoPlanner.git
   ```

2. Configure a connection string no `appsettings.json`

3. Aplique as migrations:
   ```bash
   dotnet ef database update
   OBS: "caso não encontre o projeto precisa especificar qual é o projeto que contem as informações do entity framework e qual o projeto em que esta a classe de inicialização do projeto "Program.cs", usando 
--project EstudoPlanner.DAL --startup-project EstudoPlanner.API"
   ```

4. Execute o projeto:
   ```bash
   dotnet run --project EstudoPlanner.API
   ```

## 📌 Atualizações Futuras

- [ ] Implementação de envio automático de e-mails como lembrete para os usuários.

## 🤝 Contribuições

Sinta-se à vontade para abrir *issues* ou enviar *pull requests*.

---

Feito por Kaique Bezerra.
