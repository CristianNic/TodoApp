import PropTypes from "prop-types";

export function TodoItem({
 id,
 title,
 text,
 deadline,
 status,
 completed,
 toggleTodo,
 deleteTodo,
}) {
 return (
  <tr>
   <td>{title}</td>
   <td>{text}</td>
   <td>{new Date(deadline).toLocaleDateString()}</td>
   <td>{status}</td>
   <td>
    <input
     type="checkbox"
     checked={completed}
     onChange={(e) => toggleTodo(id, e.target.checked)}
     className="form-check-input me-2"
    />
    <button onClick={() => deleteTodo(id)} className="btn btn-danger btn-sm">
     Delete
    </button>
   </td>
  </tr>
 );
}

TodoItem.propTypes = {
  id: PropTypes.string.isRequired,
  title: PropTypes.string.isRequired,
  text: PropTypes.string.isRequired,
  deadline: PropTypes.string.isRequired,
  status: PropTypes.string.isRequired,
  completed: PropTypes.bool.isRequired,
  toggleTodo: PropTypes.func.isRequired,
  deleteTodo: PropTypes.func.isRequired,
};