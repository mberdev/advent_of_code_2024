#include <vector>
#include <string>
#include <set>

class PuzzleData {
public:
    PuzzleData(const std::vector<std::string>& lines);

    const std::vector<std::pair<int, int>>& getRules() const { return rules; }
    const std::set<std::pair<int, int>>& getRulesAsSet() const { return rulesAsSet; }

    const std::vector<std::vector<int>>& getUpdates() const { return updates; }

    void print() const;

private:
    std::vector<std::pair<int, int>> rules;
    std::set<std::pair<int, int>> rulesAsSet;
    std::vector<std::vector<int>> updates;
};
