#!/usr/bin/env groovy
data = new File('input.txt').text;
lines = data.split("\n");
rules = [];
updates = [];
split = false;
for (line in lines) {
	if (line == "") {
		split = true;
		continue;
	}
	if (split)
		updates << line.split(",").collect { it.toInteger() };
	else
		rules << line.split("\\|").collect { it.toInteger() };
}

sum = 0;
for (update in updates) {
	isValid = true;
	for (i = 1; i < update.size(); i++) {
		for (j = 0; j < i; j++) {
			if (rules.any({ it[0] == update[i] && it[1] == update[j] })) {
				isValid = false;
			}
		}
	}
	if (!isValid) {
		update.sort { l, r -> rules.any({ it[0] == l && it[1] == r }) ? -1 : 1 };
		sum += update[(int)(update.size() / 2)];
	}
}

println sum;
