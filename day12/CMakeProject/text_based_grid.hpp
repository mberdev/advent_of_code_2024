#ifndef TEXT_BASED_GRID_HPP
#define TEXT_BASED_GRID_HPP

#include <vector>
#include <string>

#define EMPTY_CHAR  ' '

class Position {
public:
	Position(int x, int y) : x(x), y(y) {}
	int x;
	int y;

	bool operator==(const Position& other) const {
		return x == other.x && y == other.y;
	}

	bool operator!=(const Position& other) const {
		return !(*this == other);
	}
};

class TextBasedGrid {
public:
    TextBasedGrid(const std::vector<std::string>& lines) : lines(lines) {}

    char getAt(int x, int y) const;
    int width();

    int height();

	Position findNonEmpty();

	bool isInGrid(Position position) {
		return position.x >= 0 && position.x < width() && position.y >= 0 && position.y < height();
	}

	std::vector<Position> floodFill(Position position, char replacement);

private:
    std::vector<std::string> lines;
};

#endif // TEXT_BASED_GRID_HPP

