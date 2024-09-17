import { useState } from "react";
import PropTypes from "prop-types";

export function NewTodoForm({ onSubmit, onClose }) {

  const [title, setTitle] = useState("");
  const [text, setText] = useState("");
  const [deadline, setDeadline] = useState("");

  function handleSubmit(e) {
    e.preventDefault();
    if (title === "") return;
    onSubmit({ title, text, deadline });
    setTitle("");
    setText("");
    setDeadline("");
    onClose(); // Close the form after submission
  }

  return (
    <form onSubmit={handleSubmit} className="mb-4">
      <div className="mb-3">
        <label htmlFor="title" className="form-label">Title</label>
        <input
          value={title}
          onChange={e => setTitle(e.target.value)}
          type="text"
          id="title"
          className="form-control"
        />
      </div>
      <div className="mb-3">
        <label htmlFor="text" className="form-label">Text</label>
        <textarea
          value={text}
          onChange={e => setText(e.target.value)}
          id="text"
          className="form-control"
        ></textarea>
      </div>
      <div className="mb-3">
        <label htmlFor="deadline" className="form-label">Deadline</label>
        <input
          value={deadline}
          onChange={e => setDeadline(e.target.value)}
          type="date"
          id="deadline"
          className="form-control"
        />
      </div>
      <div className="d-flex justify-content-end">
        <button type="button" onClick={onClose} className="btn btn-secondary me-2">
          Cancel
        </button>
        <button type="submit" className="btn btn-primary">
          Add
        </button>
      </div>
    </form>
  );
}

NewTodoForm.propTypes = {
  onSubmit: PropTypes.func.isRequired, 
  onClose: PropTypes.func.isRequired,
};
