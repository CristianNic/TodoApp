db = db.getSiblingDB('TodoDb');

db.createCollection("Todos");

db.Todos.insertMany([
  {
    Title: "Buy groceries",
    Text: "Milk, Eggs, Bread",
    Completed: false,
    Status: "Pending",
    Deadline: new Date(Date.now() + 3 * 24 * 60 * 60 * 1000) // 3 days from now
  },
  {
    Title: "Walk the dog",
    Text: "Take Max to the park",
    Completed: true,
    Status: "Completed",
    Deadline: new Date(Date.now() - 24 * 60 * 60 * 1000) // 1 day ago
  },
  {
    Title: "Finish project",
    Text: "Complete the final report",
    Completed: false,
    Status: "In Progress",
    Deadline: new Date(Date.now() + 7 * 24 * 60 * 60 * 1000) // 7 days from now
  }
]);
