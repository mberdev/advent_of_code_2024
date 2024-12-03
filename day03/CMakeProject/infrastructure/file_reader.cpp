#include "./file_reader.hpp"
#include <fstream>
#include <string>
#include <stdexcept>
#include <vector>
#include <iostream>

std::vector<std::string> FileReader::readAll(const std::string& filePath, const bool showAfterReading) {
    std::ifstream fileStream(filePath, std::ios::in);
    if (!fileStream.is_open()) {
        throw std::runtime_error("Failed to open file: " + filePath);
    }

    std::vector<std::string> lines;
    std::string line;
    while (std::getline(fileStream, line)) {
        lines.push_back(line);
    }

	if (showAfterReading) {
       displayLines(lines);
	}

    return lines;
}

void FileReader::displayLines(std::vector<std::string>& lines)
{
    std::cout << "============= INPUT ===============" << std::endl;
    for (const auto& line : lines) {
        std::cout << line << std::endl;
    }
    std::cout << "===================================" << std::endl;
    std::cout << std::endl;
}
