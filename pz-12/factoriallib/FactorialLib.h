#pragma once

#ifdef _WIN32
  #ifdef FACTORIALLIB_EXPORTS
    #define FACTORIALLIB_API __declspec(dllexport)
  #else
    #define FACTORIALLIB_API __declspec(dllimport)
  #endif
#else
  #define FACTORIALLIB_API
#endif

extern "C" FACTORIALLIB_API unsigned long long factorial(unsigned int n);
