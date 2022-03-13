#pragma once
#include <windows.h>
#include <wincrypt.h>

struct dataFile
{
    int sizeFile;
    BYTE* data;
};

dataFile ReadFile(char* path);
void WriteFile(char* path, int size, BYTE* data);
void EncryptionFile(char* path);
void DecryptionFile(char* path);
void EncryptionFiles(char* path);
void DecryptionFiles(char* path);