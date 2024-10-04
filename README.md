
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


## Perguntas para o PO - Refinamento da Segunda Fase

Para refinar e planejar futuras implementa��es ou melhorias, seria interessante discutir os seguintes pontos com o Product Owner (PO):

1. **Buscar Tasks por filtro**:
   - Quais filtros seriam necess�rios? (Ex: data, status, criada entre (data inicial e final), etc.)
   - Os filtros devem ser combinados? Por exemplo, buscar por status "Em andamento" e respons�vel espec�fico ao mesmo tempo.
   - A busca por filtros deve ser dispon�vel apenas para administradores ou para todos os usu�rios?

2. **Gerar Relat�rio de Conclus�o de Tasks por Per�odo**:
   - Quais m�tricas e informa��es s�o importantes para incluir no relat�rio? (Ex: conclu�das no prazo, fora do prazo, atrasos m�dios, etc.)
   - � necess�rio exportar esses relat�rios? Se sim, em quais formatos? (PDF, Excel, etc.)

3. **Gerar Relat�rio de Conclus�o de Projetos por Per�odo**:
   - Quais dados s�o relevantes para os relat�rios de conclus�o de projetos? (Ex: tarefas conclu�das, tempo total, desvios do prazo, etc.)
   - O relat�rio deve incluir informa��es detalhadas de cada task ou apenas um resumo do projeto?
   - A gera��o de relat�rios deve ser feita manualmente ou pode ser automatizada em intervalos espec�ficos?

4. **Adicionar Arquivos �s Tasks**:
   - H� algum limite de tamanho ou tipo de arquivo que deve ser respeitado?
   - Os arquivos anexados devem ser versionados? (Ex: substituir ou manter vers�es anteriores ao adicionar um novo arquivo)
   - � necess�rio rastrear quem fez o upload de cada arquivo?
   - Os arquivos devem ser armazenados localmente ou em um servi�o de armazenamento externo?
   - Quem pode visualizar ou baixar os arquivos anexados? Isso deve ser controlado por permiss�es?

5. **Adicionar Links Relacionados �s Tasks**:
   - Que tipo de links devem ser suportados? (URLs, links internos para outras tasks ou projetos, etc.)
   - Os links precisam ser validados antes de serem aceitos?
   - Deve haver alguma categoriza��o para os links? (Ex: documenta��o, refer�ncias, recursos externos)

6. **Adicionar Data de In�cio e Fim aos Projetos**:
   - A data de in�cio e fim dos projetos deve ser fixa ou pode ser alterada durante o andamento do projeto?
   - Qual ser� o comportamento do sistema se a data de conclus�o de uma task ultrapassar a data de fim do projeto?
   - Precisamos de notifica��es ou alertas se um projeto estiver chegando ao seu prazo final?

Essas perguntas ajudam a entender melhor os requisitos e expectativas, facilitando o planejamento e implementa��o das melhorias para a pr�xima fase do projeto.


## Melhorias Sugeridas para a Terceira Fase

Para a terceira fase do projeto, sugerimos v�rias melhorias e implementa��es que podem trazer benef�cios significativos em termos de arquitetura, desempenho e disponibilidade do sistema. Abaixo est�o algumas �reas identificadas e as melhorias propostas:

1. **Migra��o para a Nuvem (Cloud Enablement)**:
   - Migrar a aplica��o para um ambiente de nuvem, como AWS, Azure ou Google Cloud, pode trazer in�meros benef�cios, como escalabilidade, gerenciamento simplificado e servi�os adicionais de monitoramento e seguran�a.
   - Utilizar servi�os PaaS (Platform as a Service) para hospedagem, banco de dados e armazenamento de arquivos, reduzindo a complexidade de gerenciamento de infraestrutura.

2. **Autentica��o e Dados do Usu�rio com `HttpContext`**:
   - Implementar o uso do `HttpContext` para capturar e utilizar os dados do usu�rio nas opera��es da aplica��o. Isso permitir� que as opera��es sejam rastreadas e personalizadas de acordo com o usu�rio autenticado, melhorando a seguran�a e a experi�ncia do usu�rio.
   - Integrar com provedores de identidade (ex: Azure AD, IdentityServer) para uma gest�o mais eficiente de autentica��o e autoriza��o.

3. **Application Performance Monitoring (APM)**:
   - Integrar uma solu��o de APM, como Application Insights (Azure), New Relic ou Dynatrace, para monitorar o desempenho da aplica��o em tempo real.
   - O APM permitir� identificar gargalos, monitorar m�tricas de desempenho, e visualizar a experi�ncia do usu�rio final, ajudando a manter a aplica��o em funcionamento com alta efici�ncia.

4. **Orquestra��o de Cont�ineres com Kubernetes**:
   - Implementar a orquestra��o de cont�ineres com Kubernetes (K8s) para gerenciar, escalar e implementar a aplica��o de maneira eficiente.
   - Configurar pods, deployments e services para garantir que a aplica��o possa ser escalada horizontalmente e tenha toler�ncia a falhas.

5. **Redund�ncia e Alta Disponibilidade**:
   - Implementar redund�ncia na infraestrutura para garantir que a aplica��o continue funcionando mesmo em caso de falhas.
   - Utilizar m�ltiplas inst�ncias da aplica��o em diferentes regi�es ou zonas de disponibilidade (caso esteja na nuvem) para garantir alta disponibilidade.

6. **An�lise de Disponibilidade e Sa�de da Aplica��o**:
   - Implementar um sistema de an�lise e monitoramento da disponibilidade da aplica��o, integrando com solu��es de monitoramento da nuvem (ex: Azure Monitor, AWS CloudWatch).
   - Adicionar endpoints de health check para verificar a integridade dos componentes cr�ticos da aplica��o, como conex�o com o banco de dados, acesso � rede, etc.
   - Configurar o Kubernetes para utilizar os health checks na orquestra��o, garantindo que inst�ncias problem�ticas sejam automaticamente reiniciadas ou substitu�das.

7. **Implementa��o de Health Checks**:
   - Adicionar uma rota `/health` na aplica��o que retorne o status da aplica��o (UP/DOWN) e verifique componentes-chave, como banco de dados e servi�os externos.
   - Utilizar bibliotecas como `AspNetCore.Diagnostics.HealthChecks` para uma implementa��o completa e detalhada do status da aplica��o.

### Vis�o do Projeto para Arquitetura e Cloud

- A transi��o para a nuvem e a ado��o de servi�os de cloud computing devem ser planejadas de acordo com a criticidade e a complexidade do sistema. Recomenda-se come�ar com a migra��o da aplica��o para um ambiente de nuvem IaaS (Infraestrutura como Servi�o) e, posteriormente, evoluir para PaaS (Plataforma como Servi�o) conforme necess�rio.
- A aplica��o pode ser aprimorada para seguir uma arquitetura baseada em microsservi�os, que facilita a escalabilidade, a manuten��o e o desenvolvimento de novos recursos. Isso permitir� a implementa��o mais eficiente de novos padr�es, como orquestra��o com Kubernetes e integra��o com APMs.
