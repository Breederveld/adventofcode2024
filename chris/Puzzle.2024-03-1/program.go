package main

import (
	"fmt"
	"os"
	"strings"
	"regexp"
	"strconv"
	)

func check(e error) {
    if e != nil {
        panic(e)
    }
}

func main() {
		data, err := os.ReadFile("input.txt")
		check(err)
		expr := regexp.MustCompile(`mul\(\d{1,3},\d{1,3}\)`)
		matches := expr.FindAll(data, len(data))
		var sum int64
		sum = 0;
		for _, match := range matches {
			parts := strings.Split(string(match)[4:len(match)-1], ",")	
			left, err := strconv.Atoi(parts[0])
			check(err)
			right, err := strconv.Atoi(parts[1])
			check(err)
			sum += int64(left) * int64(right)
		}
    fmt.Println(sum)
}
