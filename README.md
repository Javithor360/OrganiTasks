# OrganiTask 🚀

## What is OrganiTask? 📝

OrganiTask is a **desktop Kanban-style task manager** built with C# and Windows Forms. Designed for **offline-first** use, it empowers individual and community projects without relying on external services. Manage your personal to-dos or collaborative initiatives with drag‑and‑drop ease and secure local storage.

## Technology Ecosystem Overview 🔍

<p align="center">
  <a href="https://docs.microsoft.com/dotnet/csharp/"><img alt="C#" src="https://img.shields.io/badge/C%23-green?style=for-the-badge" /></a>
  <a href="https://learn.microsoft.com/dotnet/framework/winforms/"><img alt="WinForms" src="https://img.shields.io/badge/WinForms-dark_green_?style=for-the-badge" /></a>
  <a href="https://docs.microsoft.com/dotnet/framework/data/adonet/"><img alt="ADO.NET" src="https://img.shields.io/badge/ADO.NET-yellow?style=for-the-badge" /></a>
  <a href="https://www.nuget.org/packages/BCrypt.Net-Next/"><img alt="BCrypt" src="https://img.shields.io/badge/BCrypt-orange?style=for-the-badge" /></a>
  <a href="https://www.microsoft.com/sql-server/"><img alt="SQL Server" src="https://img.shields.io/badge/SQL%20Server-2022-blue?style=for-the-badge" /></a>
  <a href="https://docs.microsoft.com/ef/"><img alt="Entity Framework" src="https://img.shields.io/badge/Entity%20Framework-6.2-yellow?style=for-the-badge" /></a>
</p>

## Features Summary 🪄

### 🔒 Secure Authentication
Secure user authentication with **BCrypt** password hashing. Usernames and passwords are stored in a local SQL Server database, ensuring data privacy.

![Authentication](https://i.imgur.com/oo7GsP1.png)

### ✅ Dashboard Overview
Get a quick overview of you kanban boards and manage them in a single place. Create, edit, and delete boards with ease.

![Dashboard](https://i.imgur.com/uPfJ1qY.png)

### 📊 Offline-First Kanban Board

Drag & drop your tasks across customizable columns without any internet connection.

![Kanban Board](https://i.imgur.com/xwvMtEC.png)

### 🗂️ Task Management
Create, edit, and delete tasks with ease. Add due dates, descriptions, and labels to keep your tasks organized.

| ![Task Detail View](https://i.imgur.com/4JaTxDy.png) | ![Task List View](https://i.imgur.com/yI2VUC5.png) |
| :------------------------------------------------: | :--------------------------------------------------: |



### ✍️ Kanban Element Management
Create, edit, and delete Kanban elements (columns) with ease. Customize the name and color of each column to suit your workflow.

![Label Management](https://i.imgur.com/USYKl5r.png)


## Installation Process 📦

1. **Clone** the repository.
2. **Create** the database in SQL Server Management Studio (ensure SQL Server is installed).
3. **Locate** the SQL script in the `Recursos` folder: `organitask.sql`.
4. **Connect** to your local server in SSMS (note your server name).
5. **Execute**:

   * First, run the `CREATE DATABASE [...]` section.
   * Then, execute the remainder to create tables & constraints.
6. **Open** Visual Studio and **Open a Project or Solution**, then select `OrganiTask.sln`.
7. In **Solution Explorer**, copy `App.config.example` → rename to `App.config`.
8. In `<connectionStrings>`, replace `SERVER_NAME` in the provider string with your server name.
9. **Build & Run** — NuGet packages will restore automatically.

## Usage License ⚖️

<a rel="license" href="http://creativecommons.org/licenses/by-nc/4.0/"><img alt="Licencia Creative Commons" style="border-width:0" src="https://i.creativecommons.org/l/by-nc/4.0/88x31.png" /></a><br /> <span xmlns:dct="http://purl.org/dc/terms/" property="dct:title" rel="dct:type">
OrganiTask is distributed under an <a rel="license" href="http://creativecommons.org/licenses/by-nc/4.0/">Attribution-NonCommercial 4.0 International License</a>. </span>
