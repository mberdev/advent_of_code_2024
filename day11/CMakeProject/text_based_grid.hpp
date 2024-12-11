#ifndef TEXT_BASED_GRID_HPP
#define TEXT_BASED_GRID_HPP

#include <vector>
#include <string>

class TextBasedGrid {
public:
    TextBasedGrid(const std::vector<std::string>& lines) : lines(lines) {}

    char getAt(int x, int y) const;
    int width();

    int height();

private:
    std::vector<std::string> lines;
};

#endif // TEXT_BASED_GRID_HPP

