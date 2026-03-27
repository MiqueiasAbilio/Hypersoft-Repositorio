📄 README.md
# Hypersoft Challenge

Sistema fullstack com autenticação via Keycloak, API em .NET e banco MongoDB.

## 🚀 Tecnologias

- Frontend: Next.js
- Backend: .NET
- Banco de dados: MongoDB
- Autenticação: Keycloak
- Infraestrutura: Docker Compose

---

## 📦 Como rodar o projeto

### 1. Clonar o repositório

```bash
git clone <repo-url>
cd hypesoft-challenge
2. Subir os containers
docker compose up --build
🌐 Serviços disponíveis
Serviço	URL
Frontend	http://localhost:3000

API (.NET)	http://localhost:5000

Swagger	http://localhost:5000/swagger

Mongo Express	http://localhost:8081

Keycloak	http://localhost:8080
🔐 Configuração do Keycloak
1. Acessar o painel
http://localhost:8080

Login:

admin / admin
2. Criar Realm
hypesoft-realm
3. Criar Client
Client ID: hypesoft-client
Type: OpenID Connect

Configurações importantes:

Client authentication: OFF
Standard flow: ON
Redirect URIs
http://localhost:3000/*
Web Origins
http://localhost:3000
4. Criar usuário

Exemplo:

username: teste
password: 123456
📊 Banco de dados

MongoDB disponível em:

http://localhost:8081

Banco esperado:

hypesoft_db
⚠️ Problema conhecido (em andamento)

Atualmente, a autenticação no frontend está funcionando, porém:

As requisições para a API não incluem o token Bearer
A API retorna 401 Unauthorized nos endpoints protegidos
Causa

O frontend não está enviando o Authorization header com o access token do Keycloak.

Próximo passo
Ajustar o frontend para incluir o token nas requisições
Validar persistência no MongoDB
🧠 Observações técnicas
Keycloak está configurado com volume persistente
Docker Compose ajustado para evitar variáveis não definidas
Ambiente totalmente containerizado
📌 Status do projeto
Parte	Status
Docker	✅ OK
MongoDB	✅ OK
Keycloak	✅ OK
Frontend login	✅ OK
API	✅ OK
Persistência	⚠️ Em ajuste