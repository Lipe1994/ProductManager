Api construída em camadas(Api, Domain, Infra) com inspiração no DDD. 


Conta com:

    - Dotnet 5;
    - Testes unitários(camada de domínio e camada de infra);
    - ORM EntityFramework;
    - Repository Pattern;
    - Implementação do UnitOfWork;
    - Documentação com swagger;
    - AutoMapper.


Para executar o projeto é necessário: 
    
    - Configurar uma ConnectionString(SqlServer) no Startup;
    - Rodar as migrações, supondo que esteja dentro do projeto de Infra(ProductManager.Infra), rode o comando `dotnet ef database update -s ../ProductManager.Api`.



Link do projeto rodando em um AppService: https://producto-manager-api.azurewebsites.net/swagger/index.html


