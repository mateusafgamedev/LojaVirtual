# 🛒 LojaVirtual

Aplicação web de loja virtual desenvolvida com **C# e ASP.NET Core MVC (.NET 8)**. Clientes navegam pela vitrine, pesquisam e compram produtos; administradores gerenciam produtos, usuários e acompanham o registro de vendas, com exportação para Excel.

## ✨ Funcionalidades

**Para todos os usuários**
- Cadastro de conta (cliente ou administrador), login e logout
- Senhas protegidas com **hash + salt** (nunca em texto puro)
- Sessão de usuário e filtros de acesso por cargo (`Cliente` e `Administrador`)

**Cliente**
- Vitrine de produtos com foto, categoria e preço (só aparecem produtos com estoque)
- Pesquisa por nome ou marca
- Página de detalhes do produto
- Compra de produto, com baixa automática no estoque

**Administrador**
- CRUD de produtos, com upload de foto
- Gerenciamento de usuários (listar, cadastrar e editar)
- Registro de vendas, agrupado por categoria e data, com total geral
- Relatório de vendas em **Excel (.xlsx)**
- O histórico de vendas é preservado mesmo se o produto for excluído

## 🛠️ Tecnologias

- C# / .NET 8
- ASP.NET Core MVC (Razor Views)
- Entity Framework Core 8 (Migrations)
- SQL Server 2022 em contêiner Docker
- AutoMapper
- ClosedXML (geração de planilhas)
- Bootstrap e jQuery Validation

## 🧱 Estrutura do projeto

```
LojaVirtual/                 # projeto ASP.NET Core
    ├── Controllers/             # Login, Home, Produto, Usuario, Estoque
    ├── Views/                   # Telas Razor (.cshtml) e layouts
    ├── Models/                  # Entidades do banco
    ├── DTO/                     # Objetos de transferência (usuário, produto, endereço, login)
    ├── Repository/              # Interfaces dos serviços
    ├── Services/                # Regras de negócio
    ├── Filtros/                 # Filtros de acesso (UsuarioLogado, UsuarioLogadoAdmin)
    ├── Data/                    # AppDbContext
    ├── Enums/                   # CargoEnum
    ├── Profiles/                # Mapeamentos do AutoMapper
    ├── Migrations/              # Migrations do Entity Framework
    └── wwwroot/                 # Arquivos estáticos (css, js, imagens)
```

## 🚀 Como executar

### Pré-requisitos

- [.NET SDK 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop/) em execução
- Git
- Ferramenta do Entity Framework:
  ```bash
  dotnet tool install --global dotnet-ef --version 8.*
  ```

### Passo a passo

```bash
# 1. Clone o repositório
git clone https://github.com/mateusafgamedev/LojaVirtual.git

# 2. Entre na pasta do repositório
cd LojaVirtual

# 3. Suba o banco de dados (SQL Server) no Docker
docker compose up -d

# 4. Entre na pasta do projeto
cd LojaVirtual

# 5. Crie o banco e as tabelas
dotnet ef database update

# 6. Execute a aplicação
dotnet run --launch-profile https
```

Acesse **https://localhost:7225** (ou http://localhost:5273). A aplicação abre na tela de login.

> A string de conexão (`DefaultDockerConnection`, em `appsettings.json`) já aponta para o SQL Server do `docker-compose.yml` (`localhost,1433`), então não é preciso configurar nada para rodar localmente.

### Criando o primeiro administrador

O cadastro de administrador exige um token, lido da configuração `AdminToken`. Defina um token com o *user secrets*, a partir da pasta do projeto:

```bash
dotnet user-secrets set "AdminToken" "escolha-um-token"
```

Depois, em **Cadastre-se → Cadastrar Administrador**, informe os dados e esse token. Sem o `AdminToken` configurado, o cadastro de administrador é recusado.

As categorias **Eletrônicos**, **Calçados** e **Roupas** já são criadas pelas migrations.

## 🔐 Segurança

- Senhas armazenadas com `SenhaHash` e `SenhaSalt`.
- Rotas protegidas por filtros de acesso: `UsuarioLogado` (exige login) e `UsuarioLogadoAdmin` (exige cargo de administrador).
- Cadastro de administrador protegido por token.
- As credenciais do SQL Server no `docker-compose.yml` e no `appsettings.json` são **apenas para desenvolvimento local**. Não use em produção.

## 👤 Autor

**Mateus** — [@mateusafgamedev](https://github.com/mateusafgamedev)
