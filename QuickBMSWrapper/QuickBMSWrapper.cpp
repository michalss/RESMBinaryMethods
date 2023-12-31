
#include "QuickBMSWrapper.h"
#include <msclr\marshal_cppstd.h>

namespace ReverseEngineering {

	QuickBMS::QuickBMS() {

		// first try load library
		quickbmsdll = LoadLibrary(L"quickbms.dll");
		if (quickbmsdll == NULL) {
			throw std::exception("Library quickbms.dll was not found!");
		}

		// get basic interfaces per allowed types
		quickbms_compression = (int(__stdcall *)(const char*, void*, int, void*, int))GetProcAddress((HMODULE)quickbmsdll, "quickbms_compression");
		if (quickbms_compression == NULL) {
			throw std::exception("QuickBMS: quickbms_compression was not found!");
		}
		quickbms_compression2 = (int(__stdcall *)(const char*, void*, int, void*, int, void*, int))GetProcAddress((HMODULE)quickbmsdll, "quickbms_compression2");
		if (quickbms_compression2 == NULL) {
			throw std::exception("QuickBMS: quickbms_compression2 was not found!");
		}
		quickbms_encryption = (int(__stdcall *)(const char*, void*, int, void*, int, int, void*, int))GetProcAddress((HMODULE)quickbmsdll, "quickbms_encryption");
		if (quickbms_encryption == NULL) {
			throw std::exception("QuickBMS: quickbms_encryption was not found!");
		}

	}

	QuickBMS::~QuickBMS() {
	
		if (quickbmsdll != NULL) {
			FreeLibrary((HMODULE)quickbmsdll);
			quickbmsdll = NULL;
		}

	}
	 
	int QuickBMS::QuickBMSDecompression(String^ decompress_algo, array<Byte>^% compressed_data, [Runtime::InteropServices::Out] array<Byte>^% decompressed_data, int estimated_size) {
		
		if (compressed_data->Length == 0) {

			decompressed_data = gcnew array<Byte>(0);

			return 1;
		}
		if (estimated_size == 0) estimated_size = 1000000; // use 1 mb if not sure

		std::string decomp_algo = msclr::interop::marshal_as<std::string>(decompress_algo);

		pin_ptr<Byte> data_pointer = &compressed_data[0];

		unsigned char *inData = data_pointer;

		unsigned char *outData = new unsigned char[estimated_size];

		int size = quickbms_compression(decomp_algo.c_str(), inData, compressed_data->Length, outData, estimated_size);
		

		if (size < 0) size = 0; // handle error codes simply - creates empty array in case of error

		decompressed_data = gcnew array<Byte>(size);

		// copy data from native buffer to managed buffer
		if (size > 0) {
			
			Marshal::Copy((IntPtr)outData, decompressed_data, 0, size);

			delete[] outData;
		}

		return 1;
	}
	int QuickBMS::QuickBMSCompression(String^ compress_algo, array<Byte>^% decompressed_data, [Runtime::InteropServices::Out] array<Byte>^% compressed_data, int estimated_size) {

		if (decompressed_data->Length == 0) {

			compressed_data = gcnew array<Byte>(0);

			return 1;
		}
		if (estimated_size == 0) estimated_size = 1000000; // use 1 mb if not sure

		std::string comp_algo = msclr::interop::marshal_as<std::string>(compress_algo);

		pin_ptr<Byte> data_pointer = &decompressed_data[0];

		unsigned char *inData = data_pointer;

		unsigned char *outData = new unsigned char[estimated_size];
		
		int size = quickbms_compression(comp_algo.c_str(), inData, decompressed_data->Length, outData, estimated_size);


		if (size < 0) size = 0; // handle error codes simply - creates empty array in case of error

		compressed_data = gcnew array<Byte>(size);

		// copy data from native buffer to managed buffer
		if (size > 0) {

			Marshal::Copy((IntPtr)outData, compressed_data, 0, size);

			delete[] outData;
		}

		return 1;
	}

	int QuickBMS::QuickBMSDecompression(String^ decompress_algo, array<Byte>^% dictionary, array<Byte>^% compressed_data, [Runtime::InteropServices::Out] array<Byte>^% decompressed_data, int estimated_size) {

		if (compressed_data->Length == 0) {

			decompressed_data = gcnew array<Byte>(0);

			return 1;
		}
		if (estimated_size == 0) estimated_size = 1000000; // use 1 mb if not sure

		std::string decomp_algo = msclr::interop::marshal_as<std::string>(decompress_algo);

		pin_ptr<Byte> data_pointer = &compressed_data[0];
		pin_ptr<Byte> dict_pointer = &dictionary[0];

		unsigned char *inData = data_pointer;

		unsigned char *outData = new unsigned char[estimated_size];

		unsigned char *dictData = dict_pointer;

		int size = quickbms_compression2(decomp_algo.c_str(), dictData, dictionary->Length, inData, compressed_data->Length, outData, estimated_size);


		if (size < 0) size = 0; // handle error codes simply - creates empty array in case of error

		decompressed_data = gcnew array<Byte>(size);

		// copy data from native buffer to managed buffer
		if (size > 0) {

			Marshal::Copy((IntPtr)outData, decompressed_data, 0, size);

			delete[] outData;
		}

		return 1;
	}
	int QuickBMS::QuickBMSCompression(String^ compress_algo, array<Byte>^% dictionary, array<Byte>^% decompressed_data, [Runtime::InteropServices::Out] array<Byte>^% compressed_data, int estimated_size) {

		if (decompressed_data->Length == 0) {

			compressed_data = gcnew array<Byte>(0);

			return 1;
		}
		if (estimated_size == 0) estimated_size = 1000000; // use 1 mb if not sure

		std::string comp_algo = msclr::interop::marshal_as<std::string>(compress_algo);

		pin_ptr<Byte> data_pointer = &decompressed_data[0];
		pin_ptr<Byte> dict_pointer = &dictionary[0];

		unsigned char *inData = data_pointer;

		unsigned char *outData = new unsigned char[estimated_size];

		unsigned char *dictData = dict_pointer;

		int size = quickbms_compression2(comp_algo.c_str(), dictData, dictionary->Length, inData, decompressed_data->Length, outData, estimated_size);


		if (size < 0) size = 0; // handle error codes simply - creates empty array in case of error

		compressed_data = gcnew array<Byte>(size);

		// copy data from native buffer to managed buffer
		if (size > 0) {

			Marshal::Copy((IntPtr)outData, compressed_data, 0, size);

			delete[] outData;
		}

		return 1;
	}

	int QuickBMS::QuickBMSEncryption(String^ encrypt_algo, array<Byte>^% key, array<Byte>^% ivec, array<Byte>^% input_data, [Runtime::InteropServices::Out] array<Byte>^% output_data) {
		 
		if (input_data->Length == 0) {

			output_data = gcnew array<Byte>(0);

			return 1;
		}

		std::string enc_algo = msclr::interop::marshal_as<std::string>(encrypt_algo);

		unsigned char *keyData = NULL;
		unsigned char *ivecData = NULL;
		
		unsigned char *inoutData = NULL;

		// copy data to temporary array - decryption changes input data!!! 
		inoutData = new unsigned char[input_data->Length];
		Marshal::Copy(input_data, 0, (IntPtr)inoutData, input_data->Length);

		if (key->Length > 0) {
			pin_ptr<Byte> key_pointer = &key[0];
			keyData = key_pointer;
		}
		if (ivec->Length > 0) {
			pin_ptr<Byte> ivec_pointer = &ivec[0];
			ivecData = ivec_pointer;
		}
		
		int size = quickbms_encryption(enc_algo.c_str(), keyData, key->Length, ivecData, ivec->Length, 1, inoutData, input_data->Length);


		if (size < 0) size = 0; // handle error codes simply - creates empty array in case of error

		output_data = gcnew array<Byte>(size);

		// copy data from native buffer to managed buffer
		if (size > 0) {

			Marshal::Copy((IntPtr)inoutData, output_data, 0, size);

			delete[] inoutData;
		}

		return 1;
	}

	int QuickBMS::QuickBMSDecryption(String^ decrypt_algo, array<Byte>^% key, array<Byte>^% ivec, array<Byte>^% input_data, [Runtime::InteropServices::Out] array<Byte>^% output_data) {

		if (input_data->Length == 0) {

			output_data = gcnew array<Byte>(0);

			return 1;
		}

		std::string dec_algo = msclr::interop::marshal_as<std::string>(decrypt_algo);

		unsigned char *keyData = NULL;
		unsigned char *ivecData = NULL;

		unsigned char *inoutData = NULL;

		// copy data to temporary array - decryption changes input data!!! 
		inoutData = new unsigned char[input_data->Length];
		Marshal::Copy(input_data, 0, (IntPtr)inoutData, input_data->Length);

		if (key->Length > 0) {
			pin_ptr<Byte> key_pointer = &key[0];
			keyData = key_pointer;
		}
		if (ivec->Length > 0) {
			pin_ptr<Byte> ivec_pointer = &ivec[0];
			ivecData = ivec_pointer;
		}

		int size = quickbms_encryption(dec_algo.c_str(), keyData, key->Length, ivecData, ivec->Length, 0, inoutData, input_data->Length);


		if (size < 0) size = 0; // handle error codes simply - creates empty array in case of error

		output_data = gcnew array<Byte>(size);

		// copy data from native buffer to managed buffer
		if (size > 0) {

			Marshal::Copy((IntPtr)inoutData, output_data, 0, size);

			delete[] inoutData;
		}

		return 1;
	}
}