# 🛒 LojaVirtual

Aplicação web de loja virtual desenvolvida em **C# com ASP.NET Core MVC**, com autenticação de usuários, cadastro com endereço e controle de acesso por cargo.

## ✨ Funcionalidades

- Cadastro de usuário com dados pessoais e endereço
- Login com senha protegida por **hash + salt**
- Controle de sessão do usuário logado
- Níveis de acesso por cargo (`CargoEnum`)
- Validação de formulários com Data Annotations (`[Required]`, etc.)
- Mensagens de erro e sucesso na interface
- Layouts separados para usuário logado e deslogado

## 🛠️ Tecnologias

- C# / .NET `[versão, ex.: 8.0]`
- ASP.NET Core MVC (Razor Views)
- Entity Framework Core
- `[Banco de dados, SQL Server]`
- Docker / Docker Compose (SQL Server em container)
- Bootstrap

## 🧱 Arquitetura

O projeto segue o padrão MVC, com camadas de serviço e DTOs:

```
LojaVirtual/
├── Controllers/   # Recebem as requisições e retornam as Views
├── Views/         # Telas em Razor (.cshtml)
├── Models/        # Entidades do banco (UsuarioModel, EnderecoModel...)
├── DTO/           # Objetos de transferência de dados (LoginUsuarioDto, CadastrarUsuarioDto...)
├── Enum/          # Enumeradores (CargoEnum)
├── Services/      # Regras de negócio (UsuarioService, autenticação, sessão)
├── Data/          # DbContext e configuração do banco
└── Migrations/    # Migrations do Entity Framework
```

<!-- Ajuste os nomes das pastas conforme a estrutura real do repositório -->

## 🚀 Como executar

### Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) `[versão]`
- `[Banco de dados escolhido]`
- Git

### Passo a passo

```bash
# 1. Clone o repositório
git clone https://github.com/mateusafgamedev/LojaVirtual.git

# 2. Entre na pasta do projeto
cd LojaVirtual

# 3. Restaure as dependências
dotnet restore
```

**4. Configure a string de conexão** no arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "sua-string-de-conexao-aqui"
  }
}
```

> O nome da chave (`DefaultConnection`) deve ser o mesmo usado no `Program.cs`.

```bash
# 5. Aplique as migrations para criar o banco
dotnet ef database update

# 6. Execute a aplicação
dotnet run
```

Depois, acesse o endereço exibido no terminal (geralmente `https://localhost:xxxx`).

## 🔐 Segurança

- As senhas **não** são armazenadas em texto puro: cada usuário tem `SenhaHash` e `SenhaSalt`.
- Os formulários são validados no servidor via `ModelState`.

## 👤 Autor

**Mateus** — [@mateusafgamedev](https://github.com/mateusafgamedev)
