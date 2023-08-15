#include <iostream>

// The function that adds two integers
extern "C" __declspec(dllexport) int AddIntegers(int a, int b) {
    return a + b;
}
