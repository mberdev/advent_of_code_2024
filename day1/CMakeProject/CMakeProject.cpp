#include "CMakeProject.h"
#include "./infrastructure/file_reader.hpp"

#include <vector>
#include <filesystem>
#include <iostream>

using namespace std;

class App {
public:
    void run() {
        try {
            string inputFilePath = (std::filesystem::current_path() / "input_data" / "input.txt").string();
            std::vector<std::string> lines = FileReader::readAll(inputFilePath, true);
            processLines(lines);
            cout << "Done." << endl;
            cout << endl;
        }
        catch (const std::exception& e) {
            cerr << endl;
            cerr << "An error occurred: " << endl;
            cerr << e.what() << std::endl;
            cerr << "Done (with errors)." << endl;
            cerr << endl;
        }
    }

private:
    void processLines(std::vector<std::string>& lines) {
        for (const auto& line : lines) {
            std::cout << line << std::endl;
        }
    }
};

int main() {
    App app;
    app.run();
    return 0;
}
