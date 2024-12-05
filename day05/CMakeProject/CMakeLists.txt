# CMakeList.txt : CMake project for CMakeProject, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.8)

# Enable Hot Reload for MSVC compilers if supported.
if (POLICY CMP0141)
  cmake_policy(SET CMP0141 NEW)
  set(CMAKE_MSVC_DEBUG_INFORMATION_FORMAT "$<IF:$<AND:$<C_COMPILER_ID:MSVC>,$<CXX_COMPILER_ID:MSVC>>,$<$<CONFIG:Debug,RelWithDebInfo>:EditAndContinue>,$<$<CONFIG:Debug,RelWithDebInfo>:ProgramDatabase>>")
endif()

project ("CMakeProject")

# Add source to this project's executable.
add_executable (CMakeProject "CMakeProject.cpp" "CMakeProject.h" "input_parser.cpp" "infrastructure/file_reader.cpp"  "input_data/puzzle_data.cpp" "input_data/puzzle_data.hpp")

if (CMAKE_VERSION VERSION_GREATER 3.12)
  set_property(TARGET CMakeProject PROPERTY CXX_STANDARD 20)
endif()


# Specify the source directory and the destination directory
set(DATA_SOURCE_DIR "${CMAKE_SOURCE_DIR}/input_data")
set(DATA_DEST_DIR "${CMAKE_BINARY_DIR}/input_data")

# Copy the input_data directory to the execution folder
add_custom_command(
    TARGET CMakeProject POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy_directory
    ${DATA_SOURCE_DIR} ${DATA_DEST_DIR}
)