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
#include <unordered_map>
#include <cstdint>

using namespace std;

class DataAfterStep {
public:
    int stonesCount;
    std::vector<int64_t> stones;

    DataAfterStep(int stonesCount, const std::vector<int64_t>& stones) : stonesCount(stonesCount), stones(stones) {}
    DataAfterStep() {}
};

class App {
public:
    void run() {
        try {
            string inputFilePath = (std::filesystem::current_path() / "input_data" / "test_input.txt").string();
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

        testPattern(67, 10);
        //part1(stones);
        part2(stones);
    }

	void testPattern(int stone, int count) {
		std::vector<int64_t> stones{ stone };
		for (int i = 0; i < count; i++)
		{
			stones = blinkStones1(stones);
			cout << i << ": ";
			for (const auto& stone : stones) {
				cout << stone << " ";
			}
			cout << endl;
		}
	}

    void part1(std::vector<int64_t>& stones)
    {
        int BLINK_COUNT = 5;

        for (int i = 1; i <= BLINK_COUNT; i++)
        {
            stones = blinkStones1(stones);

            // Control
            cout << i << ": ";
            for (const auto& stone : stones) {
                cout << stone << " ";
            }
            cout << endl;
            cout << "stones after " << i << " blinks: " << stones.size() << endl;
        }
    }

    std::vector<int64_t> blinkStones1(std::vector<int64_t>& stones) {
        std::vector<int64_t> updatedStones;
        for (const auto& stone : stones)
        {
            auto newStones = blinkOneStone1(stone);
            updatedStones.insert(updatedStones.end(), newStones.begin(), newStones.end());
        }
        return updatedStones;
    }

    std::vector<int64_t> blinkOneStone1(int64_t stone) {
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

    DataAfterStep& getAfterStep5(int64_t stone, std::unordered_map<int64_t, DataAfterStep>& stoneMap)
    {
        int STEP5 = 5;

        if (stone == 67)
            int i = 0; //DEBUG

        if (stoneMap.find(stone) != stoneMap.end()) {
            return stoneMap[stone];
        }

        cout << "getAfter5 " << stone << "... ";

        std::vector<int64_t> stones{ stone };
        for (int i = 0; i < STEP5; i++)
        {
            stones = blinkStones1(stones);
        }
        DataAfterStep data(static_cast<int>(stones.size()), stones);
        stoneMap[stone] = data;

        cout << "ok" << endl;
        return stoneMap[stone];
    }


    DataAfterStep& getAfterStep15(int64_t stone, std::unordered_map<int64_t, DataAfterStep>& stoneMap5, std::unordered_map<int64_t, DataAfterStep>& stoneMap15)
    {
        int STEP5 = 5;
        int STEP15 = 15;


        if (stoneMap15.find(stone) != stoneMap15.end()) {
            return stoneMap15[stone];
        }

        cout << "getAfter15 " << stone << "... ";

        std::vector<int64_t> stones{ stone };
        for (int i = 0; i < STEP5; i++)
        {
            stones = blinkStones1(stones);
        }
        DataAfterStep data(static_cast<int>(stones.size()), stones);
        stoneMap[stone] = data;

        cout << "ok" << endl;
        return stoneMap[stone];
    }


    int countRecursive(int64_t stone, std::unordered_map<int64_t, DataAfterStep>& stoneMap, int depth, int maxDepth, int step)
    {
		//cout << "depth: " << depth << endl;

        int total = 0;

        if (depth == maxDepth)
        {
            return 0;
        }

        auto& data = getAfterStep(stone, stoneMap, step);
        total += data.stonesCount;

        for (const auto& stone : data.stones) {
            total += countRecursive(stone, stoneMap, depth + 1, maxDepth, step);
        }

        return total;
    }

    void part2(std::vector<int64_t> stones) {

#define TARGET_BLINK_COUNT 75
        int STEP5 = 5;
        int STEP15 = 15;

        int total = 0;

        // early population, to start with something
        std::unordered_map<int64_t, DataAfterStep> stoneMap5;
        for (int i = 0; i < 10; i++) {
            cout << "Populating " << i << endl;
            getAfterStep5(i, stoneMap5);
        }

        std::unordered_map<int64_t, DataAfterStep> stoneMap15;
        for (int i = 0; i < 10; i++) {
            cout << "Populating " << i << endl;
            getAfterStep15(i, stoneMap5, STEP15);
        }

        for (const auto& stone : stones) {
            total += countRecursive(stone, stoneMap, 0, TARGET_BLINK_COUNT / STEP5);
        }

        cout << "count: " << total << endl;
    }

};

int main() {
    App app;
    app.run();
    return 0;
}





