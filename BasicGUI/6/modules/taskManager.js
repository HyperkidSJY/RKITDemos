let tasks = []; // Array to store task objects
let editingTaskIndex = -1; // Tracks index of the task being edited

// Load tasks from local storage
export function loadTasks() {
    const storedTasks = JSON.parse(localStorage.getItem("tasks"));
    if (storedTasks) {
        tasks = storedTasks; 
    }
    renderTasks(); 
}

// Add a new task or update an existing task
export function addTask() {
    const taskInput = document.getElementById("task-input").value;
    const dateInput = document.getElementById("task-date").value;

    if (taskInput === "" || dateInput === "") return alert("Please enter a task and select a date.");

    if (editingTaskIndex >= 0) { // Update existing task
        tasks[editingTaskIndex] = { task: taskInput, date: dateInput, completed: false };
        editingTaskIndex = -1;
    } else { // Add new task
        tasks.push({ task: taskInput, date: dateInput, completed: false });
    }

    saveTasks();
    renderTasks();
    document.getElementById("task-input").value = "";
    document.getElementById("task-date").value = "";
}

// Render tasks to the page
function renderTasks() {
    const taskList = document.getElementById("task-list");
    taskList.innerHTML = "";

    tasks.forEach((task, index) => {
        let taskItem = document.createElement("li");
        const completedClass = task.completed ? 'completed' : ''; // Style if completed
        taskItem.innerHTML = `<span class="${completedClass}" onclick="toggleComplete(${index})">${task.task} - Due: ${new Date(task.date).toLocaleDateString()}</span>
                              <button id="editBtn" onclick="editTask(${index})">Edit</button>
                              <button id="deleteBtn" onclick="deleteTask(${index})">Delete</button>`;
        taskList.appendChild(taskItem);
    });
}

// Toggle task completion status
function toggleComplete(index) {
    tasks[index].completed = !tasks[index].completed; 
    saveTasks();
    renderTasks(); 
}

// Delete task by index
function deleteTask(index) {
    tasks.splice(index, 1); 
    saveTasks(); 
    renderTasks(); 
}

// Edit a task by setting its values in the input fields
function editTask(index) {
    const task = tasks[index];
    document.getElementById("task-input").value = task.task;
    document.getElementById("task-date").value = task.date; 
    editingTaskIndex = index; 
}

// Clear all completed tasks
export function clearCompletedTasks() {
    tasks = tasks.filter(task => !task.completed); 
    saveTasks();
    renderTasks();
}

// Search and filter tasks by input text
export function searchTasks() {
    const searchInput = document.getElementById("search-input").value.toLowerCase();
    const filteredTasks = tasks.filter(task => task.task.toLowerCase().includes(searchInput));
    renderFilteredTasks(filteredTasks);
}

// Render only filtered tasks
function renderFilteredTasks(filteredTasks) {
    const taskList = document.getElementById("task-list");
    taskList.innerHTML = "";

    filteredTasks.forEach((task, index) => {
        let taskItem = document.createElement("li");
        const completedClass = task.completed ? 'completed' : '';
        taskItem.innerHTML = `<span class="${completedClass}" onclick="toggleComplete(${index})">${task.task} - Due: ${new Date(task.date).toLocaleDateString()}</span>
                              <button id="editBtn" onclick="editTask(${index})">Edit</button>
                              <button id="deleteBtn" onclick="deleteTask(${index})">Delete</button>`;
        taskList.appendChild(taskItem);
    });
}

// Sort tasks based on selected criteria
export function sortTasks() {
    const sortOption = document.getElementById("sort-options").value;

    if (sortOption === "date-asc") { // Sort by ascending date
        tasks.sort((a, b) => new Date(a.date) - new Date(b.date));
    } else if (sortOption === "date-desc") { // Sort by descending date
        tasks.sort((a, b) => new Date(b.date) - new Date(a.date));
    }

    saveTasks();
    renderTasks();
}

// Save tasks to local storage
function saveTasks() {
    localStorage.setItem("tasks", JSON.stringify(tasks)); 
}

// Assign functions to window object for inline onclick usage
window.toggleComplete = toggleComplete;
window.editTask = editTask;
window.deleteTask = deleteTask;
