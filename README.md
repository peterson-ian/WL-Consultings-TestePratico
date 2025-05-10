# WL-Consultings-TestePratico

## Estrutura da Solução  

A solução está organizada da seguinte forma:  

- **WL-Consultings-TestePratico.csproj**: Arquivo de projeto principal que contém as definições e dependências do projeto.  
    - Separacao de responsabilidade;
    - UnityOfWork;
    - JWT;
    - SOLID;
- **Dockerfile**: Arquivo de configuração para criação da imagem Docker do projeto.  
- **.NET 8**: O projeto utiliza o .NET 8 como framework principal.  

## Executando o Projeto com Docker Compose  

Para executar o projeto utilizando o Docker Compose, siga os passos abaixo:  

1. Certifique-se de que o Docker e o Docker Compose estão instalados em sua máquina.  

2. Certifique-se de estar na pasta da solução, se não estiver:
```bash
  cd /WL-Consultings-TestePratico
```

3. Iniciar os serviços com Docker Compose
```bash
  docker-compose up -d
```

4. A API está rodando em http://localhost:8080/, e sua documentação em http://localhost:8080/swagger
