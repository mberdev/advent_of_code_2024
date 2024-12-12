#include "region.hpp"
#include <iostream>
#include <optional>


ExtPosition* EdgesCounter::findPlotAbove(const ExtPosition& plot) {
    auto it = std::find_if(plots.begin(), plots.end(), [&](ExtPosition& p) {
        return p.x == plot.x && p.y == plot.y - 1;
    });
    if (it != plots.end()) {
        return &(*it);
    }
    return NULL;
}

ExtPosition* EdgesCounter::findPlotBelow(const ExtPosition& plot) {
	auto it = std::find_if(plots.begin(), plots.end(), [&](ExtPosition& p) {
		return p.x == plot.x && p.y == plot.y + 1;
		});
	if (it != plots.end()) {
		return &(*it);
	}
	return NULL;
}

ExtPosition* EdgesCounter::findPlotLeft(const ExtPosition& plot) {
	auto it = std::find_if(plots.begin(), plots.end(), [&](ExtPosition& p) {
		return p.x == plot.x - 1 && p.y == plot.y;
		});
	if (it != plots.end()) {
		return &(*it);
	}
	return NULL;
}

ExtPosition* EdgesCounter::findPlotRight(const ExtPosition& plot) {
	auto it = std::find_if(plots.begin(), plots.end(), [&](ExtPosition& p) {
		return p.x == plot.x + 1 && p.y == plot.y;
		});
	if (it != plots.end()) {
		return &(*it);
	}
	return NULL;
}


int EdgesCounter::countEdges() {
	int edgesCount = 0;

	for (auto& plot : plots) {
		if (plot.processedTop && plot.processedBottom && plot.processedLeft && plot.processedRight) {
			continue;
		}

		if (!plot.processedTop) {
			auto plotAbove = findPlotAbove(plot);
			if (!plotAbove) {
				edgesCount++;
				plot.processedTop = true;

			} else {
				if (!plotAbove->processedBottom) {
					plotAbove->processedBottom = true;
					plot.processedTop = true;
					edgesCount++;
				}
				else {
					std::string message = "This should not happen at position " + std::to_string(plot.x) + ", " + std::to_string(plot.y);
					throw std::exception(message.c_str());
				}
			}
		}


		if (!plot.processedBottom) {
			auto plotBelow = findPlotBelow(plot);
			if (!plotBelow) {
				edgesCount++;
				plot.processedBottom = true;
			}
			else {
				if (!plotBelow->processedTop) {
					plotBelow->processedTop = true;
					plot.processedBottom = true;
					edgesCount++;
				}
				else {
					std::string message = "This should not happen at position " + std::to_string(plot.x) + ", " + std::to_string(plot.y);
					throw std::exception(message.c_str());
				}
			}
		}

		if (!plot.processedLeft) {
			auto plotLeft = findPlotLeft(plot);
			if (!plotLeft) {
				edgesCount++;
				plot.processedLeft = true;
			}
			else {
				if (!plotLeft->processedRight) {
					plotLeft->processedRight = true;
					plot.processedLeft = true;
					edgesCount++;
				}
				else {
					std::string message = "This should not happen at position " + std::to_string(plot.x) + ", " + std::to_string(plot.y);
					throw std::exception(message.c_str());
				}
			}
		}

		if (!plot.processedRight) {
			auto plotRight = findPlotRight(plot);
			if (!plotRight) {
				edgesCount++;
				plot.processedRight = true;
			}
			else {
				if (!plotRight->processedLeft) {
					plotRight->processedLeft = true;
					plot.processedRight = true;
					edgesCount++;
				}
				else {
					std::string message = "This should not happen at position " + std::to_string(plot.x) + ", " + std::to_string(plot.y);
					throw std::exception(message.c_str());
				}
			}
		}
	}

	return edgesCount;
}