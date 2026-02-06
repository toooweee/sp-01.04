cmake_minimum_required(VERSION 4.1)
project(pz_11)

set(CMAKE_CXX_STANDARD 20)

add_library(MathLib STATIC mathlib/MathLib.cpp)

target_include_directories(MathLib PUBLIC ${CMAKE_CURRENT_SOURCE_DIR}/mathlib)

add_executable(pz_11 main.cpp)

target_link_libraries(pz_11 PRIVATE MathLib)
