#include "input_parser.hpp"
#include <fstream>
#include <string>
#include <stdexcept>
#include <vector>
#include <iostream>
#include <sstream>

std::vector<std::vector<int>> InputParser::parseLines(const std::vector<std::string>& lines) {
    std::vector<std::vector<int>> result;

    for (const auto& line : lines) {
        std::istringstream iss(line);
        std::vector<int> numbers;
        int number;
        while (iss >> number) {
            numbers.push_back(number);
        }
        result.push_back(numbers);
    }

    return result;
}