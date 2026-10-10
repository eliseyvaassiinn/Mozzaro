🍕 Mozzaro

Mozzaro — вебсистема онлайн-замовлення піци, розроблена командою CrustCode.

Актуальна версія проєкту відповідає SRS 2.0 та фактично реалізованій версії на етапі Sprint 6.

📋 Зміст

Про проєкт

Основні можливості

Архітектура

Структура solution

Технологічний стек

Основні сценарії

База даних

API

Запуск проєкту

Production

Документація

Межі MVP

SRS

Команда

🎯 Про проєкт

Mozzaro — онлайн-піцерія з клієнтським вебінтерфейсом та серверним REST API.

Користувач може:

переглядати каталог піц;

відкривати детальну інформацію про товар;

додавати піцу до кошика;

змінювати кількість товарів;

оформлювати замовлення;

вказувати адресу доставки;

переглядати історію власних замовлень.

Основний користувацький сценарій

Каталог
↓
Вибір піци
↓
Кошик
↓
Авторизація
↓
Адреса доставки
↓
Оформлення замовлення
↓
Історія замовлень

🚀 Основні можливості

🍕 Каталог

перегляд каталогу піц;

перегляд назви, опису та ціни;

перегляд зображення піци;

перегляд детальної інформації.

👤 Користувач

реєстрація;

авторизація;

вихід із системи;

робота з поточною користувацькою сесією.

🛒 Кошик

додавання піци;

збільшення кількості;

зменшення кількості;

видалення позиції;

очищення кошика;

розрахунок загальної вартості.

📦 Замовлення

оформлення замовлення;

введення адреси доставки;

створення Order та OrderItem;

очищення кошика після успішного оформлення;

перегляд історії власних замовлень;

перегляд статусу замовлення.

🏗️ Архітектура

Проєкт побудований на принципах Clean / Onion Architecture.

Загальна схема

┌─────────────────────┐
│   Mozzaro.Client    │
│      Blazor UI      │
└──────────┬──────────┘
│ HTTP
▼
┌─────────────────────┐
│     Mozzaro.API     │
│    ASP.NET Core     │
└──────────┬──────────┘
▼
┌─────────────────────┐
│ Mozzaro.Application │
│   Business Logic    │
└──────────┬──────────┘
▼
┌─────────────────────┐
│ Mozzaro.Infrastructure│
│ EF Core / Repository │
└──────────┬──────────┘
▼
┌─────────────────────┐
│     PostgreSQL      │
└─────────────────────┘

Mozzaro.Domain містить доменні сутності та не залежить від інфраструктурного шару.

Основні принципи

Dependency Injection;

async/await;

REST API;

Clean / Onion Architecture;

Repository Pattern;

Unit of Work;

LINQ;

Entity Framework Core;

розділення відповідальності між шарами.

📁 Структура solution

Mozzaro
├── Mozzaro.Domain
├── Mozzaro.Application
├── Mozzaro.Infrastructure
├── Mozzaro.API
├── Mozzaro.Client
└── Mozzaro.Tests

Mozzaro.Domain

Доменні сутності та модель предметної області:

User

Pizza

Category

Ingredient

Cart

CartItem

Order

OrderItem

Mozzaro.Application

Містить інтерфейси та бізнес-логіку застосунку.

Mozzaro.Infrastructure

Відповідає за доступ до даних та інфраструктурні компоненти:

Entity Framework Core;

PostgreSQL;

DbContext;

Repository;

Unit of Work.

Mozzaro.API

ASP.NET Core Web API:

REST controllers;

Dependency Injection;

HTTP-запити;

Swagger / OpenAPI.

Mozzaro.Client

Blazor-клієнт застосунку:

головна сторінка;

меню;

деталі піци;

кошик;

checkout;

вхід;

реєстрація;

історія замовлень.

Mozzaro.Tests

Проєкт автоматизованого тестування.

🛠️ Технологічний стек

Компонент

Технологія

Мова

C#

Framework

.NET 9

Backend

ASP.NET Core Web API

Frontend

Blazor / Razor Components

Render Mode

Interactive Server

Database

PostgreSQL

ORM

Entity Framework Core

Data Access

Repository Pattern, Unit of Work, LINQ

API

REST, Swagger / OpenAPI

Testing

xUnit, Moq, Integration Tests

Coverage

dotCover

Containerization

Docker

Deployment

Render

Database Hosting

Neon

Version Control

Git / GitHub

🔄 Основні сценарії

Перегляд меню

Відвідувач відкриває меню та отримує список доступних піц через REST API.

Реєстрація

Користувач створює обліковий запис із ім'ям, email і паролем.

Email має бути унікальним, а пароль не зберігається у відкритому вигляді.

Авторизація

Зареєстрований користувач входить за допомогою email і пароля.

Робота з кошиком

Користувач може:

додавати піцу;

змінювати кількість;

видаляти позиції;

очищати кошик.

Оформлення замовлення

Для оформлення замовлення користувач має бути авторизований і мати непорожній кошик.

Після введення адреси доставки сервер:

створює Order;

створює OrderItem;

зберігає замовлення в PostgreSQL;

очищає кошик.

Історія замовлень

Користувач може переглядати власні оформлені замовлення та їхній статус.

🗄️ База даних

Для зберігання даних використовується PostgreSQL.

Основні таблиці

Users
Pizzas
Categories
Ingredients
PizzaIngredients
Carts
CartItems
Orders
OrderItems

Доступ до даних реалізовано через:

Entity Framework Core;

Repository Pattern;

Unit of Work;

LINQ.

🔌 API

Backend надає REST API для основних операцій системи.

Приклад endpoint каталогу

GET /api/pizza

Endpoint повертає список доступних піц.

API також використовується клієнтом для:

реєстрації;

авторизації;

роботи із замовленнями;

інших операцій системи.

API Documentation

🔧 Буде додано після публікації Swagger / OpenAPI / Scalar documentation.

💻 Запуск проєкту

Вимоги

Для локального запуску необхідні:

.NET 9 SDK;

PostgreSQL;

Git;

Rider або інше середовище для .NET.

Клонування репозиторію

git clone https://github.com/eliseyvaassiinn/Mozzaro.git
cd Mozzaro

Після налаштування рядка підключення до PostgreSQL проєкт можна запускати через Rider або .NET CLI.

dotnet run

🌐 Production

Frontend

Mozzaro Client

https://mozzaro-1.onrender.com

Backend API

Mozzaro API

https://mozzaro.onrender.com

Pizza API endpoint

https://mozzaro.onrender.com/api/pizza

На безкоштовному тарифі Render сервіс може тимчасово зупинятися після періоду бездіяльності. Перший запит після зупинки може зайняти більше часу.

📚 Документація

У межах Sprint 7 готуються:

технічна документація проєкту;

документація вихідного коду;

API documentation;

публікація документації.

Документація вихідного коду

🔧 Буде додано після генерації та публікації DocFX documentation.

API documentation

🔧 Буде додано після публікації Swagger / OpenAPI / Scalar documentation.

📦 Межі MVP

✅ Входить до MVP

реєстрація;

авторизація;

каталог піц;

категорії та інгредієнти;

кошик;

оформлення замовлення;

збереження замовлень;

Order та OrderItem;

історія замовлень;

статус замовлення;

REST API;

Swagger / OpenAPI;

unit та integration testing.

❌ Не входить до поточного MVP

JWT access tokens;

повноцінне розмежування Customer / Admin;

адміністративна панель;

SignalR-сповіщення;

онлайн-платежі;

GPS та власна система доставки;

нативні мобільні застосунки;

повноцінна система лояльності;

повноцінні промокоди;

AI-рекомендації.

📄 SRS

Основний документ вимог проєкту:

Software Requirements Specification — Mozzaro, SRS 2.0

SRS описує:

актуальну архітектуру;

функціональні вимоги;

нефункціональні вимоги;

Use Cases;

межі MVP;

ризики;

критерії успіху проєкту.

👥 Команда

CrustCode

Проєкт: Mozzaro

🔗 Посилання

Ресурс

Посилання

🌐 Frontend

https://mozzaro-1.onrender.com

⚙️ Backend API

https://mozzaro.onrender.com

🐙 GitHub

https://github.com/eliseyvaassiinn/Mozzaro

📚 Документація коду

TODO

🔌 API documentation

TODO