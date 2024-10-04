
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


## Perguntas para o PO - Refinamento da Segunda Fase

Para refinar e planejar futuras implementações ou melhorias, seria interessante discutir os seguintes pontos com o Product Owner (PO):

1. **Buscar Tasks por filtro**:
   - Quais filtros seriam necessários? (Ex: data, status, criada entre (data inicial e final), etc.)
   - Os filtros devem ser combinados? Por exemplo, buscar por status "Em andamento" e responsável específico ao mesmo tempo.
   - A busca por filtros deve ser disponível apenas para administradores ou para todos os usuários?

2. **Gerar Relatório de Conclusão de Tasks por Período**:
   - Quais métricas e informações são importantes para incluir no relatório? (Ex: concluídas no prazo, fora do prazo, atrasos médios, etc.)
   - É necessário exportar esses relatórios? Se sim, em quais formatos? (PDF, Excel, etc.)

3. **Gerar Relatório de Conclusão de Projetos por Período**:
   - Quais dados são relevantes para os relatórios de conclusão de projetos? (Ex: tarefas concluídas, tempo total, desvios do prazo, etc.)
   - O relatório deve incluir informações detalhadas de cada task ou apenas um resumo do projeto?
   - A geração de relatórios deve ser feita manualmente ou pode ser automatizada em intervalos específicos?

4. **Adicionar Arquivos às Tasks**:
   - Há algum limite de tamanho ou tipo de arquivo que deve ser respeitado?
   - Os arquivos anexados devem ser versionados? (Ex: substituir ou manter versões anteriores ao adicionar um novo arquivo)
   - É necessário rastrear quem fez o upload de cada arquivo?
   - Os arquivos devem ser armazenados localmente ou em um serviço de armazenamento externo?
   - Quem pode visualizar ou baixar os arquivos anexados? Isso deve ser controlado por permissões?

5. **Adicionar Links Relacionados às Tasks**:
   - Que tipo de links devem ser suportados? (URLs, links internos para outras tasks ou projetos, etc.)
   - Os links precisam ser validados antes de serem aceitos?
   - Deve haver alguma categorização para os links? (Ex: documentação, referências, recursos externos)

6. **Adicionar Data de Início e Fim aos Projetos**:
   - A data de início e fim dos projetos deve ser fixa ou pode ser alterada durante o andamento do projeto?
   - Qual será o comportamento do sistema se a data de conclusão de uma task ultrapassar a data de fim do projeto?
   - Precisamos de notificações ou alertas se um projeto estiver chegando ao seu prazo final?

Essas perguntas ajudam a entender melhor os requisitos e expectativas, facilitando o planejamento e implementação das melhorias para a próxima fase do projeto.


## Melhorias Sugeridas para a Terceira Fase

Para a terceira fase do projeto, sugerimos várias melhorias e implementações que podem trazer benefícios significativos em termos de arquitetura, desempenho e disponibilidade do sistema. Abaixo estão algumas áreas identificadas e as melhorias propostas:

1. **Migração para a Nuvem (Cloud Enablement)**:
   - Migrar a aplicação para um ambiente de nuvem, como AWS, Azure ou Google Cloud, pode trazer inúmeros benefícios, como escalabilidade, gerenciamento simplificado e serviços adicionais de monitoramento e segurança.
   - Utilizar serviços PaaS (Platform as a Service) para hospedagem, banco de dados e armazenamento de arquivos, reduzindo a complexidade de gerenciamento de infraestrutura.

2. **Autenticação e Dados do Usuário com `HttpContext`**:
   - Implementar o uso do `HttpContext` para capturar e utilizar os dados do usuário nas operações da aplicação. Isso permitirá que as operações sejam rastreadas e personalizadas de acordo com o usuário autenticado, melhorando a segurança e a experiência do usuário.
   - Integrar com provedores de identidade (ex: Azure AD, IdentityServer) para uma gestão mais eficiente de autenticação e autorização.

3. **Application Performance Monitoring (APM)**:
   - Integrar uma solução de APM, como Application Insights (Azure), New Relic ou Dynatrace, para monitorar o desempenho da aplicação em tempo real.
   - O APM permitirá identificar gargalos, monitorar métricas de desempenho, e visualizar a experiência do usuário final, ajudando a manter a aplicação em funcionamento com alta eficiência.

4. **Orquestração de Contêineres com Kubernetes**:
   - Implementar a orquestração de contêineres com Kubernetes (K8s) para gerenciar, escalar e implementar a aplicação de maneira eficiente.
   - Configurar pods, deployments e services para garantir que a aplicação possa ser escalada horizontalmente e tenha tolerância a falhas.

5. **Redundância e Alta Disponibilidade**:
   - Implementar redundância na infraestrutura para garantir que a aplicação continue funcionando mesmo em caso de falhas.
   - Utilizar múltiplas instâncias da aplicação em diferentes regiões ou zonas de disponibilidade (caso esteja na nuvem) para garantir alta disponibilidade.

6. **Análise de Disponibilidade e Saúde da Aplicação**:
   - Implementar um sistema de análise e monitoramento da disponibilidade da aplicação, integrando com soluções de monitoramento da nuvem (ex: Azure Monitor, AWS CloudWatch).
   - Adicionar endpoints de health check para verificar a integridade dos componentes críticos da aplicação, como conexão com o banco de dados, acesso à rede, etc.
   - Configurar o Kubernetes para utilizar os health checks na orquestração, garantindo que instâncias problemáticas sejam automaticamente reiniciadas ou substituídas.

7. **Implementação de Health Checks**:
   - Adicionar uma rota `/health` na aplicação que retorne o status da aplicação (UP/DOWN) e verifique componentes-chave, como banco de dados e serviços externos.
   - Utilizar bibliotecas como `AspNetCore.Diagnostics.HealthChecks` para uma implementação completa e detalhada do status da aplicação.

### Visão do Projeto para Arquitetura e Cloud

- A transição para a nuvem e a adoção de serviços de cloud computing devem ser planejadas de acordo com a criticidade e a complexidade do sistema. Recomenda-se começar com a migração da aplicação para um ambiente de nuvem IaaS (Infraestrutura como Serviço) e, posteriormente, evoluir para PaaS (Plataforma como Serviço) conforme necessário.
- A aplicação pode ser aprimorada para seguir uma arquitetura baseada em microsserviços, que facilita a escalabilidade, a manutenção e o desenvolvimento de novos recursos. Isso permitirá a implementação mais eficiente de novos padrões, como orquestração com Kubernetes e integração com APMs.
