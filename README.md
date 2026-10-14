# Mozzaro 🍕

> Интернет-магазин пиццы: меню с категориями, корзина, оформление заказа с адресом доставки, регистрация и история заказов. Командный проект CrustCode на ASP.NET Core 9 с Clean Architecture и клиентом на Blazor.

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet)
![Blazor](https://img.shields.io/badge/Blazor-Server-512BD4?style=for-the-badge&logo=blazor)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-EF%20Core-336791?style=for-the-badge&logo=postgresql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-ready-2496ED?style=for-the-badge&logo=docker&logoColor=white)

## 📖 О проекте

Покупатель открывает меню, фильтрует пиццы по категориям, смотрит состав и цену, кладёт понравившееся в корзину и оформляет заказ с адресом доставки. Зарегистрированный пользователь видит историю своих заказов. Меню заполняется автоматически при первом запуске: 5 категорий и 24 пиццы с фотографиями и составом.

Проект разделён на слои: доменная модель не зависит ни от чего, бизнес-логика работает через интерфейсы, а доступ к базе и пользовательский интерфейс подключаются снаружи.

## ✨ Возможности

- Меню с 24 пиццами в 5 категориях: Классические, Мясные, Сырные, Острые, Авторские
- Фильтрация по категориям и страница пиццы (`/pizza/{id}`) с описанием и ингредиентами
- Корзина: добавление, удаление, изменение количества, итоговая сумма
- Оформление заказа с обязательным адресом доставки, статус нового заказа `Pending`
- Регистрация и вход по email и паролю
- История заказов пользователя (`/orders`)
- CRUD пицц и категорий через REST API
- Swagger UI в режиме разработки
- Автоматическое применение миграций и стартовое наполнение базы

## 🏗️ Архитектура

```
Mozzaro.API ──► Mozzaro.Application ──► Mozzaro.Domain ◄── Mozzaro.Infrastructure
Mozzaro.Client ──HTTP──► Mozzaro.API
```

| Проект | Ответственность |
|---|---|
| `Mozzaro.Domain` | Сущности: `Pizza`, `Category`, `Ingredient`, `PizzaIngredient`, `User`, `Cart`, `CartItem`, `Order`, `OrderItem`. Без зависимостей |
| `Mozzaro.Application` | Интерфейсы репозиториев и сервисов, `OrderService`, `UserService` |
| `Mozzaro.Infrastructure` | `MozzaroDbContext` (EF Core + Npgsql), конфигурации сущностей, репозитории, `UnitOfWork`, `PizzaService`, `CategoryService`, миграции, `DbSeeder` |
| `Mozzaro.API` | REST-контроллеры, DI-композиция, Swagger |
| `Mozzaro.Client` | Blazor-интерфейс: страницы, `CartService`, `UserSessionService`, `PizzaApiService` |
| `Mozzaro.Tests` | Модульные и интеграционные тесты на xUnit |

Паттерны: Repository (обобщённый `IRepository<T>`), Unit of Work, Dependency Injection, Service Layer.

Более подробная схема целевой структуры: [Mozzaro_Arch.txt](Mozzaro_Arch.txt).

## 🛠️ Технологии

| Слой | Технологии |
|---|---|
| Бэкенд | .NET 9, ASP.NET Core Web API, EF Core 9, Npgsql |
| База данных | PostgreSQL |
| Фронтенд | Blazor (Interactive Server), Razor-компоненты, изолированный CSS, Bootstrap |
| Тесты | xUnit, Moq, coverlet |
| Документация | XML-комментарии, Swashbuckle (Swagger), DocFX |
| Контейнеризация | Docker (отдельные Dockerfile для API и клиента) |

## 🗃️ Модель данных

```
Category 1───* Pizza *───* Ingredient     (через PizzaIngredient)
User     1───1 Cart 1───* CartItem *───1 Pizza
User     1───* Order 1───* OrderItem *───1 Pizza
```

У заказа есть статус (`Status`, по умолчанию `Pending`), итоговая сумма (`TotalPrice`), дата создания и адрес доставки. В `OrderItem` цена фиксируется на момент заказа, поэтому изменение цены пиццы не меняет старые заказы.

## 📚 API

Базовый адрес при локальном запуске: `http://localhost:5088`. Swagger: `/swagger` (только в Development).

| Группа | Эндпоинты |
|---|---|
| Pizza | `GET /api/Pizza`, `GET /api/Pizza/{id}`, `POST /api/Pizza`, `PUT /api/Pizza/{id}`, `DELETE /api/Pizza/{id}` |
| Category | `GET /api/Category`, `GET /api/Category/{id}`, `POST /api/Category`, `PUT /api/Category/{id}`, `DELETE /api/Category/{id}` |
| Order | `POST /api/Order`, `GET /api/Order/user/{userId}` |
| User | `POST /api/User/register`, `POST /api/User/login` |

Создание заказа (`POST /api/Order`) возвращает `400`, если пользователь некорректен, адрес доставки пуст или корзина не содержит позиций.

## 🚀 Быстрый старт

Нужны [.NET 9 SDK](https://dotnet.microsoft.com/download) и PostgreSQL.

```bash
git clone https://github.com/yourusername/Mozzaro.git
cd Mozzaro
```

1. Создайте базу `Mozzaro` в локальном PostgreSQL.
2. Укажите свои параметры подключения. Лучше не править `appsettings.json`, а задать переменную окружения:

   ```bash
   export ConnectionStrings__DefaultConnection="Host=localhost;Port=5432;Database=Mozzaro;Username=postgres;Password=<ваш_пароль>"
   ```

3. Запустите API (миграции и стартовые данные применятся сами):

   ```bash
   dotnet run --project Mozzaro.API --launch-profile http
   ```

4. В другом терминале запустите клиент:

   ```bash
   dotnet run --project Mozzaro.Client --launch-profile http
   ```

API слушает `http://localhost:5088`, клиент — `http://localhost:5203`. Клиент по умолчанию обращается к API на `http://localhost:5088/`. Другой адрес задаётся ключом `ApiBaseUrl` (например, переменной `ApiBaseUrl`).

## 🐳 Docker

Оба Dockerfile ожидают, что контекст сборки — корень репозитория.

```bash
# API
docker build -t mozzaro-api .
docker run -p 10000:10000 \
  -e ConnectionStrings__DefaultConnection="Host=host.docker.internal;Port=5432;Database=Mozzaro;Username=postgres;Password=<ваш_пароль>" \
  mozzaro-api

# Клиент
docker build -f Mozzaro.Client/Dockerfile -t mozzaro-client .
docker run -p 5000:10000 -e ApiBaseUrl="http://host.docker.internal:10000/" mozzaro-client
```

Оба образа двухэтапные: сборка на `dotnet/sdk:9.0`, запуск на `dotnet/aspnet:9.0`, внутри контейнера приложение слушает порт `10000`. Файла `docker-compose.yml` в репозитории пока нет.

## ⚙️ Конфигурация

| Ключ | Назначение |
|---|---|
| `ConnectionStrings:DefaultConnection` | Строка подключения к PostgreSQL (в окружении: `ConnectionStrings__DefaultConnection`) |
| `ApiBaseUrl` | Адрес API для клиента, по умолчанию `http://localhost:5088/` |
| `ASPNETCORE_URLS` | Адреса, которые слушает приложение (в Docker: `http://+:10000`) |
| `ASPNETCORE_ENVIRONMENT` | `Development` включает Swagger UI |

## 🧪 Тестирование

```bash
dotnet test
dotnet test --collect:"XPlat Code Coverage"
```

- `OrderServiceTests` и `UnitTest1`: модульные тесты бизнес-логики на xUnit и Moq
- `ApiIntegrationTests`: сценарии регистрации, входа и заказа через HTTP. Они обращаются к уже запущенному API на `http://localhost:5088`, поэтому перед их запуском стартуйте `Mozzaro.API`

## 📘 Документация кода

Конфигурация DocFX лежит в `docfx.json`, сгенерированные метаданные — в папке `api/`.

```bash
docfx docfx.json --serve
```

## ⚠️ Известные ограничения

Проект учебный, и часть вещей пока не доведена до продакшн-уровня:

- Пароли хешируются обычным SHA-256 без соли. Для реального использования нужен PBKDF2, bcrypt или Argon2 (например, `PasswordHasher` из ASP.NET Core Identity)
- Нет аутентификации в API: идентификатор пользователя передаётся в запросе, а CRUD пицц и категорий открыт для всех
- Корзина хранится в памяти клиента (`CartService`) и пропадает при перезагрузке страницы
- Сессия пользователя (`UserSessionService`) живёт в памяти Blazor-канала
- В `Mozzaro.API/appsettings.json` лежит пароль от локальной базы. Его стоит заменить на переменную окружения и сменить, если репозиторий публичный
- `DbSeeder` применяет миграции и наполняет базу при каждом старте в любом окружении

## 🗺️ Планы

- [ ] Аутентификация на JWT и роли (покупатель, администратор)
- [ ] Безопасное хранение паролей (PBKDF2 или Argon2)
- [ ] Корзина в базе данных (сущности `Cart` и `CartItem` уже есть)
- [ ] Статусы заказа и управление ими
- [ ] Админ-панель для меню
- [ ] `docker-compose.yml` для запуска API, клиента и базы одной командой
- [ ] CI: GitHub Actions со сборкой и тестами
- [ ] Интеграционные тесты через `WebApplicationFactory` без ручного запуска API

## 👨‍💻 Команда

Команда **CrustCode**.

## 📄 Лицензия

Учебный проект. Лицензия на повторное использование не указана.

---
*Разработано с ❤️ для любителей пиццы.*
