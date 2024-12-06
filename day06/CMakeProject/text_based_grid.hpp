#pragma once
#include <vector>
#include <string>

enum Direction {
    UP,
    DOWN,
    LEFT,
    RIGHT
};

class Position {
public:
    int x, y;
    Position(int x, int y) : x(x), y(y) {}

    bool operator==(const Position& other) const {
        return x == other.x && y == other.y;
    }

    bool operator!=(const Position& other) const {
        return !(*this == other);
    }

    Position targetPosition(Direction direction) const {
        switch (direction) {
            case UP:
                return Position(x, y - 1);
            case DOWN:
                return Position(x, y + 1);
            case LEFT:
                return Position(x - 1, y);
            case RIGHT:
                return Position(x + 1, y);
            default:
				throw std::exception("Invalid direction");
        }
    }
};


class TextBasedGrid {
public:
    TextBasedGrid(const std::vector<std::string>& lines) : lines(lines) {}

    char getAt(int x, int y) const;
    void setAt(int x, int y, char c);

    int width();
    int height();

    bool isInGrid(int x, int y) { return x >= 0 && x < width() && y >= 0 && y < height(); }
	bool isInGrid(Position p) { return isInGrid(p.x, p.y); }

private:
    std::vector<std::string> lines;
};
