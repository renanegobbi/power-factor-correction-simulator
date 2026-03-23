# Power Factor Correction Web Application

<a href="https://github.com/renanegobbi/power-factor-correction-simulator/blob/main/README.md">Read this page in English</a> <img alt="English" src="https://github.com/renanegobbi/Flags/blob/main/Flag_of_the_United_States.svg" width="30" height="30"> 
:---------

<p align="center">
  <img src="https://github.com/renanegobbi/power-factor-correction-simulator/blob/main/github/architecture.png"/>
</p>

Aplicação web desenvolvida para **análise e simulação de cenários de correção do fator de potência em unidades consumidoras com geração fotovoltaica**, utilizando bancos de capacitores.

O sistema permite importar dados de medição elétrica e simular diferentes estratégias de compensação para identificar a melhor configuração de capacitores para correção do fator de potência.

Este projeto foi desenvolvido como parte do estudo apresentado no artigo:

**"Web Application for Correcting the Power Factor of a Photovoltaic System with Capacitor Bank"**

## 📄 Artigo publicado

Este projeto foi desenvolvido como parte do artigo publicado no periódico científico **Revista Ifes Ciência**:

GOBBI, R. E.; SOUZA, M. R.; SILVA, D. P.  
**Aplicação web para correção do fator de potência de um consumidor com geração fotovoltaica e banco de capacitores.**  
Revista Ifes Ciência, v. 10, n. 1, p. 01–25, 2023.  

[![DOI](https://img.shields.io/badge/DOI-10.36524%2Fric.v10i1.2787-blue)](https://doi.org/10.36524/ric.v10i1.2787) 

🔗 Acesse o artigo completo:  

👉 Clique no botão abaixo para acessar o artigo:

[![Article](https://img.shields.io/badge/Full%20Article-Access-green)](https://ojs.ifes.edu.br/index.php/ric/article/view/2787/1258)

## 💾 DOI do Software

Este software está arquivado e disponível em:

[![DOI](https://zenodo.org/badge/DOI/10.5281/zenodo.19164353.svg)](https://doi.org/10.5281/zenodo.19164353)

## 💻 Registro de Software

Este software possui registro no **Instituto Nacional da Propriedade Industrial (INPI)**.

Número de registro:  
**BR512024003665-2**

---

# Sumário

- [1. Tecnologias e ferramentas](#1-tecnologias-e-ferramentas)
  - [1.1 Backend (Aplicação Web)](#11-aplicação-web-aspnet-core-mvc)
  - [1.2 Banco de dados](#12-banco-de-dados)

- [2. Sobre o projeto](#2-sobre-o-projeto)
  - [2.1 Contexto do problema](#21-explicação-do-projeto)
  - [2.2 Arquitetura](#22-arquitetura)
  - [2.3 Estrutura do projeto](#23-estrutura-do-projeto)
  - [2.4 Importação de dados](#24-importação-de-dados)
  - [2.5 Funcionalidades implementadas](#25-funcionalidades-implementadas)

- [3. Demonstração](#3-demonstração)

- [4. Como executar](#4-como-executar)
  - [4.1 Clonar o repositório](#41-clonar-o-repositório)
    - [4.1.1 Acessar a pasta do projeto](#411-acessar-a-pasta-do-projeto)
  - [4.2 Executando a aplicação](#42-executando-a-aplicação)
    - [4.2.1 Visual Studio](#421-visual-studio)
    - [4.2.2 .NET CLI](#422-net-cli)
    - [4.2.3 Executando com Docker](#423-executando-com-docker)

- [5. Licença](#5-licença)

---

# 1. Tecnologias e ferramentas

## 1.1 Aplicação Web (ASP.NET Core MVC)

A aplicação foi desenvolvida utilizando **ASP.NET Core MVC**, seguindo uma arquitetura em camadas para separação das responsabilidades entre **camada de apresentação, regras de negócio e acesso a dados**.

Tecnologias utilizadas:

- [.NET 5](https://dotnet.microsoft.com/) - Plataforma utilizada para desenvolvimento da aplicação.
- [ASP.NET Core MVC](https://learn.microsoft.com/aspnet/core) - Framework utilizado para desenvolvimento da aplicação web utilizando o padrão Model-View-Controller.
- [Entity Framework Core](https://learn.microsoft.com/ef/core/) - ORM utilizado para acesso e manipulação do banco de dados.
- [SQLite](https://www.sqlite.org/index.html) - Banco de dados utilizado para persistência local dos dados da aplicação.
- [FluentValidation](https://docs.fluentvalidation.net/) - Biblioteca utilizada para definição de regras de validação das entidades da aplicação.
- [EPPlus](https://github.com/EPPlusSoftware/EPPlus) - Biblioteca utilizada para leitura e processamento de arquivos Excel importados pelo sistema.
- [Newtonsoft.Json](https://www.newtonsoft.com/json) - Biblioteca utilizada para serialização e manipulação de dados em formato JSON.
- [xUnit](https://xunit.net/) - framework utilizado para teste de unidade.
- [FluentAssertions](https://fluentassertions.com/introduction) - conjunto de métodos de extensão .NET que ajuda a escrever teste de unidade com mais produtividade e legibilidade.

Ferramentas utilizadas:

- [Visual Studio](https://visualstudio.microsoft.com/) - IDE utilizada para desenvolvimento e execução da aplicação.
- [DBeaver Community](https://dbeaver.io/download/) - Ferramenta de banco de dados multiplataforma para desenvolvedores.
- [Docker](https://www.docker.com/) - Utilizado para containerização da aplicação e execução em ambientes isolados.
- [Git](https://git-scm.com/) - Sistema de controle de versão utilizado no projeto.


## 1.2 Banco de dados

A aplicação utiliza **SQLite** para armazenamento das informações importadas pelo usuário.

Os dados de entrada correspondem aos registros exportados de analisadores de energia elétrica, contendo informações como:

- Potência ativa
- Potência reativa
- Potência aparente
- Fator de potência
- Data e hora das medições

Esses dados são utilizados pelo sistema para realizar as simulações de correção do fator de potência.

---

# 2. Sobre o projeto

## 2.1 Explicação do projeto

Consumidores conectados em média tensão podem sofrer penalidades financeiras quando o **fator de potência da instalação fica abaixo do limite mínimo exigido pela regulamentação elétrica**.

Em instalações com **geração fotovoltaica**, a redução do consumo de potência ativa pode provocar redução do fator de potência e gerar cobranças por **energia reativa excedente**.

Esta aplicação permite:

- importar dados de medições elétricas
- identificar períodos com baixo fator de potência
- simular cenários de correção utilizando bancos de capacitores
- identificar a melhor configuração para correção do fator de potência

# 2.2 Arquitetura

A aplicação foi estruturada utilizando uma arquitetura em camadas, separando responsabilidades entre:

- Aplicação Web
- Regras de negócio
- Persistência de dados
- Testes automatizados

# 2.3 Estrutura do projeto

```
🗂️ power-factor-correction-simulator
│
├── src
│ ├── CorrecaoFp.App
│ │
│ │ → Camada de apresentação da aplicação
│ │ → Controllers
│ │ → Views (Razor)
│ │ → Models e ViewModels
│ │ → Upload e processamento de arquivos
│ │
│ ├── CorrecaoFp.Business
│ │
│ │ → Camada de regras de negócio
│ │ → Serviços
│ │ → Interfaces
│ │ → Models
│ │ → Enums
│ │
│ ├── CorrecaoFp.Data
│ │
│ │ → Camada de acesso a dados
│ │ → DbContext
│ │ → Repositórios
│ │ → Migrations
│ │
│ └── CorrecaoFp.Business.Tests
│
├── docker
│ → Configurações de containers da aplicação
│
├── sql
│ → Scripts de criação do banco de dados
│
├── tests
│ → Estrutura de testes automatizados
│
└── CorrecaoFp.sln
```


# 2.4 Importação de dados

A aplicação permite importar dados de medições elétricas através de **planilhas Excel**.

O template de importação está disponível no sistema e contém os seguintes campos:

| Campo | Descrição |
|------|-----------|
| Data inicial | Data de início da medição |
| Data final | Data final da medição |
| Potência ativa | Potência ativa medida |
| Potência reativa | Potência reativa medida |
| Potência aparente | Potência aparente |
| Fator de potência | Fator de potência medido |

Esses dados são utilizados para simular cenários de compensação de potência reativa.


# 2.5 Funcionalidades implementadas

A aplicação permite realizar as seguintes operações:

### Importação de medições

Upload de planilhas contendo dados de medições elétricas.

### Armazenamento de dados

Os dados importados são armazenados no banco de dados para posterior análise.

### Análise de fator de potência

Identificação de registros onde o fator de potência está abaixo do valor mínimo.

### Simulação de cenários

Simulação de diferentes estratégias de compensação utilizando bancos de capacitores.

### Identificação da potência reativa necessária

Cálculo da potência reativa necessária para correção do fator de potência.

---

# 3. Demonstração

A aplicação possui três principais telas responsáveis pelo fluxo de utilização do sistema: **Dados**, **Configurações** e **Documentação**.

## Página de Dados

Esta tela é responsável pela **importação e análise dos dados elétricos** obtidos do analisador de energia.

Nela o usuário pode:

- baixar o template Excel utilizado para importação
- realizar upload do arquivo contendo os dados de medições
- visualizar os dados importados em formato de tabela
- aplicar filtros por período e fator de potência
- definir a estratégia de compensação do fator de potência

<p align="center">
  <img src="https://github.com/renanegobbi/power-factor-correction-simulator/blob/main/github/data-page.png"/>
</p>


## Página de Configurações

Esta tela permite configurar os **parâmetros do sistema elétrico e dos bancos de capacitores** utilizados na simulação.

Entre os parâmetros configuráveis estão:

- tensão da linha
- relação do transformador de corrente (TC)
- relação C/K
- quantidade de bancos de capacitores
- potência de cada estágio de compensação

Essas configurações são utilizadas pelo sistema para realizar os cálculos de correção do fator de potência.

<p align="center">
  <img src="https://github.com/renanegobbi/power-factor-correction-simulator/blob/main/github/settings-page.png"/>
</p>


## Página de Documentação

A aplicação também possui uma seção de **documentação integrada**, que apresenta um guia de utilização do sistema.

Essa página descreve:

- funcionamento geral da aplicação
- etapas de importação dos dados
- configuração dos bancos de capacitores
- interpretação dos resultados gerados

<p align="center">
  <img src="https://github.com/renanegobbi/power-factor-correction-simulator/blob/main/github/documentation-page.png"/>
</p>

---

# 4. Como executar

### 4.1 Clonar o repositório

***Pré-requisito:*** 
- [Git](https://git-scm.com/) — para clonar o repositório

**Passo:**
```bash
git clone https://github.com/renanegobbi/power-factor-correction-simulator.git
```

### 4.1.1 Acessar a pasta do projeto

**Passo:**

```bash
cd power-factor-correction-simulator
```

## 4.2. Executando a aplicação

Após clonar o repositório e acessar a pasta raiz do projeto conforme os passos **4.1** e **4.1.1**, existem três formas de executar a aplicação:

### 4.2.1 Visual Studio

***Pré-requisitos:*** 

- [.NET 5 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/5.0) instalado
- Visual Studio 2019 ou superior

**Passos:**

1. Abra a solução  (**CorrecaoFp.sln**) no **Visual Studio**
2. No Solution Explorer, localizar o projeto **CorrecaoFp.App** e o defina como projeto de inicialização.
3. No menu superior, selecionar o perfil de execução (por exemplo, **CorrecaoFp.App**).
4. Clicar no botão **Run/Play** para iniciar.
5. A aplicação iniciará e poderá ser acessada em:: https://localhost:5001

### 4.2.2 .NET CLI

***Pré-requisito:*** 

- [.NET 5 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/5.0) instalado

**Passos:**

Abra o terminal e execute os seguintes comandos:
```bash
cd src/CorrecaoFp.App
```
```bash
dotnet restore
```
```bash
dotnet run --launch-profile "CorrecaoFp.App"
```
Após a inicialização do aplicativo, a API estará disponível em: https://localhost:5001

### 4.2.3 Executando com Docker

***Pré-requisitos:***

- [Docker 20.10 ou superior](https://docs.docker.com/get-docker/) instalado
- [Docker Compose](https://docs.docker.com/compose/) disponível (já incluído no Docker Desktop; em Linux pode exigir instalação separada)

A aplicação também pode ser executada utilizando **containers Docker**, permitindo rodar o sistema em um ambiente isolado sem necessidade de instalar o .NET localmente.

**Passos:**

Para iniciar a aplicação utilizando Docker, execute os seguintes comandos:
```bash
cd docker
```
```bash
docker-compose -f correcaofp_teste.yml -p dev up --build -d
```
Após a inicialização dos containers, a aplicação estará disponível no navegador no endereço: https://localhost:5001


---

# 5. Licença
Este projeto está sob a licença do MIT. Consulte a [LICENÇA](https://github.com/renanegobbi/power-factor-correction-simulator/blob/main/LICENSE) para obter mais informações.

