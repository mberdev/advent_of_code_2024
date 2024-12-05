#include "puzzle_data.hpp"
#include <sstream>
#include <iostream>

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

void PuzzleData::print() const {
	std::cout << "Rules:" << std::endl;
	for (const auto& rule : rules) {
		std::cout << rule.first << ", " << rule.second << std::endl;
	}
	std::cout << "Updates:" << std::endl;
	for (const auto& update : updates) {
		for (const auto& number : update) {
			std::cout << number << " ";
		}
		std::cout << std::endl;
	}
}
