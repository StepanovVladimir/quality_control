#include "pch.h"
#include <iostream>
#include <fstream>
#include <string>

using namespace std;

int main(int argc, char *argv[])
{
	ifstream tests("tests.txt");
	ofstream testResults("results.txt");
	string programPath = string(argv[1]);
	string args;
	
	int testNumber = 1;
	while (getline(tests, args))
	{
		string command = programPath + ' ' + args;
		system(command.data());

		ifstream fIn("output.txt");
		string result;
		getline(fIn, result);

		string expectedResult;
		getline(tests, expectedResult);

		if (result == expectedResult)
		{
			testResults << testNumber << " success;\n";
		}
		else
		{
			testResults << testNumber << " error;\n";
		}

		testNumber++;
	}
}