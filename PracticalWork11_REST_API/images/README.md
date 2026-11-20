# Скриншоты выполнения API

`architecture.svg` — схема компонентов, а **не** скриншот работы API.

Для выполнения требований преподавателя после запуска PostgreSQL и API сохраните сюда реальные скриншоты Postman:

1. `01-post.png` — POST `/api/students`, тело JSON и `201 Created`.
2. `02-get-all.png` — GET `/api/students`, массив студентов и `200 OK`.
3. `03-get-by-id.png` — GET `/api/students/1`, объект и `200 OK`.
4. `04-put.png` — PUT `/api/students/1`, тело и `204 No Content`.
5. `05-delete.png` — DELETE `/api/students/1`, `204 No Content`.

Снимайте экран **после** выполнения запросов. Id в URL должен совпадать с тем, что присвоила база данных.
