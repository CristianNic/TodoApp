import { useEffect, useState } from 'react';
import { NewTodoForm } from './components/NewTodoForm';
import { TodoList } from './components/TodoList';
// import './App.css';
import axios from 'axios';

// const API_URL = import.meta.env.VITE_API_URL || "http://localhost:5001";
const API_URL = "http://localhost:5001";

function App() {
  const [todos, setTodos] = useState([]);
  const [showForm, setShowForm] = useState(false); // form visibility
  const [error, setError] = useState(null); // error state to display message

  console.log("API URL: ", API_URL); // Debugging the API URL

  useEffect(() => {
    axios
    .get(`${API_URL}/todos`)
    .then((response) => {
        console.log("Response data:", response.data);
        setTodos(response.data);
    })
    .catch((error) => {
        console.error("Error fetching todos:", error);
        setError(
        //  "Please refresh the page."
         `Please refresh the page. ${error.message}`
        );
    });
  }, []);

  function addTodo(newTodo) {
    
    console.log("New todo data:", newTodo); 

    axios
    .post(`${API_URL}/todos`, {
        title: newTodo.title,
        text: newTodo.text,
        deadline: newTodo.deadline,
        status: "Pending",
        completed: false,
    })
    .then((response) => {
        console.log("New todo created:", response.data); 
        setTodos((currentTodos) => [...currentTodos, response.data]);
        setShowForm(false);
    })
    .catch((error) => {
        if (error.response) {
        console.error("Server response error:", error.response.data); 
        } else {
        console.error("Error adding todo:", error.message); 
        }
        setError("Failed to add todo. Please try again.");
    });
  }

  function toggleTodo(id, completed) {
    const todoToUpdate = todos.find((todo) => todo.id === id);
    if (!todoToUpdate) return;

    const updatedTodo = {
      ...todoToUpdate,
      completed,
      status: completed ? "Completed" : "Pending",
    };

    axios
    .put(`${API_URL}/todos/${id}`, updatedTodo)
    .then(() => {
      setTodos((currentTodos) =>
      currentTodos.map((todo) => (todo.id === id ? updatedTodo : todo))
      );
    })
    .catch((error) => {
        console.error("Error updating todo:", error);
        setError("Failed to update todo. Please try again.");
    });
  }

  function deleteTodo(id) {
    axios
    .delete(`${API_URL}/todos/${id}`)
    .then(() => {
        setTodos((currentTodos) => currentTodos.filter((todo) => todo.id !== id));
    })
    .catch((error) => {
        console.error("Error deleting todo:", error);
        setError("Failed to delete todo. Please try again.");
    });
  }

  function handleShowForm() {
    setShowForm(true);
  }

  function handleHideForm() {
    setShowForm(false);
  }

  return (
    <div className="container">
    <nav className="navbar navbar-expand-lg navbar-light bg-light mb-3">
      <div className="container">
      <span className="navbar-text ms-auto text-muted">Todo App v1</span>
      </div>
    </nav>

    {error && (
      <div
        className="alert alert-danger alert-dismissible fade show"
        role="alert"
      >
        {error}
        <button
          type="button"
          className="btn-close"
          data-bs-dismiss="alert"
          aria-label="Close"
          onClick={() => setError(null)}
        ></button>
      </div>
    )}

    <h3 className="mb-3">Todo Items</h3>

    {todos && todos.length > 0 ? (
      <TodoList todos={todos} toggleTodo={toggleTodo} deleteTodo={deleteTodo} />
    ) : (
      <p>No todos available. Add some!</p>
    )}

    {showForm && <NewTodoForm onSubmit={addTodo} onClose={handleHideForm} />}

    <button
      className="btn btn-primary mt-3"
      onClick={handleShowForm}
    >
      Add an Item
    </button>
    </div>
  );
}

export default App;