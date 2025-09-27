# LibraryProject DevOps Guide (Git flow lite → CI → CD → Releases)

This repository uses a lightweight Git Flow, automated CI with GitHub Actions, container images built to GHCR, and deployment via Fly.io. This document captures the conventions and provides copy/paste snippets to operate the workflow.

Note: We work on branch `develop` by default. Production is `main`.

## 1) Branch model

- `main` → production, auto-deploy on tagged releases.
- `develop` → integration/staging, optional auto-deploy to staging.
- `feature/*` → short-lived; PR → `develop`.
- `release/*` → freeze for testing; then merge to `main` and back-merge to `develop`.
- `hotfix/*` → branch off `main` for urgent fix; merge to `main` and back-merge to `develop`.

One-time setup

```bash
# Protect main & develop in GitHub settings (require PR + status checks)
git checkout -b develop
git push -u origin develop
```

## 2) Versioning & tagging (SemVer)

- Tags: `vX.Y.Z`.
- Rule of thumb:
  - breaking: `+1.0.0`
  - new feature: `+0.1.0`
  - fix/docs/chore: `+0.0.1`

Release flow

```bash
git checkout develop
git pull
git checkout -b release/1.0.0
# (bump docs/README or top-level README if needed)
git commit -m "chore(release): prepare 1.0.0 (step 12)"
git push -u origin release/1.0.0

# After review:
git checkout main
git merge --no-ff release/1.0.0 -m "release: 1.0.0"
git tag -a v1.0.0 -m "Release 1.0.0"
git push origin main --tags

# Back-merge to develop
git checkout develop
git merge --no-ff main -m "chore: sync from main after 1.0.0"
git push
```

## 3) CI with GitHub Actions

Workflows are in `.github/workflows/`.

- `ci.yml` builds .NET solution, runs tests, and builds the client (TypeScript typechecking happens during prod build). It also starts a Postgres service for tests if needed.
- Triggers: PRs to `develop`/`main` and pushes to `develop` or `feature/**`.

## 4) Containers to GHCR

- API Dockerfile: `server/LibraryProject.Api/Dockerfile` (multi-stage .NET 8 build/publish).
- `docker.yml` builds and pushes images to GitHub Container Registry (GHCR) on pushes to `develop`/`main` and on tags `v*.*.*`.
- Tagging schema used in the workflow:
  - `library-api:sha-<git_sha>` on every push
  - `library-api:latest` when on `main` (edge when on other branches)
  - Additionally, for tags: `library-api:vX.Y.Z`

## 5) Deploy via Fly.io

- Configuration: `server/LibraryProject.Api/fly.toml`
- We pull the image from GHCR.
- Set the DB connection string as a Fly secret:

```bash
fly secrets set CONN_STR="Host=...;Username=...;Password=...;Database=neondb;SSL Mode=Require;Trust Server Certificate=true"
```

- Deployment workflow: `.github/workflows/deploy.yml` triggers on `main` and tags.

Optional: Add a second Fly app for staging and a `deploy-staging.yml` workflow that triggers on `develop`.

## 6) Releases

- `.github/workflows/release.yml` creates GitHub Releases automatically when tags `v*.*.*` are pushed. It uses auto-generated release notes.

## 7) Client base URL configuration

- `client/library-client/.env.production` should contain: `VITE_API_URL=https://<your-prod-app>.fly.dev`
- For staging, create `.env.staging`: `VITE_API_URL=https://<your-staging-app>.fly.dev`
- Use in code:

```ts
import { ApiClient } from './apiClient';
export const apiBaseUrl = import.meta.env.VITE_API_URL;
export const client = new ApiClient(apiBaseUrl);
```

## 8) Commit style

Use Conventional Commits + optional step markers:

- `feat(api): add Author/Book endpoints (step 10)`
- `chore(ci): add GH Actions CI workflow (step 12)`
- `chore(docker): Dockerfile for API (step 12)`
- `chore(cd): Fly deploy via GHCR (step 12)`
- `release: 1.0.0`

## 9) Local development

- API requires env var `CONN_STR` (Neon/Postgres). In PowerShell you can source a `.env` with:

```powershell
Get-Content .env | ForEach-Object {
    if ($_ -match '^([^#][^=]+)=(.*)$') {
        $name = $matches[1].Trim()
        $value = $matches[2].Trim()
        [Environment]::SetEnvironmentVariable($name, $value, "Process")
    }
}
```

- Run API locally:

```powershell
cd server\LibraryProject.Api
$env:CONN_STR="Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=testdb"
dotnet run
```

- Client dev:

```powershell
cd client\library-client
npm install
npm run dev
```

## 10) Validation of REST behavior

- Controllers return:
  - GET /api/* → 200
  - POST /api/* → 201 Created + Location → GetById
  - GET /api/*/{id} → 200 or 404
  - PUT /api/*/{id} → 200
  - DELETE /api/*/{id} → 204

- The NSwag TypeScript client is generated to accept 201 for POST endpoints.

## FAQ

- Build locks during development?
  - Stop the running API process before rebuilding to release file locks.
- Seeing `ApiException` on POST with 201?
  - Ensure the generated TS client accepts 201 (regenerate after adding ProducesResponseType annotations) or use the updated client already in `client/library-client/src/apiClient.ts`.
