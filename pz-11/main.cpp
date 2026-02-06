#include <iostream>

#include "mathlib/MathLib.h"

using namespace std;

int main() {
    cout << "add(5, 2) = " << add(5, 2) << endl;
    cout << "subtract(5, 2) = " << subtract(5, 2) << endl;
    cout << "multiply(5, 2) = " << multiply(5, 2) << endl;
    cout << "divide(5, 2) = " << divide(5, 2) << endl;

    cout << "pow(2, 10) = " << pow(2, 10) << endl;
    cout << "pow(2, -3) = " << pow(2, -3) << endl;

    cout << "sqrt(27, 3) = " << sqrt(27, 3) << endl;
    cout << "sqrt(16, 4) = " << sqrt(16, 4) << endl;

    return 0;
}