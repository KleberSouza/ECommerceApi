# ECommerceApi

## Visão Geral
Este projeto demonstra diferentes paradigmas de API (REST, GraphQL, gRPC, WebSocket, WebHook, HTTP Stream) aplicados no contexto de um E-Commerce.

## Estrutura do Projeto
- **ECommerceApi**: Projeto principal contendo as APIs.
- **ECommerceApi.Tests**: Projeto de testes unitários para validar o comportamento das APIs.

## Requisitos
- .NET 8.0
- Visual Studio 2022
- xUnit para testes unitários

## Configuração do Projeto
1. Clone o repositório.
2. Abra a solução `ECommerceApi.sln` no Visual Studio.
3. Configure a conexão com o banco de dados no arquivo `appsettings.json`.

## Executando o Projeto
- Para rodar o projeto, utilize o comando `dotnet run` na raiz do projeto `ECommerceApi`.
- Para rodar os testes unitários, utilize o comando `dotnet test`.

## APIs Disponíveis
### Gestão de Usuários
- `POST /api/users`: Criação de um novo usuário.
- `GET /api/users/{id}`: Retorna os detalhes de um usuário.

### Gestão de Produtos
- `POST /api/products`: Criação de um novo produto.
- `GET /api/products`: Lista todos os produtos.

## Contribuição
1. Faça um fork do repositório.
2. Crie um branch com sua feature (`git checkout -b feature/AmazingFeature`).
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`).
4. Push para o branch (`git push origin feature/AmazingFeature`).
5. Abra um Pull Request.

## Licença
Este projeto está licenciado sob os termos da licença MIT.
