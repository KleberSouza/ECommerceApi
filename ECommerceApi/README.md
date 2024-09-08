# ECommerceApi

## Vis�o Geral
Este projeto demonstra diferentes paradigmas de API (REST, GraphQL, gRPC, WebSocket, WebHook, HTTP Stream) aplicados no contexto de um E-Commerce.

## Estrutura do Projeto
- **ECommerceApi**: Projeto principal contendo as APIs.
- **ECommerceApi.Tests**: Projeto de testes unit�rios para validar o comportamento das APIs.

## Requisitos
- .NET 8.0
- Visual Studio 2022
- xUnit para testes unit�rios

## Configura��o do Projeto
1. Clone o reposit�rio.
2. Abra a solu��o `ECommerceApi.sln` no Visual Studio.
3. Configure a conex�o com o banco de dados no arquivo `appsettings.json`.

## Executando o Projeto
- Para rodar o projeto, utilize o comando `dotnet run` na raiz do projeto `ECommerceApi`.
- Para rodar os testes unit�rios, utilize o comando `dotnet test`.

## APIs Dispon�veis
### Gest�o de Usu�rios
- `POST /api/users`: Cria��o de um novo usu�rio.
- `GET /api/users/{id}`: Retorna os detalhes de um usu�rio.

### Gest�o de Produtos
- `POST /api/products`: Cria��o de um novo produto.
- `GET /api/products`: Lista todos os produtos.

## Contribui��o
1. Fa�a um fork do reposit�rio.
2. Crie um branch com sua feature (`git checkout -b feature/AmazingFeature`).
3. Commit suas mudan�as (`git commit -m 'Add some AmazingFeature'`).
4. Push para o branch (`git push origin feature/AmazingFeature`).
5. Abra um Pull Request.

## Licen�a
Este projeto est� licenciado sob os termos da licen�a MIT.
