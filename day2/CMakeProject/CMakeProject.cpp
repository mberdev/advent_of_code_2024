#include "CMakeProject.h"
#include "infrastructure/file_reader.hpp"
#include "input_parser.hpp"

#include <vector>
#include <filesystem>
#include <iostream>
#include <algorithm>

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
        auto reports = InputParser::parseLines(lines);

        part1(reports);
        //part2(list1, list2);
    }

    void part1(std::vector<std::vector<int>>& reports)
    {
        int safeReportsCount = 0;
        for (const auto& report : reports) {
			if (report.size() == 0) {
				continue;
			}

            if (isSafeAnyModification(report)) {
				++safeReportsCount;
            }
        }

        cout << "Safe reports count: " << safeReportsCount << endl;
    }

    bool isSafeAnyModification(const std::vector<int>& report) {
        // Safe unchanged
        if (isReportSafe(report)) {
            return true;
        }

        // Safe modified
        for (int i = 0; i < report.size(); i++) {
            std::vector<int> modifiedReport = report;
            modifiedReport.erase(modifiedReport.begin() + i);
            if (isReportSafe(modifiedReport)) {
                return true;
            }
        }
        return false;
    }
    bool isReportSafe(const std::vector<int>& report)
    {
        if (report.size() == 1) {
			return true;
        }

        int previousValue = report[0];
		int direction = report[1] - report[0] > 0 ? 1 : -1;
        for (int i = 1; i < report.size(); i++) {
			int value = report[i];
            int step = abs(value - previousValue);
            if (step < 1 || step > 3) {
                return false;
            }
			int newDirection = value - previousValue > 0 ? 1 : -1;
			if (newDirection != direction) {
				return false;
			}
			previousValue = value;
        }

        return true;
    }

    void part2(std::vector<int>& list1, std::vector<int>& list2)
    {
        std::sort(list1.begin(), list1.end());
        std::sort(list2.begin(), list2.end());

        //Control
        cout << "Smallest number in list1 " << list1.front() << endl;
        cout << "Smallest number in list2 " << list2.front() << endl;
        cout << "Largest number in list1 " << list1.back() << endl;
        cout << "Largest number in list2 " << list2.back() << endl;

		cout << "Calculating similarity score... (" << list1.size() << " items)" << endl;

        int similarityScore = 0;

        int i1 = 0;
        int i2 = 0;
		while (i1 < list1.size() && i2 < list2.size()) {

            // Skip items that are in left list but not in right list, or vice versa
            while (i1 < list1.size() && i2 < list2.size() && list1[i1] != list2[i2]) {
                while (i1 < list1.size() && i2 < list2.size() && list1[i1] < list2[i2]) {
                    ++i1;
                }
				while (i1 < list1.size() && i2 < list2.size() && list1[i1] > list2[i2]) {
					++i2;
				}
            }

            // Item in both lists; count it.
            int counter = 0;
            while (i2 < list2.size() && list1[i1] == list2[i2]) {
                ++counter;
                ++i2;
            }

            int  value = (list1[i1] * counter);
			similarityScore += value;

            // Optimization: Items of left list that might appear several times, no need to compute again.
            if (i1 < list1.size()) {
                ++i1;
            }
            while (i1 < list1.size() && list1[i1] == list1[i1 - 1]) {
				similarityScore += value;
                ++i1;
            }
        }

        cout << "Similarity score: " << similarityScore << endl;
    }   
};

int main() {
    App app;
    app.run();
    return 0;
}
