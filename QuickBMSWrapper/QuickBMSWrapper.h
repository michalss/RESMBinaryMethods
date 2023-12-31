#include <windows.h>

#pragma once

using namespace System;
using namespace System::Runtime::InteropServices;

namespace ReverseEngineering {
	public ref class QuickBMS
	{
		//Source: Luigi Auriemma
		//http://aluigi.altervista.org/quickbms.htm
		//https://zenhax.com/viewtopic.php?p=35965#p35965

	public:
		//methods
		QuickBMS(); 
		~QuickBMS();
		int QuickBMSDecompression(String^ decompress_algo, array<Byte>^% compressed_data, [Runtime::InteropServices::Out] array<Byte>^% decompressed_data, int estimated_size);
		int QuickBMSCompression(String^ compress_algo, array<Byte>^% decompressed_data, [Runtime::InteropServices::Out] array<Byte>^% compressed_data, int estimated_size);
		
		int QuickBMSDecompression(String^ decompress_algo, array<Byte>^% dictionary, array<Byte>^% compressed_data, [Runtime::InteropServices::Out] array<Byte>^% decompressed_data, int estimated_size);
		int QuickBMSCompression(String^ compress_algo, array<Byte>^% dictionary, array<Byte>^% decompressed_data, [Runtime::InteropServices::Out] array<Byte>^% compressed_data, int estimated_size);
		 
		int QuickBMSEncryption(String^ encrypt_algo, array<Byte>^% key, array<Byte>^% ivec, array<Byte>^% input_data, [Runtime::InteropServices::Out] array<Byte>^% output_data);
		int QuickBMSDecryption(String^ decrypt_algo, array<Byte>^% key, array<Byte>^% ivec, array<Byte>^% input_data, [Runtime::InteropServices::Out] array<Byte>^% output_data);
	private:


		HINSTANCE quickbmsdll;
		int (__stdcall *quickbms_compression)(const char *algo, void *in, int zsize, void *out, int size) = NULL;
		int (__stdcall *quickbms_compression2)(const char *algo, void *dictionary, int dictionary_len, void *in, int zsize, void *out, int size) = NULL;
		int (__stdcall *quickbms_encryption)(const char *algo, void *key, int keysz, void *ivec, int ivecsz, int mode, void *data, int size) = NULL;

	};
}
