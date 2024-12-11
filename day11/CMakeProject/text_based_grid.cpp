#include "text_based_grid.hpp"

char TextBasedGrid::getAt(int x, int y) const {
    if (y < 0 || y >= lines.size() || x < 0 || x >= lines[y].size()) {
        return 0;
    }
    return lines[y][x];
}


int TextBasedGrid::width() {
    return lines.empty() ? 0 : lines[0].length();
}
int TextBasedGrid::height() {
    return lines.size();
}