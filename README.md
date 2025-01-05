# 🌐 **Sistema Web de Gestão** 🖥️

---

📅 **Ano de Desenvolvimento**: 2024  
👨‍💻 **Desenvolvido por**: Tiago Daltro Duarte  
💻 **Tecnologias**: ASP.NET Core MVC, .NET 8.0, SQL Server, Entity Framework  


## 1. Introdução

Este documento descreve o desenvolvimento de um Sistema Web de Gestão, desenvolvido com **ASP.NET Core MVC** e **.NET 8.0**, utilizando **SQL Server** para o banco de dados. A aplicação é modular, escalável e segue as melhores práticas de arquitetura de software. A solução foi construída para possibilitar a gestão de usuários e tarefas de forma eficiente e organizada.

---

## X - Configuração do ambiente e execução da aplicação

### Instale as Dependências
1. **SQL Server**  
2. **.NET 8 SDK**  
3. **Git**  

### Clone o Projeto
Usando o terminal do seu SO, clone o projeto usando:  
```bash
git clone https://github.com/Ti7801/WebManagementSystem.git
```

### Configuração do Banco de Dados
Por padrão, a aplicação irá utilizar o banco de dados de nome `WebManagementSystem`.  

A seguinte *connection string* está configurada no arquivo `src/WebManagementSystem/appsettings.json`:  
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=WebManagementSystem;Integrated Security=True;TrustServerCertificate=True;"
}
```
Se deseja utilizar outro banco de dados, é necessário substituir a *connection string* padrão.

### Migrations
Neste projeto foi utilizado o **EF Core** para gerenciar as *migrations*.  
Para criar o banco de dados e aplicar as *migrations* necessárias, execute o seguinte comando:

1. Navegue para a pasta source com o comando:  
   ```bash
   cd src/
   ```
2. Aplique as *migrations* utilizando o comando:  
   ```bash
   dotnet ef database update --context AppDbContext --project BibliotecaData --startup-project WebManagementSystem
   ```

### Executando a Aplicação
1. Navegue para `/src/WebManagementSystem/`.  
2. Compile a solução com o comando:  
   ```bash
   dotnet build
   ```
3. Execute a solução com o comando:  
   ```bash
   dotnet run
   ```
4. Acesse no navegador a URL: [http://localhost:5273](http://localhost:5273)

---

## 2. Arquitetura do Sistema

A arquitetura adotada foi baseada em uma estrutura de três camadas, com o uso de **arquitetura limpa** para garantir a separação de responsabilidades, manutenibilidade e flexibilidade para futuras expansões.

### 2.1 Estrutura do Projeto
- **Camada de Business (BibliotecaBusiness):** Contém a lógica de negócio.
- **Camada de Data (BibliotecaData):** Responsável pela interação com o banco de dados.
- **Camada Web (SistemaWebGestaoMVC):** Contém os controladores, *views* e mapeamento de dados para *ViewModels*.

### 2.2 Arquitetura Limpa
- **Interfaces:** Foram criadas interfaces para promover a injeção de dependências, garantindo que a camada de negócios não dependesse diretamente da camada de infraestrutura.
- **Serviços:** Foram implementados serviços para encapsular os casos de uso da aplicação.
- **Repositórios:** Cada repositório é responsável pelo acesso a dados, proporcionando uma interface limpa entre a aplicação e o banco de dados.

---

## 3. Banco de Dados e Modelagem

### 3.1 Entidades
1. **Usuários**  
2. **Tarefas**  
3. **IdentityUser**  
4. **IdentityRole**  

### 3.2 Relacionamento entre Usuários e Tarefas
- Um usuário pode criar várias tarefas.  
- Uma tarefa pode ser atribuída a apenas um usuário subordinado.  
- Uma tarefa possui um gestor.  

---

## 4. Funcionalidades e Serviços

### 4.1 Roles Criadas e Atribuídas
1. **GestorAdmin:** Super Administrador (*role* de maior privilégio).  
   - Permite ao usuário criar outros usuários, outros gestores e criar tarefas para atribuir aos seus subordinados.
2. **Gestor:** Outros gestores, com permissões específicas.  
   - Permite o usuário criar tarefas e atribuí-las aos seus subordinados.
3. **Subordinado:** Usuário que executa tarefas atribuídas.

### 4.2 Serviços Implementados
- **TarefaService**  
  - Caso de Uso - Cadastro de Tarefas: Para permitir a criação de novas tarefas.  
  - Caso de Uso - Serviço de Consulta de Tarefas: Para consultar as tarefas criadas.  
  - Caso de Uso - Atualização de Status de Tarefas: Para atualizar o status de uma tarefa.  

- **UsuarioService**  
  - Caso de Uso - Criação de Usuário: Para criar usuários com diferentes roles.  
  - Caso de Uso - Cadastro de Usuário: Para registrar novos usuários no sistema.  
  - Caso de Uso - Consulta de Usuário: Para visualizar informações sobre os usuários cadastrados.

- **ApplicationSetup**  
  - Caso de Uso - Criação e Atribuição de Roles.

### 4.3 Gestor Operacional Interno
Foi criado um Gestor Operacional Interno para facilitar a administração inicial. O e-mail e senha são configurados ao criar a conta de superadmin.

---

## 5. Arquitetura do Projeto

### 5.1 Camada de Business
- **BibliotecaBusiness:** Contém as abstrações e a lógica de negócio da aplicação.
  - *Abstractions:* Interfaces que definem contratos de serviços.
  - *Services:* Implementações concretas dos serviços.
  - *Models:* Modelos utilizados na lógica de negócio.
  - *Exceptions:* Classes de exceção para controle de erros.

### 5.2 Camada de Data
- **BibliotecaData:** Responsável pela configuração do banco e acesso a dados.
  - *Data:* Contém as classes que manipulam os dados.
  - *Migrations:* Arquivos de migração do banco de dados.
  - *TableConfig:* Configurações das tabelas e relacionamentos.

### 5.3 Camada Web
- **SistemaWebGestaoMVC:** Camada que interage diretamente com o usuário.
  - *Controllers:* Controladores que gerenciam as interações do usuário.
  - *Mappers:* Mapeamento entre modelos e ViewModels.
  - *Models & ViewModels:* Modelos de dados e ViewModels para as Views.
  - *Views:* Interfaces de usuário, com formulários e apresentação das informações.
  - *Appsettings.json:* Arquivo de configuração da aplicação.
  - *Program.cs:* Arquivo de configuração do pipeline da aplicação.

---

## 6. Front-End

### 6.1 Formulários e Telas
- Cadastro de Usuário
- Consulta de Usuário
- Cadastro de Tarefa
- Consulta de Tarefas
- Login

### 6.2 Views
Utilização do Razor para renderização das *Views*, garantindo uma separação clara entre a lógica de apresentação e o código. Isso facilita a manutenção e a expansão da interface.

---

## 7. Autenticação e Autorização com Identity

### 7.1 Configuração do Identity

- **Passos para Utilização da Classe Identity:**
  1. Adicionar Pacotes Necessários: Instalar o pacote `Microsoft.AspNetCore.Identity.EntityFrameworkCore`.
  2. Configurar o Banco de Dados: Configuração do banco de dados com Identity.
  3. Configurar o Identity no Pipeline: Adicionar e configurar Identity no pipeline da aplicação.
  4. Adicionar Migrations: Criar migrações e atualizar o banco.
  5. Proteger Controladores ou Ações: Utilização de filtros de autorização.
  6. Páginas de Login e Registro: Implementação das páginas de login e registro para autenticação de usuários.
  7. Configuração das Views: Configuração das views de login e registro.

---

## 8. Próximos Passos

- **Notificações por E-mail:** A funcionalidade de notificações por e-mail está planejada, mas não implementada. A arquitetura modular permite que ela seja facilmente adicionada no futuro.
- **Integração do Tailwind CSS:** O uso do Tailwind CSS e seus componentes visuais está planejado, mas não foi possível implementá-lo até o momento.
- **Melhorias na Interface:** A interface será melhorada, com a adição de componentes responsivos e um layout mais moderno.

---

## 10. Conclusão

Este documento descreve o processo de desenvolvimento de um Sistema Web de Gestão modular, escalável e organizado. A aplicação segue boas práticas de arquitetura e está pronta para futuras expansões e melhorias. A estrutura da aplicação permite que funcionalidades adicionais sejam implementadas de maneira simples e eficiente, garantindo a continuidade no desenvolvimento do sistema.
