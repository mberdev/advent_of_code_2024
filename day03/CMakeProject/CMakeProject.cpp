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

        //part1(lines);
        part2(lines);
    }

    void part1(std::vector<std::string>& lines)
    {
        int total = 0;

        std::regex re("mul\\((\\d{1,3}),(\\d{1,3})\\)");
        for (const auto& line : lines) {
            std::sregex_iterator next(line.begin(), line.end(), re);
            std::sregex_iterator end;
            while (next != end) {
                std::smatch match = *next;
                if (match.size() == 3) {
                    int first = std::stoi(match[1].str());
                    int second = std::stoi(match[2].str());
                    std::cout << first << "*" << second << "=" << first * second << std::endl;
                    total += first * second;
                }
                next++;
            }
        }

        std::cout << "TOTAL: " << total << std::endl;
    }

    void part2(std::vector<std::string>& lines) {
        bool enabled = true;
        int total = 0;

        regex re("(mul\\((\\d{1,3}),(\\d{1,3})\\)|do\\(\\)|don't\\(\\))");
        for (const auto& line : lines) {
            std::sregex_iterator next(line.begin(), line.end(), re);
            std::sregex_iterator end;
            while (next != end) {
                std::smatch match = *next;
                if (match.size() == 4) {
                    if (match[0].str() == "do()") {
                        enabled = true;
						cout << "do()" << endl;
                    }
                    else if (match[0].str() == "don't()") {
                        enabled = false;
						cout << "don't()" << endl;
                    }
                    else {
                        int first = std::stoi(match[2].str());
                        int second = std::stoi(match[3].str());

						if (enabled)
                            total += first * second;

                        std::cout << (enabled? "":"/") << first << "*" << second << "=" << first * second << std::endl;

                    }
                }
                else {
					throw new std::exception("Invalid line");
                }
                next++;
            }
        }

        std::cout << "TOTAL: " << total << std::endl;
    }
};

int main() {
    App app;
    app.run();
    return 0;
}


