
# ProjectManager

Este é um projeto .NET Core estruturado em uma arquitetura em camadas, utilizando os princípios de CQRS na camada de aplicação. 

## Arquitetura do Projeto

A estrutura do projeto está organizada da seguinte forma:

- **0-Entrypoint**: Contém o ponto de entrada do projeto, onde a API está definida.
- **1-Application**: Implementa a lógica de negócios, incluindo os padrões CQRS (Command Query Responsibility Segregation).
- **2-Domain**: Define as entidades de domínio e as regras de negócio.
- **3-Infrastructure**: Contém a implementação de infra-estrutura, como repositórios e acesso ao banco de dados SQL Server.
- **tests**: Contém os testes unitários para garantir a qualidade do código.

## Configuração da String de Conexão

A string de conexão com o banco de dados SQL Server é definida no arquivo `appsettings.json` do projeto `ProjectManager` (dentro da pasta `0-Entrypoint`). 
Certifique-se de atualizar essa string de conexão de acordo com seu ambiente.
O atual docker-compose.yml já está configurado para utilizar a string de conexão definida no `appsettings.json`, caso queira, basta remover o trecho:

``` yaml
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: sqlserver
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=defaultPasswordByGabriel123
    ports:
      - "1433:1433"
    volumes:
      - sql_data:/var/opt/mssql
```

e

``` yaml
volumes:
  sql_data:
```

Exemplo de configuração no `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=sqlserver;Database=ProjectManagerDB;User=sa;Password=defaultPasswordByGabriel123;"
  }
}
```

## Executando o Projeto com Docker

### Pré-requisitos

- Docker e Docker Compose instalados em sua máquina.
- .NET SDK (opcional, caso queira executar o projeto fora do Docker).

O arquivo `docker-compose.yml` realiza a instalação do SDK do .NET Core e executa o comando `dotnet run` para iniciar a aplicação.

### Passos para Execução

1. **Clonar o repositório**:
   ```bash
   git clone https://github.com/Gavisira/ProjectManager.git
   cd ProjectManager
   ```

2. **Definir a string de conexão**: Certifique-se de que a string de conexão no arquivo `appsettings.json` do projeto `ProjectManager` está correta, especialmente a senha do banco de dados (`SA_PASSWORD`).

3. **Construir e executar os contêineres com Docker Compose**:
   Na raiz do projeto (onde estão localizados o `docker-compose.yml` e o `Dockerfile`), execute o seguinte comando:
   
   ```bash
   docker-compose up --build
   ```

   Esse comando fará o seguinte:
   - Construirá a imagem do aplicativo .NET e configurará o contêiner SQL Server.
   - Executará o comando `dotnet ef database update` dentro do contêiner da aplicação para atualizar o banco de dados com as migrações definidas.

4. **Acessar a aplicação**:
   - A aplicação estará disponível na porta `8080`. Você pode acessar a API ou a interface da aplicação em: `http://localhost:8080`.

### Outras Informações Úteis

- **Parar os contêineres**:
  Para parar os contêineres em execução, pressione `Ctrl+C` no terminal onde o `docker-compose` foi iniciado ou execute:
  ```bash
  docker-compose down
  ```

- **Remover os volumes persistentes** (opcional):
  Se você deseja remover os dados do banco de dados persistidos, execute:
  ```bash
  docker-compose down -v
  ```

- **Executar testes**:
  Para rodar os testes unitários, utilize o seguinte comando:
  ```bash
  dotnet test
  ```

## Observações

- O arquivo `docker-compose.yml` na raiz do projeto é responsável por orquestrar a aplicação e o banco de dados, garantindo que ambos sejam configurados e iniciados corretamente.
