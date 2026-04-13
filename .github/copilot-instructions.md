# GitHub Copilot Custom Instructions

## 1. Core Philosophy & Rules
* **Model Preference:** Always use the Claude Sonnet model (e.g., Claude 4.6 Sonnet or latest available) as the default engine for generating code and reasoning.
* **Spec-Driven Design:** You MUST read and adhere strictly to the rules, data dictionaries, and dynamic chart architecture defined in the `architecture.md` file. Do not invent new database tables, fields, or workflows unless explicitly requested by the user.
* **"One-Man Army" (Indie Maker) Vibe:** Prioritize simplicity, delivery speed, and maintainability. Avoid over-engineering, complex design patterns (like clean architecture layers, repository patterns, or heavy DTO mappings), and unnecessary abstractions. 
* **KISS (Keep It Simple, Stupid):** If a simple function or a single file solves the problem, use it. Do not spread logic across multiple files unnecessarily.

## 2. Backend Coding Standards (.NET 10)
* **Framework:** Use .NET 10 Minimal APIs. Do NOT use traditional MVC Controllers.
* **Routing:** Define endpoints directly in `Program.cs` or use simple static extension methods for grouping.

## 3. Frontend Coding Standards (Vue 3)
* **Paradigm:** Use Vue 3 with the Composition API exclusively. 
* **Syntax:** Always use `<script setup lang="ts">`. Do not use the Options API.
* **vue functions:** Always use `const myFunction = () => {}` instead of `function myFunction() {}` for defining functions.
* **Page Components:** Page components should have the sufix `Page.vue` (e.g., `LoginPage.vue`, `ProductsPage.vue`) and be placed in the `pages/` directory. Reusable components should be placed in the `components/` directory and should not have the `Page` suffix.
* **Styling:** Use Tailwind CSS for all styling. Avoid custom CSS/SCSS files unless strictly necessary. Use utility classes directly in the template.
* **State Management:** Use Pinia for global state (e.g., storing the JWT token, selected `PersonalTrainerId`, and current selected student). Keep component-specific state local using `ref()` or `reactive()`.
* **Data Fetching:** Prefer native `fetch` API or a lightweight wrapper for interacting with the .NET backend. 
* **Dynamic Rendering:** Build components that can recursively or dynamically render input fields (`<input type="number">` vs `<select>`) based on the `DataType` provided by the backend chart definitions.
MicroShopPOC/src/frontend/
│
├── src/
│   ├── assets/              # Imagens, ícones, estilos globais
│   ├── components/          # Componentes reutilizáveis (botões, tabelas, modais)
│   ├── composables/         # Funções reativas (useAuth, useProducts, etc.)
│   ├── layouts/             # Layouts principais (DefaultLayout, AuthLayout)
│   ├── models/              # Entidades em TypeScript (User.ts, Product.ts, Sale.ts)
│   ├── pages/               # Páginas da aplicação (LoginPage.vue, HomePage.vue, ProductsPage.vue, SalesPage.vue, UsersPage.vue)
│   ├── router/              # Configuração do Vue Router (routes.ts)
│   ├── stores/              # Pinia stores (authStore.ts, productStore.ts, salesStore.ts)
│   ├── services/            # Comunicação com API (apiGateway.ts, authService.ts, productService.ts, salesService.ts)
│   ├── utils/               # Funções utilitárias (formatters, helpers)
│   ├── App.vue              # Componente raiz
│   └── main.ts              # Ponto de entrada da aplicação
│
├── public/                  # Arquivos públicos (index.html, favicon)
├── package.json
├── tsconfig.json
└── vite.config.ts           # Configuração do Vite

## 4. Response Guidelines
* Before generating code, briefly state which section of `architecture.md` you are addressing.
* Provide complete, runnable code blocks. Do not leave "TODO" comments for critical business logic.
* If the user asks for a feature that contradicts `architecture.md`, politely warn them about the architectural deviation before proceeding.