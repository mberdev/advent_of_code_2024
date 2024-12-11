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
        auto stones = InputParser::parseLines(lines);

        // Control
        for (const auto& stone : stones) {
            cout << stone << " ";
        }
        cout << endl;

        part1(stones);
        //part2(lines);
    }

    void part1(std::vector<int64_t>& stones)
    {
        int BLINK_COUNT = 25;

        for (int i = 1; i <= BLINK_COUNT; i++)
        {
            stones = blink(stones);

            // Control
			//cout << i << ": ";
   //         for (const auto& stone : stones) {
   //             cout << stone << " ";
   //         }
   //         cout << endl;
            cout << "stones after " << i << " blinks: " << stones.size() << endl;

        }
    }

    std::vector<int64_t> blink(std::vector<int64_t>& stones) {
        std::vector<int64_t> updatedStones;
        for (const auto& stone : stones)
        {
            auto newStones = blinkOneStone(stone);
            updatedStones.insert(updatedStones.end(), newStones.begin(), newStones.end());
        }
        return updatedStones;
    }

    std::vector<int64_t> blinkOneStone(int64_t stone) {
        std::vector<int64_t> updatedStones;

        if (stone == 0) {
            updatedStones.push_back(1);
            return updatedStones;
        }
        else {
            std::string asString = std::to_string(stone);
            if (asString.length() % 2 == 0) {
                std::string leftHalf = asString.substr(0, asString.length() / 2);
				std::string rightHalf = asString.substr(asString.length() / 2);
                updatedStones.push_back(stoi(leftHalf));
                updatedStones.push_back(stoi(rightHalf));
                return updatedStones;
            }
            else {
                int64_t multiplied = stone * 2024;
                if (multiplied < 0) {
					throw std::runtime_error("Overflow");
                }
                updatedStones.push_back(multiplied);
                return updatedStones;
            }
        }
    }

    void part2(std::vector<std::string>& lines) {
        int total = 0;

		cout << "count: " << total << endl;
    }

};

int main() {
    App app;
    app.run();
    return 0;
}


