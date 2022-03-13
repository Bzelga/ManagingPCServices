#include"Cryptographer.h"
#include <wincrypt.h>
#include <vector>
#include <iostream>
#include <fstream>
#include <filesystem>

#pragma comment(lib, "crypt32.lib")
#pragma warning(disable:4996)

#define MY_ENCODING_TYPE  (PKCS_7_ASN_ENCODING | X509_ASN_ENCODING)

using namespace std;

void EncryptionFiles(char *path)
{
    for (auto& p : filesystem::recursive_directory_iterator(path))
    {
        if (!filesystem::is_directory(p))
        {
            char* path_chr = strdup(p.path().string().c_str());
            EncryptionFile(path_chr);
        }
    }
}

void DecryptionFiles(char* path)
{
    for (auto& p : filesystem::recursive_directory_iterator(path))
    {
        if (!filesystem::is_directory(p))
        {
            char* path_chr = strdup(p.path().string().c_str());
            DecryptionFile(path_chr);
        }
    }
}

void EncryptionFile(char *path)
{
    HCRYPTMSG hMsg;
    BYTE* pbContent;
    DWORD cbContent;
    DWORD cbEncodedBlob;
    BYTE* pbEncodedBlob;

   

    dataFile openFileData = ReadFile(path);

    pbContent = openFileData.data;
    cbContent = openFileData.sizeFile;

    cbEncodedBlob = CryptMsgCalculateEncodedLength(
        MY_ENCODING_TYPE,
        0,
        CMSG_DATA,
        NULL,
        NULL,
        cbContent);

    pbEncodedBlob = (BYTE*)malloc(cbEncodedBlob);

    hMsg = CryptMsgOpenToEncode(
        MY_ENCODING_TYPE,
        0,
        CMSG_DATA,
        NULL,
        NULL,
        NULL);

    CryptMsgUpdate(
        hMsg,
        pbContent,
        cbContent,
        TRUE);

    free(openFileData.data);

    CryptMsgGetParam(
        hMsg,
        CMSG_BARE_CONTENT_PARAM,
        0,
        pbEncodedBlob,
        &cbEncodedBlob);

    WriteFile(path, cbEncodedBlob, pbEncodedBlob);

    if (pbEncodedBlob)
        free(pbEncodedBlob);
    if (hMsg)
        CryptMsgClose(hMsg);
}

void DecryptionFile(char *path)
{
    HCRYPTMSG hMsg;
    DWORD cbDecoded;
    BYTE* pbDecoded;
    dataFile openFileData = ReadFile(path);

    hMsg = CryptMsgOpenToDecode(
        MY_ENCODING_TYPE,
        0,
        CMSG_DATA,
        NULL,
        NULL,
        NULL);

    CryptMsgUpdate(
        hMsg,
        openFileData.data,
        openFileData.sizeFile,
        TRUE);

    CryptMsgGetParam(
        hMsg,
        CMSG_CONTENT_PARAM,
        0,
        NULL,
        &cbDecoded);

    pbDecoded = (BYTE*)malloc(cbDecoded);


    CryptMsgGetParam(
        hMsg,
        CMSG_CONTENT_PARAM,
        0,
        pbDecoded,
        &cbDecoded);

    WriteFile(path, cbDecoded, pbDecoded);

    if (pbDecoded)
        free(pbDecoded);
    if (hMsg)
        CryptMsgClose(hMsg);
}

dataFile ReadFile(char* path)
{
    vector <BYTE> dataVector;
    dataFile data;
    char ch;

    ifstream file(path, ios_base::binary | ios::in);
    if (!file)
    {
        throw "File not exsist";
    }

    while (file.get(ch))
    {
        dataVector.push_back(ch);
    }

    file.close();
    int sizeArray = dataVector.size();
    BYTE* dataOpenFile = dataVector.data();

    data.sizeFile = sizeArray;
    data.data = (BYTE*)malloc(sizeArray);

    for (int i = 0; i < sizeArray; i++)
    {
        data.data[i] = dataOpenFile[i];
    }

    return data;
}

void WriteFile(char* path, int size, BYTE* data)
{
    ofstream file(path, ios_base::binary | ios::out);

    for (int i = 0; i < size; i++)
    {
        file << data[i];
    }

    file.close();
}