#include "pch.h"
#include <iostream>
#include <string>
#include <fstream>

using namespace std;

struct Triangle
{
	int side1;
	int side2;
	int side3;
};

Triangle GetTriangle(const string &arg1, const string &arg2, const string &arg3)
{
	Triangle triangle;
	try
	{
		triangle.side1 = stoi(arg1);
		triangle.side2 = stoi(arg2);
		triangle.side3 = stoi(arg3);
	}
	catch (const invalid_argument&)
	{
		throw runtime_error("Unknown error\n");
	}
	catch (const out_of_range&)
	{
		throw runtime_error("Unknown error\n");
	}

	if (triangle.side1 <= 0 || triangle.side2 <= 0 || triangle.side3 <= 0)
	{
		throw runtime_error("Unknown error\n");
	}

	return triangle;
}

bool IsTriangle(const Triangle &triangle)
{
	return triangle.side1 + triangle.side2 > triangle.side3 &&
		triangle.side1 + triangle.side3 > triangle.side2 &&
		triangle.side2 + triangle.side3 > triangle.side1;
}

bool TriangleIsEquilateral(const Triangle &triangle)
{
	return triangle.side1 == triangle.side2 && triangle.side1 == triangle.side3;
}

bool TriangleIsIsosceles(const Triangle &triangle)
{
	return triangle.side1 == triangle.side2 ||
		triangle.side1 == triangle.side3 ||
		triangle.side2 == triangle.side3;
}

int main(int argc, char *argv[])
{
	ofstream fOut("output.txt");

	if (argc != 4)
	{
		fOut << "Unknown error\n";
		return 1;
	}

	Triangle triangle;
	try
	{
		triangle = GetTriangle(argv[1], argv[2], argv[3]);
	}
	catch (const runtime_error &err)
	{
		fOut << err.what() << endl;
		return 1;
	}

	if (!IsTriangle(triangle))
	{
		fOut << "Not a triangle\n";
		return 1;
	}

	if (TriangleIsEquilateral(triangle))
	{
		fOut << "Equilateral\n";
	}
	else if (TriangleIsIsosceles(triangle))
	{
		fOut << "Isosceles\n";
	}
	else
	{
		fOut << "Usual\n";
	}

	return 0;
}