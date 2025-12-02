# Technight 2025-12 Monorepo

A minimal monorepo setup with frontend (React + Vite), backend (Express + TypeScript + Prisma), and automatic SDK generation.

## Project Structure

```
technight-2025-12/
├── backend/node/        # Express + TypeScript backend with Swagger
│   ├── prisma/          # Prisma schema and migrations
│   ├── scripts/         # SDK generation script
│   └── server.ts        # Main entry point
├── frontend/            # React + Vite + TypeScript
│   └── src/
│       ├── App.tsx      # Main app component
│       └── main.tsx     # Entry point
├── sdk/ts/              # Auto-generated TypeScript SDK
└── package.json         # Root monorepo configuration
```

## Technologies

### Backend
- **Framework**: Express.js
- **Language**: TypeScript 5.6
- **Database ORM**: Prisma
- **API Documentation**: Swagger/OpenAPI
- **Port**: 6173

### Frontend
- **Framework**: React 19
- **Build Tool**: Vite 6
- **Language**: TypeScript 5.6
- **Port**: 5173

### SDK
- **Generator**: OpenAPI Generator (typescript-fetch)
- **Package**: @technight/api
- **Auto-generated**: From backend OpenAPI spec

## Requirements

- **Node.js**: >= 20.19.4
- **npm**: >= 10.0.0

Use [nvm](https://github.com/nvm-sh/nvm) to manage Node versions:

```bash
nvm use
```

The project includes a `.nvmrc` file that automatically sets the correct Node version.

## GitHub Codespaces

This project is fully configured for GitHub Codespaces. Simply:

1. Click "Code" → "Create codespace on master"
2. Wait for the environment to build (dependencies install automatically)
3. Run `npm run dev` to start both frontend and backend

The devcontainer includes:
- Node.js 20
- VS Code extensions (ESLint, Prettier, Prisma)
- Auto-forwarding for ports 5173 (frontend) and 6173 (backend)

## Getting Started

### Install Dependencies

```bash
# Install all dependencies (root, backend, frontend)
npm install
cd backend/node && npm install
cd ../../frontend && npm install
```

### Development

```bash
# Run both frontend and backend concurrently
npm run dev

# Or run individually:
npm run dev:backend    # Backend on http://localhost:6173
npm run dev:frontend   # Frontend on http://localhost:5173
```

### Backend Endpoints

- **Health Check**: http://localhost:6173/api/health
- **Swagger UI**: http://localhost:6173/api/swagger
- **OpenAPI JSON**: http://localhost:6173/api/openapi.json
- **OpenAPI YAML**: http://localhost:6173/api/openapi.yaml

### Generate SDK

The SDK is automatically generated from the backend's OpenAPI specification:

```bash
# Generate TypeScript SDK
npm run sdk:generate
```

The script will:
1. Start the backend server on port 7173
2. Wait for the health check endpoint
3. Generate the SDK to `sdk/ts/`
4. Stop the server

### Build for Production

```bash
# Build both frontend and backend
npm run build

# Or build individually:
npm run build:backend
npm run build:frontend
```

## Adding API Endpoints

1. Add your endpoint in `backend/node/server.ts` with JSDoc comments:

```typescript
/**
 * @swagger
 * /api/users:
 *   get:
 *     summary: Get all users
 *     responses:
 *       200:
 *         description: List of users
 */
app.get('/api/users', (req, res) => {
  res.json({ users: [] });
});
```

2. Regenerate the SDK:

```bash
npm run sdk:generate
```

3. Use the SDK in your frontend:

```typescript
import { Configuration, HealthApi } from '@technight/api';

const config = new Configuration({
  basePath: 'http://localhost:6173'
});

const healthApi = new HealthApi(config);
const response = await healthApi.apiHealthGet();
```

## Database Setup

The project includes Prisma ORM. To set up your database:

1. Update `backend/node/.env` with your database URL:
```
DATABASE_URL="postgresql://user:password@localhost:5432/mydb?schema=public"
```

2. Define your schema in `backend/node/prisma/schema.prisma`

3. Run migrations:
```bash
cd backend/node
npm run prisma:migrate
```

## Notes

- No deployment configurations included (local development only)
- No authentication setup (minimal setup)
- No extra UI libraries (just React + basic styling)
- Database schema is empty (ready for your models)
