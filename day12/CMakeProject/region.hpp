#ifndef REGION_HPP
#define REGION_HPP

#include <vector>
#include <string>
#include "text_based_grid.hpp"
#include <optional>

class ExtPosition : public Position {
public:
	ExtPosition(int x, int y) : Position(x, y) {}
	bool processedTop;
	bool processedBottom;
	bool processedLeft;
	bool processedRight;
};

class EdgesCounter {
public:
	EdgesCounter(const std::vector<Position> plots) {
		for (const auto& plot : plots) {
			this->plots.push_back(ExtPosition(plot.x, plot.y));
		}
		//leftToProcess = this->plots.size();
	}

	ExtPosition* findPlotAbove(const ExtPosition& plot);
	ExtPosition* findPlotBelow(const ExtPosition& plot);
	ExtPosition* findPlotLeft(const ExtPosition& plot);
	ExtPosition* findPlotRight(const ExtPosition& plot);


	int countEdges();
private:
    std::vector<ExtPosition> plots;
	//int leftToProcess = 0;
};

#endif // REGION_HPP