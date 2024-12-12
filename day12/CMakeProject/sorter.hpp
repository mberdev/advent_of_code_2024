#pragma once
#include <set>
#include <vector>

class Sorter {
public:
    Sorter(const std::set<std::pair<int, int>>& rules) : rules(rules) {}

    std::vector<int> sort(const std::vector<int>& input) const;

private:
    std::set<std::pair<int, int>> rules;

    bool compareInts(int a, int b) const; // Add const qualifier
};
