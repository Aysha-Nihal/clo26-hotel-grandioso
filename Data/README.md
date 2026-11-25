# Data

This is where the database connection is located.

- **EF Core:** a `DbContext`-class and migrations. The connection string is read from an environment variable or a local file included in `.gitignore`, never from the code.
- **Serverdatabas:** if you use one, describe how it is started in the project’s README. SQLite is sufficient for the task.

No ADO.NET. Use EF Core.

The files `exempel_*.md` are examples. Remove them once you have added your own classes.
