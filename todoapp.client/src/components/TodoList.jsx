import { TodoItem } from './TodoItem';
import PropTypes from "prop-types";

export function TodoList({ todos, toggleTodo, deleteTodo }) {
  return (
    <>
      {todos.length === 0 ? (
        <p>No Todos yet!</p>
      ) : (
        <table className="table table-striped">
          <thead>
            <tr>
              {[
                <th key="title">Title</th>,
                <th key="text">Text</th>,
                <th key="deadline">Deadline</th>,
                <th key="status">Status</th>,
                <th key="actions">Actions</th>,
              ]}
            </tr>
          </thead>
          <tbody>
            {todos.map(todo => (
              <TodoItem
                key={todo.id}
                id={todo.id}
                title={todo.title}
                text={todo.text}
                deadline={todo.deadline}
                status={todo.status}
                completed={todo.completed}
                toggleTodo={toggleTodo}
                deleteTodo={deleteTodo}
              />
            ))}
          </tbody>
        </table>
      )}
    </>
  );
}

TodoList.propTypes = {
  todos: PropTypes.arrayOf(PropTypes.shape({
    id: PropTypes.string.isRequired,
    title: PropTypes.string.isRequired,
    text: PropTypes.string.isRequired,
    deadline: PropTypes.string.isRequired,
    status: PropTypes.string.isRequired,
    completed: PropTypes.bool.isRequired,
  })).isRequired,
  toggleTodo: PropTypes.func.isRequired,
  deleteTodo: PropTypes.func.isRequired,
};
