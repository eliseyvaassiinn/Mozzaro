Mozzaro

Mozzaro --- вебсистема онлайн-замовлення піци, розроблена командою
CrustCode.

Про проєкт

Mozzaro дає користувачеві змогу переглядати каталог піц, відкривати
інформацію про товари, додавати піцу до кошика, оформлювати замовлення
та переглядати історію власних замовлень.

Актуальна версія проєкту відповідає SRS 2.0 та фактично реалізованій
версії проєкту на етапі Sprint 6.

Основні можливості

каталог піц;

детальна інформація про піци;

реєстрація та авторизація;

кошик;

зміна кількості та видалення товарів;

оформлення замовлення;

адреса доставки;

збереження замовлень;

історія власних замовлень;

відображення статусу замовлення.

Основний користувацький сценарій:

каталог → вибір піци → кошик → авторизація → адреса доставки → оформлення замовлення → історія замовлень

Архітектура

Проєкт побудований на принципах Clean / Onion Architecture.

Загальна схема:

Mozzaro.Client
      │
      ▼
Mozzaro.API
      │
      ▼
Mozzaro.Application
      │
      ▼
Mozzaro.Infrastructure
      │
      ▼
PostgreSQL

Mozzaro.Domain містить доменні сутності та не залежить від
інфраструктури.

Основні принципи:

Dependency Injection;

async/await;

REST API;

Clean / Onion Architecture;

Repository Pattern;

Unit of Work;

LINQ;

Entity Framework Core;

розділення відповідальності між шарами.

Структура solution

Mozzaro
├── Mozzaro.Domain
├── Mozzaro.Application
├── Mozzaro.Infrastructure
├── Mozzaro.API
├── Mozzaro.Client
└── Mozzaro.Tests

Mozzaro.Domain {#mozzarodomain}

Доменні сутності та модель предметної області:

User;

Pizza;

Category;

Ingredient;

Cart;

CartItem;

Order;

OrderItem.

Mozzaro.Application {#mozzaroapplication}

Інтерфейси та бізнес-логіка застосунку.

Mozzaro.Infrastructure {#mozzaroinfrastructure}

Доступ до даних та інфраструктурні компоненти:

Entity Framework Core;

PostgreSQL;

DbContext;

Repository;

Unit of Work.

Mozzaro.API {#mozzaroapi}

ASP.NET Core Web API:

REST controllers;

dependency injection;

HTTP-запити;

Swagger / OpenAPI.

Mozzaro.Client {#mozzaroclient}

Blazor-клієнт:

головна сторінка;

меню;

деталі піци;

кошик;

checkout;

вхід;

реєстрація;

історія замовлень.

Mozzaro.Tests {#mozzarotests}

Проєкт автоматизованого тестування.

Технологічний стек

Компонент          Технологія

Мова               C#
Framework          .NET 9
Backend            ASP.NET Core Web API
Frontend           Blazor / Razor Components
Render mode        Interactive Server
Database           PostgreSQL
ORM                Entity Framework Core
Data Access        Repository Pattern, Unit of Work, LINQ
API                REST, Swagger / OpenAPI
Testing            xUnit, Moq, integration tests
Coverage           dotCover
Containerization   Docker
Deployment         Render
Database hosting   Neon
Version control    Git / GitHub

Основні сценарії

Перегляд меню

Відвідувач відкриває меню та отримує список доступних піц через REST
API.

Реєстрація

Користувач створює обліковий запис із ім'ям, email і паролем. Email має
бути унікальним, пароль не зберігається у відкритому вигляді.

Авторизація

Зареєстрований користувач входить за допомогою email і пароля.

Кошик

Користувач може додавати піцу, змінювати кількість, видаляти позиції та
очищати кошик.

Оформлення замовлення

Для оформлення замовлення користувач має бути авторизований і мати
непорожній кошик.

Користувач вказує адресу доставки. Після успішного створення сервер
зберігає Order та OrderItem, після чого кошик очищується.

Історія замовлень

Користувач переглядає власні оформлені замовлення та їхній статус.

База даних

Використовується PostgreSQL.

Основні таблиці:

Users
Pizzas
Categories
Ingredients
PizzaIngredients
Carts
CartItems
Orders
OrderItems

Доступ до даних реалізовано через Entity Framework Core, Repository
Pattern та Unit of Work.

API

Backend надає REST API для основних операцій системи.

Приклад endpoint каталогу:

GET /api/pizza

API використовується клієнтом для отримання даних про піци, реєстрації
та авторизації користувачів, а також роботи із замовленнями.

API documentation

TODO: додати публічне посилання на Swagger / OpenAPI / Scalar

Запуск проєкту

Вимоги

.NET 9 SDK;

PostgreSQL;

Git.

Клонування

git clone https://github.com/eliseyvaassiinn/Mozzaro.git
cd Mozzaro

Після налаштування рядка підключення до PostgreSQL проєкт можна
запускати з Rider або через .NET CLI:

dotnet run

Production

Frontend

https://mozzaro-1.onrender.com

Backend API

https://mozzaro.onrender.com

Pizza endpoint

https://mozzaro.onrender.com/api/pizza

На безкоштовному тарифі Render сервіс може тимчасово зупинятися після
періоду бездіяльності. Перший запит після зупинки може зайняти більше
часу.

Документація

У Sprint 7 готуються:

технічна документація проєкту;

документація вихідного коду;

API documentation;

публікація документації.

Документація вихідного коду

TODO: додати посилання на опубліковану документацію DocFX

API documentation

TODO: додати посилання на опубліковану Swagger / OpenAPI / Scalar документацію

Межі MVP

Входить до MVP

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

Не входить до поточного MVP

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

SRS

Основний документ вимог проєкту --- Software Requirements
Specification, SRS 2.0.

SRS описує актуальну архітектуру, функціональні та нефункціональні
вимоги, Use Cases, межі MVP, ризики та критерії успіху проєкту.

Команда

CrustCode

Проєкт: Mozzaro