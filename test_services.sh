#!/bin/bash
echo "------------------------------------------"
echo "------------ Test Services: ------------- "
echo "------------------------------------------"

# Test webapp service
echo "webapp:"
curl -I http://localhost:3000

echo "------------------------------------------"

# Test webapi service
# echo "webapi:"
# curl -X GET http://localhost:5001/todos

echo "webapi:"
response=$(curl -s -X GET http://localhost:5001/todos)
echo $response

# Extract the first two IDs using awk
id1=$(echo $response | awk -F'"' '{print $4}')
id2=$(echo $response | awk -F'"' '{print $20}')

# Make GET requests for the first two IDs
echo "First ToDo Item:"
curl -X GET http://localhost:5001/todos/$id1
echo

echo "Second ToDo Item:"
curl -X GET http://localhost:5001/todos/$id2
echo

echo "------------------------------------------"

# Test POST request to add a new ToDo item
echo "Adding a new ToDo item:"
curl -X POST http://localhost:5001/todos \
-H "Content-Type: application/json" \
-d '{"title": "Test Todo", "text": "This is a test", "deadline": "2024-09-30T00:00:00Z", "status": "Pending", "completed": false}'
echo

echo "------------------------------------------"

# Test mongo service
echo "mongo:"
mongo --host localhost --port 27017 TodoDb --eval 'db.Todos.find().pretty()'

echo "------------------------------------------"

# List all running Docker containers
echo "docker ps"
docker ps