# Rota Viagens

## Decisões de design

Seguem abaixo a descrição das decisões de design adotadas.

### Clean Architecture

Foi a abordagem escolhida por ser a que dá mais ênfase à lógica de casos de uso do sistema. Por se tratar de um sistema pequeno, sem um domínio complexo e sem a necessidade momentânea de integrações complexas, embora haja potencial disso evoluir futuramente, a Clean Architecture atende bem ao cenário proposto no teste.

A Clean Architecture organiza o código de forma a torná-lo altamente modular, testável e independente de detalhes externos, como frameworks, bancos de dados e interfaces de usuário. Ela usa princípios como:

- Independência de Camadas: Camadas internas não dependem de camadas externas, mas o contrário é permitido. Regras de negócios (features/use cases) não conhecem detalhes técnicos como bancos de dados ou APIs.
- Inversão de Dependência: Camadas internas definem interfaces (contratos) que são implementadas pelas camadas externas. A comunicação entre as camadas deve seguir a direção das camadas mais externas para as mais internas.
- Modularidade: Cada camada tem uma responsabilidade bem definida, facilitando manutenção e adição de novas funcionalidades.
- Testabilidade: As camadas internas (casos de uso e entidades) são isoladas, permitindo testes unitários independentes de dependências externas.
- Flexibilidade: Troca de frameworks ou tecnologias (ex.: mudar banco de dados ou UI) sem impacto no núcleo do sistema.
- Estrutura de diretórios: Possui uma estrutura de diretórios bem organizada. Pastas separadas para cada camada (domain, data, aplicattion...) e padrões de nomenclatura claros.
- Evolução do Código: Facilita a refatoração e a evolução do projeto. Minimiza o impacto de mudanças. Serve como ponto de partida para utilizar outras abordagens mais robustas como Onion Architecture ou Hexagonal (Ports and Adapters).

### Estrutura de diretórios

#### API

Possui as controllers, com as chamadas para as feaures/use cases do sistema.

![Estrutura pasta API - Camada API](solution-items/prints/estrutura-api-rotaviagem-api.png)

#### Application

Mantém a lógica de negócio de cada feaure/use case do sistema. As features estão organizadas por entidades de domínio do negócio.

![Estrutura pasta API - Camada Application](solution-items/prints/estrutura-api-rotaviagem-application.png)

#### Domain

Aqui são implementadas as classes que representam entidades, value objects e outras regras específicas do domínio da aplicação.

![Estrutura pasta API - Camada Domain](solution-items/prints/estrutura-api-rotaviagem-domain.png)

#### Data

Representa as classes que realizam acesso ao banco de dados e realizam as operações de leitura e escrita.

![Estrutura pasta API - Camada Data](solution-items/prints/estrutura-api-rotaviagem-data.png)

#### Infra CrossCutting

Implementa os injeções de dependência e recursos comuns a todas as camadas da aplicação.

![Estrutura pasta API - Camada Infra Cross Cuiiting](solution-items/prints/estrutura-api-rotaviagem-infra-crosscutting-ioc.png)

#### Tests

Implementa os testes da aplicação.

![Estrutura pasta Tests](solution-items/prints/estrutura-tests.png)

#### Tests

Possui os arquivos necessários para preparação de ambiente de desenvolvimento e manutenção de documentação.

![Estrutura pasta Solution Items](solution-items/prints/estrutura-solution-items.png)

## Instruções para execução da aplicação

Pré-requisitos:

- Docker e Docker Compose instalados

Para iniciar a aplicação pela primeira vez, basta:

- [Iniciar aplicação executando o arquivo 'env-start.bat'](solution-items/env-start.bat)
- [Executar o script de criação de banco de dados no SQL Server](solution-items/scripts/init.sql)
- [Acessar a URL da documentação Swagger da API via browser](localhost:5001/swagger/index.html)

Informações para acesso a recursos:

- SQL Server: (server: localhost | port: 1433 | user:sa | password: 123Aa321 | database: RotaViagem)
- API: 127.0.0.1:5001/swagger/index.html ou localhost:5001/swagger/index.html

Para parar a aplicação, basta:

- [Parar aplicação executando o arquivo 'env-start.bat'](solution-items/env-stop.bat)
