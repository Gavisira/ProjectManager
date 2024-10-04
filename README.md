
# ProjectManager

Este � um projeto .NET Core estruturado em uma arquitetura em camadas, utilizando os princ�pios de CQRS na camada de aplica��o. 

## Arquitetura do Projeto

A estrutura do projeto est� organizada da seguinte forma:

- **0-Entrypoint**: Cont�m o ponto de entrada do projeto, onde a API est� definida.
- **1-Application**: Implementa a l�gica de neg�cios, incluindo os padr�es CQRS (Command Query Responsibility Segregation).
- **2-Domain**: Define as entidades de dom�nio e as regras de neg�cio.
- **3-Infrastructure**: Cont�m a implementa��o de infra-estrutura, como reposit�rios e acesso ao banco de dados SQL Server.
- **tests**: Cont�m os testes unit�rios para garantir a qualidade do c�digo.

## Configura��o da String de Conex�o

A string de conex�o com o banco de dados SQL Server � definida no arquivo `appsettings.json` do projeto `ProjectManager` (dentro da pasta `0-Entrypoint`). 
Certifique-se de atualizar essa string de conex�o de acordo com seu ambiente.
O atual docker-compose.yml j� est� configurado para utilizar a string de conex�o definida no `appsettings.json`, caso queira, basta remover o trecho:

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

Exemplo de configura��o no `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=sqlserver;Database=ProjectManagerDB;User=sa;Password=defaultPasswordByGabriel123;"
  }
}
```

## Executando o Projeto com Docker

### Pr�-requisitos

- Docker e Docker Compose instalados em sua m�quina.
- .NET SDK (opcional, caso queira executar o projeto fora do Docker).

O arquivo `docker-compose.yml` realiza a instala��o do SDK do .NET Core e executa o comando `dotnet run` para iniciar a aplica��o.

### Passos para Execu��o

1. **Clonar o reposit�rio**:
   ```bash
   git clone https://github.com/Gavisira/ProjectManager.git
   cd ProjectManager
   ```

2. **Definir a string de conex�o**: Certifique-se de que a string de conex�o no arquivo `appsettings.json` do projeto `ProjectManager` est� correta, especialmente a senha do banco de dados (`SA_PASSWORD`).

3. **Construir e executar os cont�ineres com Docker Compose**:
   Na raiz do projeto (onde est�o localizados o `docker-compose.yml` e o `Dockerfile`), execute o seguinte comando:
   
   ```bash
   docker-compose up --build
   ```

   Esse comando far� o seguinte:
   - Construir� a imagem do aplicativo .NET e configurar� o cont�iner SQL Server.
   - Executar� o comando `dotnet ef database update` dentro do cont�iner da aplica��o para atualizar o banco de dados com as migra��es definidas.

4. **Acessar a aplica��o**:
   - A aplica��o estar� dispon�vel na porta `8080`. Voc� pode acessar a API ou a interface da aplica��o em: `http://localhost:8080`.

### Outras Informa��es �teis

- **Parar os cont�ineres**:
  Para parar os cont�ineres em execu��o, pressione `Ctrl+C` no terminal onde o `docker-compose` foi iniciado ou execute:
  ```bash
  docker-compose down
  ```

- **Remover os volumes persistentes** (opcional):
  Se voc� deseja remover os dados do banco de dados persistidos, execute:
  ```bash
  docker-compose down -v
  ```

- **Executar testes**:
  Para rodar os testes unit�rios, utilize o seguinte comando:
  ```bash
  dotnet test
  ```

## Observa��es

- O arquivo `docker-compose.yml` na raiz do projeto � respons�vel por orquestrar a aplica��o e o banco de dados, garantindo que ambos sejam configurados e iniciados corretamente.
