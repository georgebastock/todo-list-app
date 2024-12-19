# Todo List App

This is a Todo List App, It allows users to manage tasks, including CRUD operations (Create, Read, Update, Delete).

## Getting Started

These instructions will help you set up the project on your local machine.

### Setting Up the Backend

1. Clone the repo
2. Navigate to the TodoListApi project directory
    command - cd TodoListApi
3. Install dependencies
    command - dotnet restore
4. Apply the database migrations
    command - dotnet ef database update
5. Run the application
    command - dotnet run

The backend API should now be running at `http://localhost:5266`.

### Testing the API

You can test the API using Swagger or Postman.

Swagger will be available at:  
`http://localhost:5266/swagger/index.html`

### Available Endpoints:

- **GET /api/tasks**: Retrieve all tasks.
- **GET /api/tasks/{id}**: Retrieve a specific task by ID.
- **POST /api/tasks**: Create a new task.
- **PUT /api/tasks/{id}**: Update an existing task.
- **DELETE /api/tasks/{id}**: Delete a task by ID.

### Setting Up the Frontend

1. Navigate to the todo-list-frontend project directory in a new terminal
    command - cd todo-list-frontend
2. Install dependencies
    command - npm install
3. Run the frontend
    command - npm start

The frontend should now be running at `http://localhost:3000`.
