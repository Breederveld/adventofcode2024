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
				int x0 = ix * 2 - jx;
				int y0 = iy * 2 - jy;
				int x1 = jx * 2 - ix;
				int y1 = jy * 2 - iy;
				//cout << x0 << 'x' << y0 << '\n';
				//cout << x1 << 'x' << y1 << '\n';
				if (x0 >= 0 && x0 < width && y0 >= 0 && y0 < height) {
					antinodes.insert(y0 * width + x0);
				}
				if (x1 >= 0 && x1 < width && y1 >= 0 && y1 < height) {
					antinodes.insert(y1 * width + x1);
				}
			}
		}
	}
	cout << antinodes.size() << '\n';
}
