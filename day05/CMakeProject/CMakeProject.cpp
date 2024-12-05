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
#include <string>
#include <unordered_map>
#include "input_data/puzzle_data.hpp"

using namespace std;

class App {
public:
    void run() {
        try {
            string inputFilePath = (std::filesystem::current_path() / "input_data" / "input.txt").string();
            std::vector<std::string> lines = FileReader::readAll(inputFilePath, false);

            PuzzleData puzzleData(lines);

            // Control
			//puzzleData.print();

			part1(puzzleData);
			//part2(puzzleData);

            cout << "Done." << endl << endl;
        }
        catch (const std::exception& e) {
            cerr << endl << "An error occurred: " << endl;
            cerr << e.what() << std::endl;
            cerr << "Done (with errors)." << endl << endl;
        }
    }

private:
    void part1(PuzzleData& puzzleData)
    {
        int total = 0;

        auto collapsedRules = collapseRules(puzzleData.getRules());

        for (const auto& update : puzzleData.getUpdates()) {

            if (updateFollowsRules(update, collapsedRules)) {

                // Corporate reflex : never trust the data.
                if (update.size() % 2 == 0) {
                    throw std::runtime_error("This update doesn't have a 'middle' element.");
                }

                int middleElement = update[update.size() / 2];

                cout << middleElement << endl;

				total += middleElement;

            }
            else {
                cout << "invalid update" << endl;
            }
        }

        cout << "total: " << total << endl;
    }

    bool updateFollowsRules(const std::vector<int>& update, const unordered_map<int, vector<int>>& collapsedRules)
    {
        for (int page = 0; page < update.size(); ++page) {
            if (collapsedRules.find(update[page]) == collapsedRules.end()) {
                // No rules for this page
                continue;
            }

            const auto& pagesThatMustComeAfterThisPage = collapsedRules.at(update[page]);

            std::vector<int> pagesBeforeThisPage(update.begin(), update.begin() + page);

            // check if there's a page in both vectors, i.e. a page which breaks the rule.
            for (const auto& pageBeforeThisPage : pagesBeforeThisPage) {
                if (std::find(pagesThatMustComeAfterThisPage.begin(), pagesThatMustComeAfterThisPage.end(), pageBeforeThisPage) != pagesThatMustComeAfterThisPage.end()) {
                    return false;
                }
            }
        }
        return true;
    }

	void part2(PuzzleData& puzzleData)
    {
        //cout << "count: " << total << endl;
    }

    unordered_map<int, vector<int>> collapseRules(const vector<pair<int, int>>& rules)
    {
        unordered_map<int, vector<int>> collapsedRules;
        for (const auto& [left, right] : rules) {
            collapsedRules[left].push_back(right);
        }
        return collapsedRules;
    }
};

int main() {
    App app;
    app.run();
    return 0;
}


