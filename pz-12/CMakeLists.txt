cmake_minimum_required(VERSION 4.1)
project(pz_12)

set(CMAKE_CXX_STANDARD 20)

add_library(FactorialLib SHARED
        factoriallib/FactorialLib.cpp
)

target_include_directories(FactorialLib PUBLIC
        ${CMAKE_CURRENT_SOURCE_DIR}/factoriallib
)

target_compile_definitions(FactorialLib PRIVATE FACTORIALLIB_EXPORTS)

add_executable(pz_12 main.cpp)

target_link_libraries(pz_12 PRIVATE FactorialLib)

add_custom_command(TARGET pz_12 POST_BUILD
        COMMAND ${CMAKE_COMMAND} -E copy_if_different
        $<TARGET_FILE:FactorialLib>
        $<TARGET_FILE_DIR:pz_12>
)