#ifndef FILE_READER_HPP
#define FILE_READER_HPP

#include <string>
#include <vector>

class FileReader {
public:
    FileReader();
    ~FileReader();

    static std::vector<std::string> readAll(const std::string& filePath, const bool showAfterReading);
    static void displayLines(std::vector<std::string>& lines);
};

#endif // FILE_READER_HPP
