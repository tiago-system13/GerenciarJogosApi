# GerenciarJogosApi
É uma api que tem como objetivo gerenciar jogos emprestados para amigos.  

Manual de montagem de ambiente de desenvolvimento.

## Conteúdo

-[Pré-requisitos](#pré-requisitos)
- [Configuração](#configuração)
- [Execução](#execução)
- [LaunchSettings](#launchSettings)

**Atenção**

> Todos os passos desta documentação são obrigatórios, sendo imprescindível que você obtenha sucesso na realização de cada passo.

> Nesta documentação considero que você está utilizando o SO Windows 10. Caso esteja utilizando outro sistema operacional, faça as devidas adaptações.

## Pré-requisitos

É necessário que você tenha instalado em sua máquina:

- [.Net Core](https://dotnet.microsoft.com/download) (_3.1_)
  _A instalação deve anteceder os próximos passos ou pode ser feita através do visual studio installer caso opte por usar a IDE, adicionando o pacote .Net Core._

- Recomendo a IDE [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/) (_2019 ou superior_) ou o editor de texto [Visual Studio Code](https://code.visualstudio.com/download)

- A instalação do banco de dados [MySql](https://dev.mysql.com/downloads/mysql/) (5.7.22 ou superior_)

> No  projeto GerenciadorDeJogos.Infrastructure existe a pasta db que contém os scripts de criação de tabelas.
  > Apos concluir a instalação do MySql é necessário executar esses scripts na seguinte ordem:
  1. v1_0_0_create_database.sql
  2. v1_0_1_create_table.sql
  3. v1_0_2_insert_usuario.sql

## Configuração

### Ambiente

- Enquanto o projeto estiver em ambiente de desenvolvimento os valores abaixo deverão permanecer como foram previamente configurados

- **Caso esteja utilizando o Visual Studio**

  > Clicando com o botão direito no projeto GerenciadorDeJogos.Api e selecionando a opção propriedades, será aberto o menu de propriedades do projeto em questão, selecionando a opção depurar é possível encontrar as variáveis do ambiente.
  > Enquanto a variável `ASPNETCORE_ENVIRONMENT` estiver com o valor `Development`, o projeto irá iniciar com as configurações de desenvolvimento, caso o valor seja alterado as configurações de inicialização também sofrerão alterações. Valores possíveis para a variável são: `Development e Production`
  

## Execução

O projeto Enquete está dividido em módulos, o módulo GerenciadorDeJogos.Api é o ponto de entrada da aplicação, os módulos GerenciadorDeJogos.Application, GerenciadorDeJogos.Domain, GerenciadorDeJogos.Infrastructure e o GerenciadorDeJogos.Test é o projeto que contém os testes.

**Atenção**

- **Caso esteja utilizando o Visual Studio**

> Neste momento o seu Visual Studio já deve estar configurado com o .NET Core.


1. Abra o arquivo GerenciadorDeJogos.Api.sln_ com o Visual Studio.

3. Execute a aplicação a partir do projeto GerenciadorDeJogos.Api, utilizando o menu que se encontra no topo da tela clicando no botão play.

> No navegador padrão da máquina será aberto uma página no endereço `https://localhost:44335/swagger/index.html`, o endpoint apresentará uma página com a documentação da Api. 

## LaunchSettings

No projeto GerenciadorDeJogos.Api, é necessário a criação de um arquivo `launchSettings.json` na pasta `Properties` para que o projeto seja executado corretamente e tenha todas as variáveis de ambiente. Para isso basta criar a pasta Properties no projeto GerenciadorDeJogos.Api, adicionar o arquivo launchSettings.json na pasta e colar o json abaixo.

```json
{
  "iisSettings": {
    "windowsAuthentication": false, 
    "anonymousAuthentication": true, 
    "iisExpress": {
      "applicationUrl": "http://localhost:51497",
      "sslPort": 44335
    }
  },
  "profiles": {
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "ISSUER": "Jogos",
        "AUDIENCE": "Jogos",
        "TEMPOEXPIRACAOTOKEN": "30"
      }
    },
    "GerenciadorDeJogos.Api": {
      "commandName": "Project",
      "launchBrowser": true,
      "applicationUrl": "https://localhost:5001;http://localhost:5000",
      "environmentVariables": {
       "ASPNETCORE_ENVIRONMENT": "Development",
       "ISSUER": "Jogos",
        "AUDIENCE": "Jogos",
        "TEMPOEXPIRACAOTOKEN": "30"
      }
    }
  }
}

