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
#include <functional>
#include "input_data/puzzle_data.hpp"
#include "sorter.hpp"   
#include "text_based_grid.hpp"
#include "region.hpp"

using namespace std;

class App {
public:
    void run() {
        try {
            string inputFilePath = (std::filesystem::current_path() / "input_data" / "test_input2.txt").string();
            std::vector<std::string> lines = FileReader::readAll(inputFilePath, false);

            TextBasedGrid grid(lines);

            // Control
			//puzzleData.print();

			part1(grid);
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
    void part1(TextBasedGrid& grid)
    {
        int edgesCount = 0;
        int regionsCount = 0;

        Position candidate = grid.findNonEmpty();
        while (grid.isInGrid(candidate)) {
            regionsCount++;
            std::vector<Position> regionPlots = grid.floodFill(candidate, EMPTY_CHAR);

            EdgesCounter ec(regionPlots);
			int regionEdges = ec.countEdges();

			cout << "Region plots: " << regionPlots.size() << endl;
			cout << " Region edges: " << regionEdges << endl;
			cout << endl;

            candidate = grid.findNonEmpty();
        }

        cout << "regions: " << regionsCount << endl;
		cout << "edges: " << edgesCount << endl;
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
        Sorter sorter(puzzleData.getRulesAsSet());

        int total = 0;

        auto collapsedRules = collapseRules(puzzleData.getRules());


        for (const auto& update : puzzleData.getUpdates()) {

            if (!updateFollowsRules(update, collapsedRules)) {

                const auto sortedPages = sorter.sort(update);

                int middleElement = sortedPages[sortedPages.size() / 2];
                cout << middleElement << endl;
                total += middleElement;
            }
            else {
                cout << "valid update" << endl;
            }
        }

        cout << "total: " << total << endl;
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


