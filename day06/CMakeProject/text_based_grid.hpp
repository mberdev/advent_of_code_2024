#ifndef TEXT_BASED_GRID_HPP
#define TEXT_BASED_GRID_HPP

#include <vector>
#include <string>

enum Direction {
    UP,
    DOWN,
    LEFT,
    RIGHT
};

Direction turnClockwise(Direction direction);

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

    Position inFront(Direction direction) const {
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

class GuardState {
public:
    Position pos;
    Direction direction;

    GuardState(Position pos, Direction direction) : pos(pos), direction(direction) {}

    bool operator==(const GuardState& other) const {
        return pos == other.pos && direction == other.direction;
    }
    bool operator!=(const GuardState& other) const {
        return !(*this == other);
    };


	Position inFront() const { return pos.inFront(direction); }

	GuardState turnRight() const {
		return GuardState(pos, turnClockwise(direction));
	}

    
	std::string toString() const {
		return "(" + std::to_string(pos.x) + "," + std::to_string(pos.y) + ") " + std::to_string(direction);
	}

};

// to be able to use GuardState in an unordered_set
struct GuardStateHash {
    std::size_t operator()(const GuardState& gs) const {
        return std::hash<int>()(gs.pos.x) ^ std::hash<int>()(gs.pos.y) ^ std::hash<int>()(gs.direction);
    }
};

namespace std {
    template <>
    struct hash<GuardState> {
        std::size_t operator()(const GuardState& gs) const {
            return GuardStateHash()(gs);
        }
    };
}

class TextBasedGrid {
public:
    TextBasedGrid(const std::vector<std::string>& lines) : lines(lines) {}

    char getAt(int x, int y) const;
    char getAt(Position pos) const { return getAt(pos.x, pos.y); };
    char getAt(GuardState pos) const { return getAt(pos.pos); };

    void setAt(int x, int y, char c);
	void setAt(Position pos, char c) { setAt(pos.x, pos.y, c); }
    void setAt(GuardState pos, char c) { setAt(pos.pos, c); }

    int width();
    int height();

    bool isInGrid(int x, int y) { return x >= 0 && x < width() && y >= 0 && y < height(); }
	bool isInGrid(Position p) { return isInGrid(p.x, p.y); }

private:
    std::vector<std::string> lines;
};

#endif
