import 'dart:async';
import 'dart:io';
import 'dart:math';

void main() {
  File('input.txt').readAsString().then((String contents) {
    var lines = contents.split("\n").where((line) => line.length > 0);
		var cnt = 0;
		lines.forEach((line) {
			var numbers = line.replaceAll(":", "").split(" ").map((word) => int.parse(word)).toList();
			var match = numbers[0];
			var ops = <int>[];
			var sum = 0.0;
			do {
				if (ops.length < numbers.length - 1) {
					sum += numbers[ops.length + 1];
					ops.add(0); // +
					continue;
				}
				if (sum == match) {
					cnt += match;
					return;
				}
				while (ops.length < numbers.length) {
					var op = ops.removeLast();
					if (op == 0) { // +
						sum -= numbers[ops.length + 1];
						sum *= numbers[ops.length + 1];
						ops.add(1);
						break;
					} else if (op == 1) { // *
						sum = sum / numbers[ops.length + 1];
						sum = sum * pow(10, numbers[ops.length + 1].toString().length) + numbers[ops.length + 1];
						ops.add(2);
						break;
					} else { // ||
						sum = (sum / pow(10, numbers[ops.length + 1].toString().length)).floor() * 1.0;
					}
					if (ops.length == 0) {
						return;
					}
				}
			} while (ops.length > 0);
		});
		print(cnt);
  });
}
