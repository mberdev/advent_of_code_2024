#include "CMakeProject.h"
#include "infrastructure/file_reader.hpp"
//#include "input_parser.hpp"

#include <vector>
#include <filesystem>
#include <iostream>
#include <algorithm>
#include <regex>
#include <cassert>
#include <tuple>
#include <string>
#include "text_based_grid.hpp"

using namespace std;

class App {
public:
    void run() {
        try {
            string inputFilePath = (std::filesystem::current_path() / "input_data" / "input.txt").string();
            std::vector<std::string> lines = FileReader::readAll(inputFilePath, false);
            processLines(lines);
            cout << "Done." << endl << endl;
        }
        catch (const std::exception& e) {
            cerr << endl << "An error occurred: " << endl;
            cerr << e.what() << std::endl;
            cerr << "Done (with errors)." << endl << endl;
        }
    }

private:
    void processLines(std::vector<std::string>& lines) {

        int startX = -1; int startY =-1;

        for (int y = 0; y < lines.size(); ++y) {
            size_t x = lines[y].find('^');
            if (x != std::string::npos) {
                startX = x;
                startY = y;
                break;
            }
        }

		if (startX == -1 || startY == -1) {
			throw std::invalid_argument("No start position found");
		}

        auto grid = TextBasedGrid(lines);
		grid.setAt(startX, startY, '.');

        auto start = Position(startX, startY);
        //auto reports = InputParser::parseLines(lines);

        part1(grid, start);

        //part2(lines);
    }

    void part1(TextBasedGrid& grid, Position& start) {
        Position pos = start;

        int xCount = 1;
        Direction direction = Direction::UP;
		grid.setAt(pos.x, pos.y, 'X');
        while (true) {
            Position candidate = pos.targetPosition(direction);
            if (!grid.isInGrid(candidate)) {
                break;
            }

            if (grid.getAt(candidate.x, candidate.y) == '.') {
                xCount++;
                pos = candidate;
                grid.setAt(pos.x, pos.y, 'X');
            }
            else if (grid.getAt(candidate.x, candidate.y) == 'X') {
                pos = candidate;
            }
            else if (grid.getAt(candidate.x, candidate.y) == '#') {
                direction = turnRight(direction);
            }
        }

		cout << "Part 1: " << xCount << endl;
    }

    Direction turnRight(Direction direction) {
        switch (direction) {
        case Direction::UP:
            return Direction::RIGHT;
        case Direction::RIGHT:
            return Direction::DOWN;
        case Direction::DOWN:
            return Direction::LEFT;
        case Direction::LEFT:
            return Direction::UP;
        };
    }
};

int main() {
    App app;
    app.run();
    return 0;
}
