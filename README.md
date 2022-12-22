# iSearch

This is an application to search for music, videos, app etc from iTunes. When the user clicks on the media, they are redirected to the iTunes preview page and the click is recorded in the system

## Installation

Use docker compose to install iSearch.

```bash
docker-compose up -d
```
This will start the azure-sql-edge database and start the webapp which connects to the containerized database. On startup the database migrations are applied to the database.


## License

[MIT](https://choosealicense.com/licenses/mit/)
