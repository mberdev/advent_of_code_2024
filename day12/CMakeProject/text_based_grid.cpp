#include "text_based_grid.hpp"
#include <iostream>

char TextBasedGrid::getAt(int x, int y) const {
    if (y < 0 || y >= lines.size() || x < 0 || x >= lines[y].size()) {
        return 0;
    }
    return lines[y][x];
}


int TextBasedGrid::width() {
    return lines.empty() ? 0 : lines[0].length();
}
int TextBasedGrid::height() {
    return lines.size();
}

Position TextBasedGrid::findNonEmpty() {
	for (int y = 0; y < height(); ++y) {
		for (int x = 0; x < width(); ++x) {
			if (getAt(x, y) != EMPTY_CHAR) {
				return Position(x, y);
			}
		}
	}
	return Position(-1, -1);
}

// Not a very good floodfill because doesn't use horizontal scanlines (but at least it's iterative and not recursive)
std::vector<Position> TextBasedGrid::floodFill(Position position, char replacement) {

	//std::cout << "Floodfilling at " << position.x << ", " << position.y << std::endl;

	char valueToReplace = getAt(position.x, position.y);
	if (valueToReplace == replacement) {
		throw std::exception("Already filled");
	}

	std::vector<Position> region;

	std::vector<Position> stack;
	stack.push_back(position);
	while (!stack.empty()) {
		Position current = stack.back();
		stack.pop_back();
		if (getAt(current.x, current.y) == valueToReplace) {
			region.push_back(current);
			lines[current.y][current.x] = replacement;
			if (current.x > 0) {
				stack.push_back(Position(current.x - 1, current.y));
			}
			if (current.x < width() - 1) {
				stack.push_back(Position(current.x + 1, current.y));
			}
			if (current.y > 0) {
				stack.push_back(Position(current.x, current.y - 1));
			}
			if (current.y < height() - 1) {
				stack.push_back(Position(current.x, current.y + 1));
			}
		}
	}

	//std::cout << "Region size: " << region.size() << std::endl;

	return region;
}