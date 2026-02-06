cmake_minimum_required(VERSION 3.20)
project(pz_13)

set(CMAKE_CXX_STANDARD 20)

add_executable(pz_13 main.cpp)

add_subdirectory(../pz-11 pz-11-build)

target_link_libraries(pz_13 PRIVATE MathLib)
