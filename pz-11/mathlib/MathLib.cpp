#include "MathLib.h"
#include <stdexcept>
#include <cmath>

double add(double a, double b) { return a + b; }
double subtract(double a, double b) { return a - b; }
double multiply(double a, double b) { return a * b; }
double divide(double a, double b) {
    if (b == 0) {
        return 0;
    }

    return a / b;
}


double pow(double x, int n) {
    if (n == 0) return 1;

    bool negative = false;
    if (n < 0) {
        negative = true;
        n = -n;
    }

    double result = 1;
    for (int i = 0; i < n; i++) {
        result *= x;
    }

    if (negative) {
        if (result == 0) throw std::runtime_error("нельзя возводить 0 в отриц степень");
        return 1.0 / result;
    }

    return result;
}

double sqrt(double x, int n) {
    if (n < 0) throw std::runtime_error("Нельзя");

    return std::pow(x, 1.0 / n);
}

