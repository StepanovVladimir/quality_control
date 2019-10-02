#include "pch.h"
#include <iostream>
#include <fstream>
#include <string>

using namespace std;

int main(int argc, char *argv[])
{
	string programPath = string(argv[1]);
	programPath += " 2 2 3";
	cout << system(programPath.data());

	ifstream fIn("output.txt");
}