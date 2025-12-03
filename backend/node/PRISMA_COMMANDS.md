# Prisma Commands Guide

## Working with AWS RDS / Managed Databases

When using managed databases like AWS RDS, you typically don't have permission to create databases. This affects which Prisma commands you can use.

## Available Commands

### 1. `npm run prisma:generate`
Generates the Prisma Client based on your schema.

```bash
npm run prisma:generate
```

**When to use:** After changing your `schema.prisma` file.

---

### 2. `npm run prisma:migrate:deploy`
Applies pending migrations without creating a shadow database.

```bash
npm run prisma:migrate:deploy
```

**When to use:**
- ✅ In production environments
- ✅ With managed databases (AWS RDS, Azure, etc.)
- ✅ In CI/CD pipelines
- ✅ When you don't have permission to create databases

**What it does:**
- Applies all pending migrations
- Does NOT create a shadow database
- Does NOT validate the migration
- Fast and safe for production

---

### 3. `npm run prisma:push`
Pushes your schema changes directly to the database without migrations.

```bash
npm run prisma:push
```

**When to use:**
- ✅ During rapid prototyping
- ✅ When you want quick schema changes
- ✅ With managed databases (no shadow database needed)
- ❌ NOT for production (use migrations instead)

**What it does:**
- Syncs your Prisma schema with the database
- No migration files created
- Warns if data loss might occur

---

### 4. `npm run prisma:migrate` (Standard Dev Command)
Creates and applies migrations with validation.

```bash
npm run prisma:migrate
```

**When to use:**
- ✅ Local development with full database permissions
- ✅ When you need migration history
- ❌ NOT with managed databases (requires shadow database)

**What it does:**
- Creates migration files
- Validates migrations using a shadow database
- Applies migrations
- **Requires permission to create databases**

---

### 5. `npm run prisma:studio`
Opens Prisma Studio to view/edit your data.

```bash
npm run prisma:studio
```

**When to use:**
- ✅ To view database data in a GUI
- ✅ To manually edit records
- ✅ For debugging

---

## Recommended Workflow for This Project

Since we're using AWS RDS (shared database), here's the recommended workflow:

### Initial Setup (Already Done ✅)
```bash
# Mark existing migrations as applied
npx prisma migrate resolve --applied "20251203150629_add_example_entity"
npx prisma migrate resolve --applied "20251203150630_seed_example_data"

# Generate Prisma Client
npm run prisma:generate
```

### For New Schema Changes

**Option 1: Using Migrations (Recommended)**
```bash
# 1. Update prisma/schema.prisma
# 2. Create migration file manually in prisma/migrations/
# 3. Deploy the migration
npm run prisma:migrate:deploy

# 4. Regenerate client
npm run prisma:generate
```

**Option 2: Quick Prototyping**
```bash
# 1. Update prisma/schema.prisma
# 2. Push changes directly
npm run prisma:push
```

### Daily Development
```bash
# Just start the server - no migration needed if schema hasn't changed
npm run dev
```

---

## Troubleshooting

### Error: "Permission denied to create database"
**Cause:** Trying to use `prisma migrate dev` with AWS RDS.

**Solution:** Use `npm run prisma:migrate:deploy` or `npm run prisma:push` instead.

### Error: "Migration already applied"
**Cause:** Table already exists in the database.

**Solution:** Mark migration as applied:
```bash
npx prisma migrate resolve --applied "<migration-name>"
```

### Error: "Database schema is not in sync"
**Cause:** Schema differs from database.

**Solution:** 
```bash
# Check status
npx prisma migrate status

# Push schema to sync (dev only)
npm run prisma:push

# Or deploy migrations
npm run prisma:migrate:deploy
```

---

## Important Notes

- ✅ **Shared Database:** This database is shared with Spring Boot and .NET backends
- ✅ **Coordinate Changes:** Schema changes should be coordinated across all backends
- ✅ **No Shadow Database:** AWS RDS doesn't allow creating databases, so use `deploy` or `push`
- ✅ **Generate After Changes:** Always run `prisma:generate` after schema changes

---

## Quick Reference

| Task | Command |
|------|---------|
| Generate Client | `npm run prisma:generate` |
| Apply Migrations (RDS) | `npm run prisma:migrate:deploy` |
| Quick Schema Push | `npm run prisma:push` |
| View Data | `npm run prisma:studio` |
| Check Status | `npx prisma migrate status` |
| Mark as Applied | `npx prisma migrate resolve --applied "<name>"` |

