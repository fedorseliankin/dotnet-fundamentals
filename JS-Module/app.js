console.log("Task 1:")
const sentense = "I can eat bananas all day";
console.log(sentense.slice(9, 17).toUpperCase());

console.log("Task 2:");
const cars = ["Saab", "Volvo", "BMW"];
console.log(cars);

console.log(cars[2]);


cars[0] = "Mercedes";
console.log(cars);

cars.pop();
console.log(cars);

cars.push("Audi");
console.log(cars);

const start = cars.indexOf("Volvo");
const deleteCount = 2;
cars.splice(start, deleteCount);
console.log(cars);