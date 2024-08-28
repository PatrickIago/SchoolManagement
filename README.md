# SchoolManagement

# Overview

Este é um sistema de gerenciamento escolar desenvolvido como uma API RESTful utilizando .NET 8. A API oferece funcionalidades para gerenciar estudantes, incluindo operações de CRUD (Create, Read, Update, Delete).

# Tecnologias Usadas

Back-End

.NET 8: Framework para construir a API RESTful.

Entity Framework Core: ORM para interação com o banco de dados.

SQL Server: Banco de dados relacional utilizado.

Swagger/OpenAPI: Documentação interativa da API.

AutoMapper: Mapeamento entre entidades e DTOs.

Dependency Injection: Injeção de dependências nativa do ASP.NET Core.

FluentValidation: Validação de dados das entidades.

JWT (JSON Web Tokens): Para autenticação e autorização segura de usuários.

ASP.NET Core Identity: Sistema de gerenciamento de identidade e autenticação.

# Funcionalidades

Gerenciamento de Estudantes: Cadastro, visualização, atualização e exclusão de estudantes.

Documentação da API: Documentação interativa com Swagger.

Autenticação e Autorização: Protege endpoints utilizando JWT e ASP.NET Core Identity para garantir segurança e controle de acesso.

Como Rodar o Projeto

# Pré-requisitos

.NET 8 SDK: Para rodar a API.

SQL Server: Para o banco de dados.

# Configuração do Ambiente

Clone o Repositório:

Abra o terminal e execute o seguinte comando para clonar o repositório para o seu ambiente local:

git clone https://github.com/PatrickIago/SchoolManagement.git

Configuração do Banco de Dados:

Certifique-se de ter o SQL Server instalado e configurado.

Atualize a string de conexão no arquivo appsettings.json para apontar para seu servidor SQL Server.

# Executar as Migrações:

Abra o terminal e navegue até o diretório do projeto.

Execute as migrações para configurar o banco de dados:

dotnet ef database update

# Executar o Projeto:

Inicie a aplicação:

dotnet run

Acessar a Documentação da API:

Abra um navegador e vá para http://localhost:5000/swagger para acessar a documentação interativa da API gerada pelo Swagger.

# Configuração de Autenticação:

Registre um usuário utilizando o endpoint de registro.

Utilize o token JWT retornado para autenticar suas requisições à API.

# Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues, fazer pull requests ou sugerir melhorias.

