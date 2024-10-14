// LocalStorage Example
localStorage.setItem('name', 'Shivam');
console.log(localStorage.getItem('name')); // Outputs: 'Shivam'

// SessionStorage Example
sessionStorage.setItem('sessionName', 'Shivam Session');
console.log(sessionStorage.getItem('sessionName')); // Outputs: 'Shivam Session'


// Set Cookie
document.cookie = "username=Shivam; expires=Fri, 31 Dec 2024 12:00:00 UTC; path=/";

// Get Cookie
function getCookie(name) {
  let value = `; ${document.cookie}`;
  let parts = value.split(`; ${name}=`);
  if (parts.length === 2) return parts.pop().split(';').shift();
}
console.log(getCookie('username')); // Outputs: 'Shivam'


// Class using constructor function
function Person(name, age) {
    this.name = name;
    this.age = age;
}
Person.prototype.sayHello = function() {
    return `Hello, my name is ${this.name}`;
};

const person1 = new Person('Shivam', 21);
console.log(person1.sayHello()); // Output: "Hello, my name is Alice"

// ES6 Class syntax
class Animal {
    constructor(type, name) {
        this.type = type;
        this.name = name;
    }
    static identify() {
        return 'I am an animal';
    }
    speak() {
        return `${this.name} makes a sound`;
    }
}
const dog = new Animal('Dog', 'Oatmeal');
console.log(Animal.identify()); // Outputs: 'I am an animal'
console.log(dog.speak()); // Outputs: 'Buddy makes a sound'

/* Difference between var, let, and const */
function scopeTest() {
    var x = 1;
    let y = 2;
    const z = 3;

    if (true) {
        var x = 100; // var is function-scoped
        let y = 200; // let is block-scoped
        const z = 300; // const is block-scoped
        console.log(x, y, z); // Outputs: 100, 200, 300
    }

    console.log(x, y, z); // Outputs: 100, 2, 3 (var changes, let and const don't)
}
scopeTest();

/* Arrow functions and regular functions */
// Regular function
function regularFunction() {
    return "I am a regular function";
}
// Arrow function
const arrowFunction = () => "I am an arrow function";

console.log(regularFunction()); // Outputs: 'I am a regular function'
console.log(arrowFunction()); // Outputs: 'I am an arrow function'

/* Import, Export, async, and await */
// Assuming we have two files: `module.js` and `main.js`
// module.js
// export function greet() {
//     return "Hello from the module!";
// }

// // main.js
// import { greet } from './module.js';
// console.log(greet()); // Outputs: 'Hello from the module!'

// Async/Await Example
async function fetchData() {
    const response = await fetch('https://jsonplaceholder.typicode.com/todos/1');
    const data = await response.json();
    console.log(data); // Outputs: fetched data
}
fetchData();

// == vs ===
console.log(5 == '5');  // true (type coercion)
console.log(5 === '5'); // false (no type coercion)

// != vs !==
console.log(5 != '5');  // false (type coercion)
console.log(5 !== '5'); // true (no type coercion)