#include "text_based_grid.hpp"
#include <stdexcept>

char TextBasedGrid::getAt(int x, int y) const {
    if (y < 0 || y >= lines.size() || x < 0 || x >= lines[y].size()) {
        return 0;
    }
    return lines[y][x];
}

void TextBasedGrid::setAt(int x, int y, char c) {
    if (y < 0 || y >= lines.size() || x < 0 || x >= lines[y].size()) {
        throw std::out_of_range("Coordinates out of range");
    }
    this->lines[y][x] = c;
}


int TextBasedGrid::width() {
    return lines.empty() ? 0 : lines[0].length();
}
int TextBasedGrid::height() {
    return lines.size();
}



