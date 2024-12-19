import React, { useEffect, useState } from "react";
import axios from "axios";
import './App.css';

function App() {
  const [tasks, setTasks] = useState([]);
  const [editTask, setEditTask] = useState(null);
  const [error, setError] = useState(null);

  // Fetch tasks from the API
  useEffect(() => {
    axios.get("http://localhost:5266/api/tasks")
      .then((response) => {
        setTasks(response.data);
      })
      .catch((error) => {
        console.error("There was an error fetching the tasks!", error);
        setError("There was an error fetching the tasks.");
      });
  }, []);

  // Handle creating a new task
  const handleCreateTask = (newTask) => {
    newTask.dueDate = newTask.dueDate || new Date().toISOString(); // Default to current date if no due date

    axios.post("http://localhost:5266/api/tasks", newTask)
      .then((response) => {
        setTasks([...tasks, response.data]);
        setError(null);  // Clear error if the request succeeds
      })
      .catch((error) => {
        console.error("There was an error creating the task!", error);
        setError("There was an error creating the task.");
      });
  };

  // Handle updating a task
  const handleUpdateTask = (task) => {
    task.dueDate = task.dueDate || new Date().toISOString(); // Default to current date if no due date

    axios.put(`http://localhost:5266/api/tasks/${task.id}`, task)
      .then(() => {
        setTasks(tasks.map(t => t.id === task.id ? task : t));
        setEditTask(null);  // Clear edit mode
        setError(null);  // Clear error if the request succeeds
      })
      .catch((error) => {
        console.error("There was an error updating the task!", error);
        setError("There was an error updating the task.");
      });
  };

  // Handle deleting a task
  const handleDeleteTask = (id) => {
    axios.delete(`http://localhost:5266/api/tasks/${id}`)
      .then(() => {
        setTasks(tasks.filter(task => task.id !== id));
        setError(null);  // Clear error if the request succeeds
      })
      .catch((error) => {
        console.error("There was an error deleting the task!", error);
        setError("There was an error deleting the task.");
      });
  };

  return (
    <div className="App">
      <h1>Todo List</h1>

      {/* Error Message */}
      {error && <div className="error-message">{error}</div>}

      <div>
        <h3>Create New Task</h3>
        <TaskForm onSubmit={handleCreateTask} />
      </div>

      <h3>Task List</h3>
      <ul>
        {tasks.map(task => (
          <li key={task.id}>
            {editTask && editTask.id === task.id ? (
              <div>
                <input
                  type="text"
                  value={editTask.title}
                  onChange={(e) => setEditTask({ ...editTask, title: e.target.value })}
                />
                <input
                  type="text"
                  value={editTask.description}
                  onChange={(e) => setEditTask({ ...editTask, description: e.target.value })}
                />
                <input
                  type="datetime-local"
                  value={editTask.dueDate}
                  onChange={(e) => setEditTask({ ...editTask, dueDate: e.target.value })}
                />
                <label>
                  Completed:
                  <input
                    type="checkbox"
                    checked={editTask.isCompleted}
                    onChange={(e) => setEditTask({ ...editTask, isCompleted: e.target.checked })}
                  />
                </label>
                <button onClick={() => handleUpdateTask(editTask)}>Save</button>
                <button onClick={() => setEditTask(null)}>Cancel</button>
              </div>
            ) : (
              <div>
                <strong>{task.title}</strong> - {task.description} <br />
                Due: {new Date(task.dueDate).toLocaleString()} <br />
                Status: {task.isCompleted ? "Completed" : "Pending"} <br />
                <button onClick={() => setEditTask({ ...task })}>Edit</button>
                <button onClick={() => handleDeleteTask(task.id)}>Delete</button>
              </div>
            )}
          </li>
        ))}
      </ul>
    </div>
  );
}

// Task Form Component for Creating a New Task
function TaskForm({ onSubmit }) {
  const [newTask, setNewTask] = useState({ title: "", description: "", dueDate: "", isCompleted: false });

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setNewTask((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit(newTask);
    setNewTask({ title: "", description: "", dueDate: "", isCompleted: false });
  };

  return (
    <form onSubmit={handleSubmit}>
      <input
        type="text"
        name="title"
        placeholder="Title"
        value={newTask.title}
        onChange={handleChange}
        required
      />
      <input
        type="text"
        name="description"
        placeholder="Description"
        value={newTask.description}
        onChange={handleChange}
      />
      <input
        type="datetime-local"
        name="dueDate"
        value={newTask.dueDate || new Date().toISOString().slice(0, 16)} // Default value for due date
        onChange={handleChange}
      />
      <label>
        Completed:
        <input
          type="checkbox"
          name="isCompleted"
          checked={newTask.isCompleted}
          onChange={handleChange}
        />
      </label>
      <button type="submit">Create Task</button>
    </form>
  );
}

export default App;