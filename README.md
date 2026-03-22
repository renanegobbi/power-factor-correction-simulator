# Power Factor Correction Web Application

<a href="https://github.com/renanegobbi/power-factor-correction-simulator/blob/main/README.pt-BR.md">Leia esta página em Português</a> <img alt="Portuguese" src="https://github.com/renanegobbi/Flags/blob/main/Flag_of_Brazil.svg" width="30" height="30">
:---------

<p align="center">
  <img src="https://github.com/renanegobbi/power-factor-correction-simulator/blob/main/github/architecture.png"/>
</p>

Web application developed for **analysis and simulation of power factor correction scenarios in consumer units with photovoltaic generation**, using capacitor banks.

The system allows importing electrical measurement data and simulating different compensation strategies in order to determine the best capacitor configuration for power factor correction.

This project was developed as part of the study presented in the article:

**"Web Application for Correcting the Power Factor of a Photovoltaic System with Capacitor Bank"**

## 📄 Published Article

This project was developed as part of the article published in the scientific journal **Revista Ifes Ciência**:

GOBBI, R. E.; SOUZA, M. R.; SILVA, D. P.  
**Aplicação web para correção do fator de potência de um consumidor com geração fotovoltaica e banco de capacitores.**  
Revista Ifes Ciência, v. 10, n. 1, p. 01–25, 2023.

DOI: https://doi.org/10.36524/ric.v10i1.2787

🔗 Access the full article:  
https://ojs.ifes.edu.br/index.php/ric/article/view/2787/1258

## 💾 Software DOI

This software is archived and available at:

[![DOI](https://zenodo.org/badge/DOI/10.5281/zenodo.19164353.svg)](https://doi.org/10.5281/zenodo.19164353)


## 💻 Software Registration

This software is registered at the **Brazilian National Institute of Industrial Property (INPI)**.

Registration number:  
**BR512024003665-2**

---

# Table of Contents

- [1. Technologies and Tools](#1-technologies-and-tools)
  - [1.1 Web Application (ASP.NET Core MVC)](#11-web-application-aspnet-core-mvc)
  - [1.2 Database](#12-database)

- [2. About the Project](#2-about-the-project)
  - [2.1 Problem Context](#21-project-overview)
  - [2.2 Architecture](#22-architecture)
  - [2.3 Project Structure](#23-project-structure)
  - [2.4 Data Import](#24-data-import)
  - [2.5 Implemented Features](#25-implemented-features)

- [3. Demonstration](#3-demonstration)

- [4. How to Run](#4-how-to-run)
  - [4.1 Clone the Repository](#41-clone-the-repository)
    - [4.1.1 Access the Project Folder](#411-access-the-project-folder)
  - [4.2 Running the Application](#42-running-the-application)
    - [4.2.1 Visual Studio](#421-visual-studio)
    - [4.2.2 .NET CLI](#422-net-cli)
    - [4.2.3 Running with Docker](#423-running-with-docker)

- [5. License](#5-license)

---

# 1. Technologies and Tools

## 1.1 Web Application (ASP.NET Core MVC)

The application was developed using **ASP.NET Core MVC**, following a layered architecture that separates responsibilities between **presentation layer, business logic, and data access**.

Technologies used:

- [.NET 5](https://dotnet.microsoft.com/) - Platform used to develop the application.
- [ASP.NET Core MVC](https://learn.microsoft.com/aspnet/core) - Framework used to build the web application using the Model-View-Controller pattern.
- [Entity Framework Core](https://learn.microsoft.com/ef/core/) - ORM used for database access and data manipulation.
- [SQLite](https://www.sqlite.org/index.html) - Relational database used for local data persistence.
- [FluentValidation](https://docs.fluentvalidation.net/) - Library used to define validation rules for application entities.
- [EPPlus](https://github.com/EPPlusSoftware/EPPlus) - Library used for reading and processing Excel files imported into the system.
- [Newtonsoft.Json](https://www.newtonsoft.com/json) - Library used for JSON serialization and data manipulation.
- [xUnit](https://xunit.net/) - Framework used for unit testing.
- [FluentAssertions](https://fluentassertions.com/introduction) - .NET extension library that helps write more readable and expressive unit tests.

Tools used:

- [Visual Studio](https://visualstudio.microsoft.com/) - IDE used to develop and run the application.
- [DBeaver Community](https://dbeaver.io/download/) - Cross-platform database tool used for database management.
- [Docker](https://www.docker.com/) - Used for containerizing the application and running it in isolated environments.
- [Git](https://git-scm.com/) - Version control system used in the project.

## 1.2 Database

The application uses **SQLite** to store data imported by the user.

The input data corresponds to records exported from electrical energy analyzers, containing information such as:

- Active power
- Reactive power
- Apparent power
- Power factor
- Measurement date and time

These data are used by the system to simulate power factor correction scenarios.

---

# 2. About the Project

## 2.1 Project Overview

Consumers connected to medium-voltage distribution systems may face financial penalties when the **power factor of the installation falls below the minimum limit required by electrical regulations**.

In installations with **photovoltaic generation**, the reduction of active power consumption can decrease the power factor and result in charges for **excess reactive energy**.

This application allows users to:

- import electrical measurement data
- identify periods with low power factor
- simulate correction scenarios using capacitor banks
- determine the best capacitor configuration for power factor correction

## 2.2 Architecture

The application follows a layered architecture separating responsibilities between:

- Web Application
- Business Logic
- Data Persistence
- Automated Tests


## 2.3 Project Structure

```
🗂️ power-factor-correction-simulator
│
├── src
│ ├── CorrecaoFp.App
│ │
│ │ → Application presentation layer
│ │ → Controllers
│ │ → Views (Razor)
│ │ → Models and ViewModels
│ │ → File upload and processing
│ │
│ ├── CorrecaoFp.Business
│ │
│ │ → Business logic layer
│ │ → Services
│ │ → Interfaces
│ │ → Models
│ │ → Enums
│ │
│ ├── CorrecaoFp.Data
│ │
│ │ → Data access layer
│ │ → DbContext
│ │ → Repositories
│ │ → Migrations
│ │
│ └── CorrecaoFp.Business.Tests
│
├── docker
│ → Application container configuration
│
├── sql
│ → Database creation scripts
│
├── tests
│ → Automated test structure
│
└── CorrecaoFp.sln
```

## 2.4 Data Import

The application allows importing electrical measurement data through **Excel spreadsheets**.

The import template available in the system contains the following fields:

| Field | Description |
|------|-------------|
| Start date | Measurement start date |
| End date | Measurement end date |
| Active power | Measured active power |
| Reactive power | Measured reactive power |
| Apparent power | Apparent power |
| Power factor | Measured power factor |

These data are used to simulate reactive power compensation scenarios.


## 2.5 Implemented Features

The application supports the following operations:

### Measurement Import

Upload spreadsheets containing electrical measurement data.

### Data Storage

Imported data are stored in the database for later analysis.

### Power Factor Analysis

Identification of records where the power factor is below the required minimum value.

### Scenario Simulation

Simulation of different compensation strategies using capacitor banks.

### Reactive Power Calculation

Calculation of the reactive power required for power factor correction.

---

# 3. Demonstration

The application includes three main pages that represent the system workflow: **Data**, **Settings**, and **Documentation**.


## Data Page

This page is responsible for **importing and analyzing electrical measurement data** obtained from the energy analyzer.

Users can:

- download the Excel import template
- upload measurement data files
- view imported data in table format
- apply filters by period and power factor
- define the power factor compensation strategy

<p align="center">
  <img src="https://github.com/renanegobbi/power-factor-correction-simulator/blob/main/github/data-page.png"/>
</p>


## Settings Page

This page allows configuring **electrical system parameters and capacitor bank settings** used in the simulation.

Configurable parameters include:

- line voltage
- current transformer ratio (CT)
- C/K ratio
- number of capacitor banks
- compensation stage power

These parameters are used by the system to perform power factor correction calculations.

<p align="center">
  <img src="https://github.com/renanegobbi/power-factor-correction-simulator/blob/main/github/settings-page.png"/>
</p>


## Documentation Page

The application also includes an integrated **documentation section** that provides a user guide for the system.

This page explains:

- overall system functionality
- data import steps
- capacitor bank configuration
- interpretation of simulation results

<p align="center">
  <img src="https://github.com/renanegobbi/power-factor-correction-simulator/blob/main/github/documentation-page.png"/>
</p>

---

# 4. How to run

### 4.1 Clone the Repository

***Prerequisite:***  
- [Git](https://git-scm.com/) — to clone the repository

**Step:**
```bash
git clone https://github.com/renanegobbi/power-factor-correction-simulator.git
```

### 4.1.1 Access the Project Folder

**Step:**

```bash
cd power-factor-correction-simulator
```

## 4.2 Running the Application

After cloning the repository and accessing the project root folder as described in **4.1** and **4.1.1**, there are three ways to run the application:

### 4.2.1 Visual Studio

***Prerequisite:*** 

- [.NET 5 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/5.0) installed
- Visual Studio 2019 or later

**Steps:**

1. Open the solution (**CorrecaoFp.sln**) in **Visual Studio**.
2. In Solution Explorer, locate the project **CorrecaoFp.App** and set it as the startup project.
3. In the top menu, select the execution profile (e.g., **CorrecaoFp.App**).
4. Click the **Run/Play** button to start.
5. The application will start and can be accessed at: https://localhost:5001

### 4.2.2 .NET CLI

***Prerequisite:*** 

- [.NET 5 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/5.0) installed

**Steps:**

Open the terminal and run the following commands:
```bash
cd src/CorrecaoFp.App
```
```bash
dotnet restore
```
```bash
dotnet run --launch-profile "CorrecaoFp.App"
```
After the application starts, the API will be available at: https://localhost:5001

### 4.2.3 Running with Docker

***Prerequisites:***

- [Docker 20.10 or later](https://docs.docker.com/get-docker/) installed
- [Docker Compose](https://docs.docker.com/compose/) available (already included in Docker Desktop; on Linux it may require separate installation)

The application can also be run using **Docker containers**, allowing the system to run in an isolated environment without needing to install .NET locally.

**Steps:**

To start the application using Docker, run the following commands:
```bash
cd docker
```
```bash
docker-compose -f correcaofp_teste.yml -p dev up --build -d
```
After the containers start, the application will be available in the browser at: https://localhost:5001

---

# 5. License
This project is licensed under the MIT License. See the [LICENÇA](https://github.com/renanegobbi/power-factor-correction-simulator/blob/main/LICENSE) file for more information.
