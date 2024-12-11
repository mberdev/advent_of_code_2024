#include "input_parser.hpp"
#include <fstream>
#include <string>
#include <stdexcept>
#include <vector>
#include <iostream>
#include <sstream>

std::vector<int64_t> InputParser::parseLines(const std::vector<std::string>& lines) {
    std::vector<int64_t> result;

    for (const auto& line : lines) {
        if (!line.empty()) {
            std::istringstream iss(line);
            int number;
            while (iss >> number) {
                result.push_back(number);
            }
            break; // Only parse the first non-empty line
        }
    }

    return result;
}