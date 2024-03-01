'use strict';

console.log("Task 1:");
const sentence = "I can eat bananas all day";
console.log(sentence.slice(9, 17).toUpperCase());

console.log("Task 2:");
const autos = ["Saab", "Volvo", "BMW"];

console.log(autos[2]);
autos[0] = "Porsche";
console.log(autos);
autos.pop();
console.log(autos);
autos.push("Audi");

let indexVolvo = cars.indexOf("Volvo");
let indexBmw = cars.indexOf("BMW");

if (indexVolvo !== -1) {
   cars.splice(indexVolvo, 1);
}
if (indexBmw !== -1) {
   cars.splice(indexBmw, 1);
}
