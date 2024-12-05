#include "puzzle_data.hpp"
#include <sstream>
#include <iostream>

PuzzleData::PuzzleData(const std::vector<std::string>& lines) {
    bool parsingRules = true;

    for (const auto& line : lines) {
		// Divider between rules and updates
        if (line.empty()) {
            parsingRules = false;
            continue;
        }
        
        // Rules
        if (parsingRules) {
            
            std::istringstream iss(line);
            int first, second;
            char delimiter;
            if (iss >> first >> delimiter >> second && delimiter == '|') {
                rules.emplace_back(first, second);

                // assumption : no duplicates
                rulesAsSet.emplace(first, second);
            }
        }
		// Updates
        else {
            std::istringstream iss(line);
            std::vector<int> update;
            int number;
            char delimiter;
            while (iss >> number) {
                update.push_back(number);
                iss >> delimiter; // Skip the comma
            }

            // Corporate reflex : never trust the data.
            if (update.size() % 2 == 0) {
                throw std::runtime_error("This update doesn't have a 'middle' element.");
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
