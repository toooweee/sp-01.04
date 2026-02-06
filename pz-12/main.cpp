#include <iostream>
#include "factoriallib/FactorialLib.h"

using namespace std;

// TIP To <b>Run</b> code, press <shortcut actionId="Run"/> or click the <icon src="AllIcons.Actions.Execute"/> icon in the gutter.
int main() {
    unsigned int n;
    cout << "Enter n: ";
    cin >> n;

    cout << n << "! = " << factorial(n) << "\n";
    return 0;
}