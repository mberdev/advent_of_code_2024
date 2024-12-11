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
#include <unordered_set>

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

        int startX = -1; int startY = -1;

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

        auto start = GuardState(Position(startX, startY), UP);

        //auto result = part1(grid, start);

        part2(grid, start);
    }

    std::vector<GuardState> part1(TextBasedGrid& grid, GuardState& start) {
        std::vector<GuardState> result;

		GuardState pos = start;

        int xCount = 1;
        grid.setAt(pos, 'X');
		result.push_back(pos);

        while (true) {
            Position candidate = pos.inFront();
            if (!grid.isInGrid(candidate)) {
                break;
            }

            if (grid.getAt(candidate) == '.') {
                xCount++;
                pos = GuardState(candidate, pos.direction);
                grid.setAt(pos, 'X');
            }
            else if (grid.getAt(candidate) == 'X') {
                pos = GuardState(candidate, pos.direction);
            }
            else if (grid.getAt(candidate) == '#') {
				pos = GuardState(pos.pos, turnClockwise(pos.direction));
            }

            result.push_back(pos);

        }

        cout << "Part 1: " << xCount << endl;

        return result;
    }


    void part2(TextBasedGrid& grid, GuardState& start) {
		int loopCount = 0;
        int movesCount = 0;
        std::unordered_set<GuardState> visited;
		visited.insert(start);

        GuardState pos = start;

        while (true) {
            Position candidate = pos.inFront();

            // Guard exits grid
            if (!grid.isInGrid(candidate)) {
                break;
            }

            // Regular obstacle
            if (grid.getAt(candidate) == '#') {
                pos = GuardState(pos.pos, turnClockwise(pos.direction));
				visited.insert(pos);
            }

            // Simulated obstacle
            else {
				TextBasedGrid gridCopy = grid;
				gridCopy.setAt(candidate, '#');

                if (searchLoop(gridCopy, pos, visited)) {
					cout << "Position " << pos.toString() << " has a loop." << endl;
					loopCount++;
                }

                // Eventually, still move guard.
				pos = GuardState(candidate, pos.direction);
				visited.insert(pos);
            }

            movesCount++;
        }

        cout << "Part 2: " << loopCount << endl;

    }

    bool searchLoop(TextBasedGrid& grid, GuardState& start, const std::unordered_set<GuardState>& visited) {
        unordered_set<GuardState> visitedCopy = visited;

        GuardState pos = start;
        pos = GuardState(pos.pos, turnClockwise(pos.direction));
        visitedCopy.insert(pos);

        Position candidate = pos.inFront();

        while (true) {
            
            if (!grid.isInGrid(candidate)) {
                return false;
            }
            
            if (grid.getAt(candidate) == '#') {
                pos = GuardState(pos.pos, turnClockwise(pos.direction));
            }
            else {
                pos = GuardState(candidate, pos.direction);
            }

            if (visitedCopy.find(pos) != visitedCopy.end()) {
                // Already visited => it's a loop
                return true;
            }

            visitedCopy.insert(pos);

            candidate = pos.inFront();
        }
    }
};

int main() {
    App app;
    app.run();
    return 0;
}



