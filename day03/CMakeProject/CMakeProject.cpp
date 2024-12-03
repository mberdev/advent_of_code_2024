#include "CMakeProject.h"
#include "infrastructure/file_reader.hpp"
#include "input_parser.hpp"

#include <vector>
#include <filesystem>
#include <iostream>
#include <algorithm>
#include <regex>
#include <cassert>

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
        int total = 0;
        bool enabled = true;

        std::regex re("mul\\((\\d{1,3}),(\\d{1,3})\\)(.*?)(?=mul\\(\\d{1,3},\\d{1,3}\\)|$)");
        std::regex action_re("(do\\(\\)|don't\\(\\))");
        for (const auto& line : lines) {
            std::sregex_iterator next(line.begin(), line.end(), re);
            std::sregex_iterator end;
            while (next != end) {
                std::smatch match = *next;
                if (match.size() >= 3) {
                    int first = std::stoi(match[1].str());
                    int second = std::stoi(match[2].str());

                    if (enabled) {
                        total += first * second;
                    }

                    std::cout << (enabled ? "" : "/") << first << "*" << second << "=" << first * second << "   (" << total << ")";

                    if (match.size() > 3) {
                        std::string following_text = match[3].str();

                        std::cout << "      " << following_text;

                        // Iterate over do() and don't()
                        int counter = 0;
                        std::sregex_iterator action_next(following_text.begin(), following_text.end(), action_re);
                        while (action_next != end) {
                            std::cout << std::endl;

                            std::smatch action_match = *action_next;
                            std::cout << "   Action: " << action_match.str();
                            if (action_match.str() == "do()") {
                                enabled = true;
                            }
                            else if (action_match.str() == "don't()") {
                                enabled = false;
                            }
                            counter++;
                            if (counter > 1) {
                                assert(false); //DEBUG
                            }
                            action_next++;
                        }
                    }
                }

				std::cout << std::endl;

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
