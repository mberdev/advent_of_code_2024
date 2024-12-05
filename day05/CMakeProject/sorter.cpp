#include "sorter.hpp"
#include <algorithm> // Include the correct header for std::sort
#include <functional>

std::vector<int> Sorter::sort(const std::vector<int>& unsorted) const {
    std::vector<int> sortedUpdate = unsorted;
    std::sort(sortedUpdate.begin(), sortedUpdate.end(), [this](int a, int b) { return this->compareInts(a, b); });
    return sortedUpdate;
}

bool Sorter::compareInts(int a, int b) const {
    // is there a rule a|b ?
    auto it = rules.find(std::make_pair(a, b));
    if (it != rules.end())
        return true;

    // is there a rule b|a ?
    it = rules.find(std::make_pair(b, a));
    if (it != rules.end())
        return false;

    // there is no rule. Leave the order unchanged. // (true == a < b)
    return a < b;
}
