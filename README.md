# movie-catalog-api
WebAPI для приложения каталога фильмов

## Описание
Реализовано WebAPI приложение на фреймворке ASP.NET Core. Создана база данных с помощью библиотеки Entity Framework core с помощью принципа code first, провайдер базы данных MS SQL Server. Реализовано авторизация с помощью jwt-токена.

## Endpoints
- Movie (Controller)
  - Получение списка фильмов с пагинацией
  - Получение деталей фильма
- FavoriteMovies (Controller)
  - Получение списка избранных фильмов пользователя
  - Добавление фильма в список избранных пользователя
  - Удаление фильма из списка избранных пользователя
- Review (Controller)
  - Создание отзыва на фильм
  - Редактирование отзыва
  - Удаление отзыва
- Auth (Controller)
  - Регистрация пользователя
  - Авторизация пользователя
  - Выход из системы
- User (Controller)
  - Получение профиль пользователя
  - Редактирование профиль пользователя   