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
#include <string>
#include "input_data/puzzle_data.hpp"

using namespace std;

class App {
public:
    void run() {
        try {
            string inputFilePath = (std::filesystem::current_path() / "input_data" / "test_input.txt").string();
            std::vector<std::string> lines = FileReader::readAll(inputFilePath, false);

            PuzzleData puzzleData(lines);

            // Print rules
            std::cout << "Rules:" << std::endl;
            for (const auto& rule : puzzleData.getRules()) {
                std::cout << rule.first << ", " << rule.second << std::endl;
            }

            // Print updates
            std::cout << "Updates:" << std::endl;
            for (const auto& update : puzzleData.getUpdates()) {
                for (const auto& number : update) {
                    std::cout << number << " ";
                }
                std::cout << std::endl;
            }

            //processLines(lines);
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

        //part1(lines);
        part2(lines);
    }

    void part1(std::vector<std::string>& lines)
    {

        //cout << "count: " << countWord1(grid, word) << endl;
    }

    void part2(std::vector<std::string>& lines) {

        //cout << "count: " << total << endl;
    }
};

int main() {
    App app;
    app.run();
    return 0;
}


