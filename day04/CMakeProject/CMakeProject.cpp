#include "CMakeProject.h"
#include "infrastructure/file_reader.hpp"
#include "input_parser.hpp"

#include <vector>
#include <filesystem>
#include <iostream>
#include <algorithm>
#include <regex>
#include <cassert>
#include <tuple>
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
        //auto reports = InputParser::parseLines(lines);

        part1(lines);
        //part2(lines);
    }

    #include <string>

    // ...

    void part1(std::vector<std::string>& lines)
    {
        auto grid = TextBasedGrid(lines);
        std::string word = "XMAS";
        cout << "count: " << countWord(grid, word) << endl;
    }

    void part2(std::vector<std::string>& lines) {
 
    }

    int countWord(TextBasedGrid& grid, std::string& word) {
		int total = 0;
        int width = grid.width();
        int height = grid.height();
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                total += countWordAt(grid, word, x, y);
            }
        }
		return total;
    }

    int countWordAt(TextBasedGrid& grid, std::string& word, int x, int y) {
        int count = 0;

        for (int direction = 0; direction < 8; direction++) {
            count += searchDirection(grid, word, x, y, direction) ? 1 : 0;
        }

		return count;        
    }

    bool searchDirection(TextBasedGrid & grid, std::string & word, int x, int y, int direction)
    {
        int x_step = 0;
        int y_step = 0;
        switch (direction) {
            case 0:	x_step = 1; y_step = 0; break;
            case 1:	x_step = 1; y_step = 1; break;
            case 2:	x_step = 0; y_step = 1; break;
            case 3:	x_step = -1; y_step = 1; break;
            case 4:	x_step = -1; y_step = 0; break;
            case 5:	x_step = -1; y_step = -1; break;
            case 6:	x_step = 0; y_step = -1; break;
            case 7:	x_step = 1; y_step = -1; break;
        }

        for (int i = 0; i < word.size(); i++) {
            char c = grid.getAt(x, y);
            if (c == 0) {
                return false;
            }
            if (c != word[i]) {
                return false;
            }
            x += x_step;
            y += y_step;
        }
        return true;
    }
};

int main() {
    App app;
    app.run();
    return 0;
}


