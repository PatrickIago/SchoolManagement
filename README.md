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

FluentValidation: Validação de dados das requests.

JWT (JSON Web Tokens): Para autenticação e autorização segura de usuários.

ASP.NET Core Identity: Sistema de gerenciamento de identidade e autenticação.

Docker: Para empacotar a aplicação e suas dependências em contêineres.

# Funcionalidades
Gerenciamento de Estudantes: Cadastro, visualização, atualização e exclusão de estudantes.

Documentação da API: Documentação interativa com Swagger.

Autenticação e Autorização: Protege endpoints utilizando JWT e ASP.NET Core Identity para garantir segurança e controle de acesso.

Containerização: Facilita a configuração e execução do projeto usando Docker.

# Como Rodar o Projeto

Pré-requisitos

Docker: Para construir e executar o contêiner da aplicação.

.NET 8 SDK: Opcional, apenas se você quiser compilar o projeto localmente fora do Docker.

SQL Server: Para o banco de dados, pode ser executado em um contêiner Docker também.

Configuração do Ambiente com Docker

Clone o Repositório:

Abra o terminal e execute o seguinte comando para clonar o repositório para o seu ambiente local:

git clone https://github.com/PatrickIago/SchoolManagement.git

# Configuração do Docker:

Certifique-se de ter o Docker instalado e funcionando em sua máquina.

Navegue até o diretório do projeto onde estão os arquivos Dockerfile e docker-compose.yml.

Construir e Executar o Contêiner:

No diretório do projeto, execute o comando para construir e iniciar os contêineres:

docker-compose up --build

Isso construirá as imagens necessárias e iniciará os contêineres, incluindo a API e o banco de dados (se configurado no docker-compose.yml).

# Acessar a Documentação da API:

Após o contêiner estar em execução, abra um navegador e vá para http://localhost:5000/swagger para acessar a documentação interativa da API gerada pelo Swagger.

# Configuração de Autenticação:

Registre um usuário utilizando o endpoint de registro.

Utilize o token JWT retornado para autenticar suas requisições à API.

# Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues, fazer pull requests ou sugerir melhorias.
