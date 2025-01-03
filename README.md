# 🌐 Sistema Web de Gestão

**Ano de Desenvolvimento:** 2024  

**Desenvolvido por:** Tiago Daltro Duarte  

**Tecnologias:** ASP.NET Core MVC, .NET 8.0, SQL Server, Entity Framework

---

## 1. Introdução

Este documento descreve o desenvolvimento de um **Sistema Web de Gestão**, desenvolvido com **ASP.NET Core MVC** e **.NET 8.0**, utilizando **SQL Server** para o banco de dados. A aplicação é modular, escalável e segue as melhores práticas de arquitetura de software. A solução foi construída para possibilitar a gestão de usuários e tarefas de forma eficiente e organizada.

---

## 2. Arquitetura do Sistema

A arquitetura adotada foi baseada em uma estrutura de **três camadas**, com o uso de **arquitetura limpa** para garantir a separação de responsabilidades, manutenibilidade e flexibilidade para futuras expansões.

### 2.1 Estrutura do Projeto

- **Camada de Business (BibliotecaBusiness):** Contém a lógica de negócio.
- **Camada de Data (BibliotecaData):** Responsável pela interação com o banco de dados.
- **Camada Web (SistemaWebGestaoMVC):** Contém os controladores, views e mapeamento de dados para ViewModels.

### 2.2 Arquitetura Limpa

- **Interfaces:** Foram criadas interfaces para promover a **injeção de dependências**, garantindo que a camada de negócios não dependesse diretamente da camada de infraestrutura.
- **Serviços:** Foram implementados serviços para encapsular os casos de uso da aplicação.
- **Repositórios:** Cada repositório é responsável pelo acesso a dados, proporcionando uma interface limpa entre a aplicação e o banco de dados.

---

## 3. Banco de Dados e Modelagem

### 3.1 Entidades

1. **Usuários:**
   - **Atributos:**
     - `Id`, `Nome`, `Nascimento`, `Telefone Fixo`, `Celular`, `E-mail`, `Endereço`, `Foto`.
   - **Perfis/Role:** SuperAdmin, Gestor e Subordinado.
   - **Gestores:** Identificados como usuários com privilégios para cadastrar outros usuários.

2. **Tarefas:**
   - **Atributos:**
     - `Id`, `Mensagem`, `Data Limite para Execução`, `Status da Tarefa`, `Criador_ID` , `Subordinado_ID` .

### 3.2 Relacionamento entre Usuários e Tarefas

- Um **usuário** pode criar várias **tarefas**.
- Uma **tarefa** pode ser atribuída a apenas um **usuário subordinado**.

Relacionamento:  
**Usuário (0, 1) --- (0, n) Tarefas**

### 3.3 Configuração do Banco de Dados

- **Pacotes Instalados:**
  - `Microsoft.EntityFrameworkCore 8.0.11`
  - `Microsoft.EntityFrameworkCore.Tools 8.0.11`
  - `Microsoft.EntityFrameworkCore.SqlServer 8.0.11`
  - `Microsoft.AspNetCore.Identity.EntityFrameworkCore 8.0.11`

### 3.4 Comandos de Migrations e UpdateDatabase

Para a criação e configuração do banco de dados, foi utilizada a abordagem de **Migrations** do **Entity Framework Core**, permitindo que as alterações no modelo de dados sejam refletidas no banco de dados de forma controlada. Abaixo estão os passos e comandos utilizados:

1. **Criação da Migration:**

   O primeiro passo para gerar as migrações é criar a **migration**. O comando utilizado para isso foi:

- `Add-Migration Initial -o Migrations`
- `Update-Database`

---

## 4. Funcionalidades e Serviços

### 4.1 Roles Criadas e Atribuídas

1. **GestorAdmin:** Super Administrador (role de maior privilégio).
2. **Gestor:** Outros gestores, com permissões específicas.
3. **Subordinado:** Usuário que executa tarefas atribuídas.

### 4.2 Serviços Implementados

- **Serviço para Criação e Atribuição de Roles**
- **Serviço para Criação de Usuário:** Para criar usuários com diferentes roles.
- **Serviço de Cadastro de Usuário:** Para registrar novos usuários no sistema.
- **Serviço de Consulta de Usuário:** Para visualizar informações sobre os usuários cadastrados.
- **Serviço de Cadastro de Tarefas:** Para permitir a criação de novas tarefas.
- **Serviço de Consulta de Tarefas:** Para consultar as tarefas criadas.
- **Serviço de Atualização de Status de Tarefas:** Para atualizar o status de uma tarefa.

### 4.3 Gestor Operacional Interno

Foi criado um **Gestor Operacional Interno** para facilitar a administração inicial. O e-mail e senha são configurados ao criar a conta de superadmin.

---

## 5. Arquitetura do Projeto

### 5.1 Camada de Business

- **BibliotecaBusiness:** Contém as abstrações e a lógica de negócio da aplicação.
  - **Abstractions**: Interfaces que definem contratos de serviços.
  - **Services**: Implementações concretas dos serviços.
  - **Models**: Modelos utilizados na lógica de negócio.
  - **Exceptions**: Classes de exceção para controle de erros.

### 5.2 Camada de Data

- **BibliotecaData:** Responsável pela configuração do banco e acesso a dados.
  - **Data**: Contém as classes que manipulam os dados.
  - **Migrations**: Arquivos de migração do banco de dados.
  - **TableConfig**: Configurações das tabelas e relacionamentos.

### 5.3 Camada Web

- **SistemaWebGestaoMVC:** Camada que interage diretamente com o usuário.
  - **Controllers**: Controladores que gerenciam as interações do usuário.
  - **Mappers**: Mapeamento entre modelos e ViewModels.
  - **Models & ViewModels**: Modelos de dados e ViewModels para as Views.
  - **Views**: Interfaces de usuário, com formulários e apresentação das informações.
  - **Appsettings.json**: Arquivo de configuração da aplicação.
  - **Program.cs**: Arquivo de configuração do pipeline da aplicação.

---

## 6. Front-End

### 6.1 Formulários e Telas

- **Cadastro de Usuário**
- **Consulta de Usuário**
- **Cadastro de Tarefa**
- **Consulta de Tarefas**
- **Login**

### 6.2 Views

Utilização do **Razor** para renderização das Views, garantindo uma separação clara entre a lógica de apresentação e o código. Isso facilita a manutenção e a expansão da interface.

---

## 7. Autenticação e Autorização com Identity

### 7.1 Configuração do Identity

- **Passos para Utilização da Classe Identity:**
  1. **Adicionar Pacotes Necessários**: Instalar o pacote `Microsoft.AspNetCore.Identity.EntityFrameworkCore`.
  2. **Configurar o Banco de Dados**: Configuração do banco de dados com Identity.
  3. **Configurar o Identity no Pipeline**: Adicionar e configurar Identity no pipeline da aplicação.
  4. **Adicionar Migrations**: Criar migrações e atualizar o banco.
  5. **Proteger Controladores ou Ações**: Utilização de filtros de autorização.
  6. **Páginas de Login e Registro**: Implementação das páginas de login e registro para autenticação de usuários.
  7. **Configuração das Views**: Configuração das views de login e registro.

---

## 8. Próximos Passos

- **Notificações por E-mail:** A funcionalidade de notificações por e-mail está planejada, mas não implementada. A arquitetura modular permite que ela seja facilmente adicionada no futuro.
- **Integração do Tailwind CSS:** A integração com o Tailwind CSS e seus componentes visuais está planejada, mas não foi possível implementá-la até o momento.
- **Melhorias na Interface:** A interface será melhorada, com a adição de componentes responsivos e um layout mais moderno.

---


## 10. Conclusão

Este documento descreve o processo de desenvolvimento de um **Sistema Web de Gestão** modular, escalável e organizado. A aplicação segue boas práticas de arquitetura e está pronta para futuras expansões e melhorias. A estrutura da aplicação permite que funcionalidades adicionais sejam implementadas de maneira simples e eficiente, garantindo a continuidade no desenvolvimento do sistema.
