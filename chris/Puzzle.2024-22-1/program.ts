import * as fs from 'fs';

const data = fs.readFileSync('input.txt', 'utf8');
var numbers = data.split("\n").map(l => parseInt(l)).filter(n => !isNaN(n));

function numToBinary(num) {
	var binary = [];
	while (num >= 1) {
		binary.unshift(num % 2 == 1);
		num = Math.floor(num / 2);
	}
	while (binary.length < 24) {
		binary.unshift(false);
	}
	return binary;
}

function binaryToNum(binary) {
	var num = 0;
	for (var i = 0; i < 24; i++) {
		num = num * 2 + binary[i];
	}
	return num;
}

function mulMix(binary, shift) {
	var newBinary = [];
	for (var i = 0; i < 24; i++) {
		var shifted = i + shift;
		newBinary[i] = shifted < 0 || shifted > 23 ? binary[i] : binary[i] != binary[i + shift];
	}
	return newBinary;
}

function next(binary) {
	return mulMix(mulMix(mulMix(binary, 6), -5), 11);
}

var results = numbers.map((num) => {
	var secret = numToBinary(num);
	for (var i = 0; i < 2000; i++) {
		secret = next(secret);
	}
	return binaryToNum(secret);
});
var result = results.reduce((acc, i) => acc + i);
console.log(result);
