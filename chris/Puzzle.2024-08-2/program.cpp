#include <iostream>
#include <fstream>
#include <string>
#include <filesystem>
#include <set>
#include <vector>
using namespace std;

int main () {
	ifstream myfile;
	myfile.open("input.txt");
	vector<char> grid;
	int width = 0;
	set<char> towers;
	if (myfile.is_open()) {
		char c;
		while (myfile) {
			myfile.get(c);
			if (c == '\n') {
				if (width == 0) {
					width = grid.size();
				}
				continue;
			}
			grid.push_back(c);
			if (c != '.') {
				towers.insert(c);
			}
		}
	}
	int height = grid.size() / width;

	set<int> antinodes;
	for (char tower : towers) {
		vector<int> positions;
		for (int i = 0; i < grid.size(); i++) {
			if (grid[i] == tower) {
				positions.push_back(i);
			}
		}
		//cout << tower << " x " << positions.size() << '\n';
		for (int i = positions.size() - 1; i > 0; i--) {
			for (int j = i - 1; j >= 0; j--) {
				int ix = positions[i] % width;
				int iy = positions[i] / width;
				int jx = positions[j] % width;
				int jy = positions[j] / width;
				//cout << ix << 'x' << iy << " - " << jx << 'x' << jy << '\n';
				int dx = ix - jx;
				int dy = iy - jy;
				int tx = ix;
				int ty = iy;
				while (tx >= 0 && tx < width && ty >= 0 && ty < height) {
					antinodes.insert(ty * width + tx);
					tx -= dx;
					ty -= dy;
				}
				tx = ix + dx;
				ty = iy + dy;
				while (tx >= 0 && tx < width && ty >= 0 && ty < height) {
					antinodes.insert(ty * width + tx);
					tx += dx;
					ty += dy;
				}
			}
		}
	}
	cout << antinodes.size() << '\n';
}
