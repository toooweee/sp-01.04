#include "FactorialLib.h"

unsigned long long factorial(unsigned int n)
{
    unsigned long long result = 1;

    for (unsigned int i = 2; i <= n; i++)
        result *= i;

    return result;
}