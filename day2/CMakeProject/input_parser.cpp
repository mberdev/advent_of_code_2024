#include "input_parser.hpp"
#include <fstream>
#include <string>
#include <stdexcept>
#include <vector>
#include <iostream>
#include <sstream>

std::pair<std::vector<int>, std::vector<int>> InputParser::parseLines(const std::vector<std::string>& lines) {
    std::vector<int> list1;
    std::vector<int> list2;

    for (const auto& line : lines) {
        std::istringstream iss(line);
        int first, second;
        iss >> first >> second;
        list1.push_back(first);
        list2.push_back(second);
    }

    /*
    //for (const auto& item : list1) {
    //    cout << item << " ";
    //}
    //cout << endl;

    //for (const auto& item : list2) {
    //    cout << item << " ";
    //}
    //cout << endl;
    */

    // Apparently, returning by-value in modern C++ is not a problem anymore thanks to RVO. Kids nowadays.
    return { std::move(list1), std::move(list2) };
}