# Rota Viagens

## Estrutura projeto (Visual Studio)

![Estrutura Projeto VS](solution-items/prints/estrutura.png)

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
- Evolução do Código: Facilita a refatoração e a evolução do projeto. Minimiza o impacto de mudanças. Serve como ponto de partida para utilizar outras abordagens mais robustas como Onion Architecture ou Hexagonal (Ports and Adapters).

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
