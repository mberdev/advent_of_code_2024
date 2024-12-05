#pragma once

#include <vector>
#include <string>
#include <utility>

class PuzzleData {
public:
    PuzzleData(const std::vector<std::string>& lines);

    const std::vector<std::pair<int, int>>& getRules() const { return rules; }
    const std::vector<std::vector<int>>& getUpdates() const { return updates; }

private:
    std::vector<std::pair<int, int>> rules;
    std::vector<std::vector<int>> updates;
};



