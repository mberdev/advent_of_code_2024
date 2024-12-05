#include "puzzle_Data.hpp"
#include <sstream>

PuzzleData::PuzzleData(const std::vector<std::string>& lines) {
    bool parsingRules = true;

    for (const auto& line : lines) {
        if (line.empty()) {
            parsingRules = false;
            continue;
        }

        if (parsingRules) {
            // Parse rules
            std::istringstream iss(line);
            int first, second;
            char delimiter;
            if (iss >> first >> delimiter >> second && delimiter == '|') {
                rules.emplace_back(first, second);
            }
        }
        else {
            // Parse updates
            std::istringstream iss(line);
            std::vector<int> update;
            int number;
            char delimiter;
            while (iss >> number) {
                update.push_back(number);
                iss >> delimiter; // Skip the comma
            }
            updates.push_back(update);
        }
    }
}
