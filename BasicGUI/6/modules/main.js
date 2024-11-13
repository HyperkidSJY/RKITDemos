// Import functions from separate modules for tasks, quotes, and date settings
import { loadTasks,addTask, clearCompletedTasks, searchTasks, sortTasks } from './taskManager.js';
// import { fetchQuote } from './quoteManager.js';
import { QuoteManager } from './quoteManager.js';
import { setMinDate } from './dateManager.js';

// Initialize tasks, quotes, and minimum date on page load
document.addEventListener("DOMContentLoaded", function() {
    loadTasks();
    // fetchQuote();
    setMinDate();

    quoteManager.fetchQuote();
    document.getElementById("quote-btn").addEventListener("click", () => quoteManager.fetchQuote());
});

//// Event listeners for UI
document.getElementById("sort-options").addEventListener("change", sortTasks);
document.getElementById("search-input").addEventListener("input", searchTasks);
document.getElementById("add-task-btn").onclick = addTask;
// document.getElementById("quote-btn").onclick = fetchQuote;
document.getElementById("clear-completed-btn").onclick = clearCompletedTasks;

const quoteManager = new QuoteManager('efb604ff0amshbf83b079bb9a9a0p16386cjsn631526761484');