#include "CMakeProject.h"
#include "./infrastructure/file_reader.hpp"

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
        auto result = parseStringPairs(lines);

        auto list1 = result.first;
        auto list2 = result.second;

        //// Control
        //for (const auto& item : list1) {
        //    cout << item << " ";
        //}
        //cout << endl;

        ////Control
        //for (const auto& item : list2) {
        //    cout << item << " ";
        //}
        //cout << endl;

        std::sort(list1.begin(), list1.end());
        std::sort(list2.begin(), list2.end());

        //Control
        cout << "Smallest number in list1 " << list1.front() << endl;
        cout << "Smallest number in list2 " << list2.front() << endl;
		cout << "Largest number in list1 " << list1.back() << endl;
		cout << "Largest number in list2 " << list2.back() << endl;

        int totalDistances = 0;
        for (size_t i = 0; i < list1.size(); ++i) {
            totalDistances += std::abs(list2[i] - list1[i]); // Calculate absolute value
        }

		cout << "Total distances: " << totalDistances << endl;
    }

    std::pair<std::vector<int>, std::vector<int>> parseStringPairs(const std::vector<std::string>& lines) {
        std::vector<int> list1;
        std::vector<int> list2;

        for (const auto& line : lines) {
            std::istringstream iss(line);
            int first, second;
            iss >> first >> second;
            list1.push_back(first);
            list2.push_back(second);
        }

		// Apparently, returning by-value in modern C++ is not a problem anymore thanks to RVO. Kids nowadays.
        return { std::move(list1), std::move(list2) };
    }
};

int main() {
    App app;
    app.run();
    return 0;
}
