# CargoDeliveryApp

## О проекте:

Это приложение предназначено для упрощения процесса управления заявками на доставку грузов. Оно позволяет клиентам
регистрировать запросы на забор и доставку груза, а также обеспечивает курьеров компании эффективным выполнением этих
запросов.

## Предварительные требования:

### С использованием Docker:

- Убедитесь, что у вас установлен Docker.

### Без использования Docker:

- Установите следующие инструменты:
    - .NET 7
    - PostgreSQL

## Демонстрация

Главная страница
![Main Page](Screenshots/Screenshot%202023-11-20%20at%2009.29.43.png)

Страница редактирования:
![Edit Page](Screenshots/Screenshot%202023-11-20%20at%2009.29.59.png)

[ссылка на демо](http://158.160.79.57:5279/)

## Установка:

Клонируйте репозиторий:

```bash
git clone https://github.com/GSRTIRSH/CargoDeliveryApp.git
````

Перейдите в папку проекта:

```bash
cd CargoDeliveryApp
```

## Запуск приложения:

*note: выполняйте команнды в корневой папке проекта*

- ### используя Docker:

```bash
docker compose up
```

- ### без Docker:

```bash
dotnet run \
  --project CargoApp.Presentation/CargoApp.Presentation.csproj \
  --environment "Development" \
  --Connection_Strings="Host={your_db_host}; Port=5432; Database={your_db}; Username={your_db_user}; Password={your_db_password}"
```

## Лицензия

Этот проект лицензирован по [Лицензии MIT](https://opensource.org/license/mit/).