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

var pricesPerSeller = numbers.map((num) => {
	var secret = numToBinary(num);
	var prices = [];
	prices.push(num % 10);
	for (var i = 0; i < 2000; i++) {
		secret = next(secret);
		prices.push(binaryToNum(secret) % 10);
	}
	return prices;
});

var sequences = {};
var sequencesPerSeller = pricesPerSeller
	.map(prices => {
		var priceSequences = {};
		for (var i = prices.length - 1; i >= 4; i--) {
			var sequence = (prices[i - 3] - prices[i - 4])
				+ "_" + (prices[i - 2] - prices [i - 3])
				+ "_" + (prices[i - 1] - prices [i - 2])
				+ "_" + (prices[i - 0] - prices [i - 1]);
			priceSequences[sequence] = prices[i];
			sequences[sequence] = 0;
		}
		return priceSequences;
	});

var result = Object.keys(sequences).reduce((acc, sequence) => {
	var sum = sequencesPerSeller.reduce((acc, prices) => {
		return acc + (prices[sequence] ?? 0);
	}, 0);
	return Math.max(acc, sum);
}, 0);

console.log(result);
