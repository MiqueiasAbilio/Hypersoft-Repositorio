README.md — Hypersoft Challenge
# 🚀 Hypersoft Challenge

Projeto fullstack desenvolvido como desafio técnico, com foco em arquitetura moderna, autenticação segura e containerização.

---

## 📌 Visão Geral

Este projeto consiste em uma aplicação composta por:

- 🖥️ Frontend em Next.js
- ⚙️ Backend em .NET
- 🔐 Autenticação com Keycloak
- 🐳 Ambiente containerizado com Docker

O objetivo é simular um ambiente real de aplicação, com separação de responsabilidades, segurança e escalabilidade.

---

## 🏗️ Arquitetura


📦 hypesoft-challenge
┣ 📂 frontend → Aplicação Next.js
┣ 📂 backend → API .NET
┣ 📂 docker → Configurações de containers
┗ 📄 docker-compose.yml


---

## 🛠️ Tecnologias Utilizadas

### Backend
- .NET (ASP.NET Core)
- MediatR
- MongoDB / InMemory
- JWT Authentication

### Frontend
- Next.js
- React
- Tailwind (se estiver usando)

### Infraestrutura
- Docker
- Docker Compose
- Keycloak (Autenticação)

---

## 🔐 Autenticação

A autenticação é gerenciada via **Keycloak**, permitindo:

- Login seguro via OAuth2 / OpenID Connect
- Gerenciamento de usuários e roles
- Integração com o frontend e backend

---

## ⚙️ Como Rodar o Projeto

### Pré-requisitos

- Docker instalado
- Docker Compose
- Node.js (opcional para rodar frontend isolado)
- .NET SDK (opcional para backend local)

---

### 🐳 Rodando com Docker (Recomendado)

```bash
docker-compose up --build

A aplicação estará disponível em:

Frontend: http://localhost:3000
Backend: http://localhost:5000
Keycloak: http://localhost:8080
🔧 Variáveis de Ambiente

Crie um arquivo .env na raiz do projeto com base no exemplo:

NEXT_PUBLIC_API_URL=http://localhost:5000
NEXTAUTH_URL=http://localhost:3000
KEYCLOAK_CLIENT_ID=hypesoft-client
KEYCLOAK_CLIENT_SECRET=your-secret