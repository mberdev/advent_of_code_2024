#ifndef INPUT_PARSER_HPP
#define INPUT_PARSER_HPP

#include <string>
#include <vector>

class InputParser {
public:
    InputParser();
    ~InputParser();

    static std::pair<std::vector<int>, std::vector<int>> InputParser::parseLines(const std::vector<std::string>& lines);

};

#endif // INPUT_PARSER_HPP
