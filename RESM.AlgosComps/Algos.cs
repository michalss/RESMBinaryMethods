using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace RESM.AlgosComps
{
    public class Algos
    {

        [DllImport("quickbms.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern int quickbms_compression(string algo, byte[] inData, int inSize, byte[] outData, int outSize);

        [DllImport("quickbms.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern int quickbms_compression2(string algo, byte[] in_data, int zsize, byte[] out_data, int size, byte[] more_data, int more_size);

        [DllImport("quickbms.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern int quickbms_encryption(string algo, byte[] in_data, int zsize, byte[] out_data, int size, int operation, byte[] more_data, int more_size);


        private int _buffer = 1000000;

        public int Buffer
        {
            get { return _buffer; }
            set { _buffer = value; }
        }

        public enum DeCompType
        {
            zlib,
            lzo1x, 
            lz4,
            deflate,
            lzo1a,
            lzss

        }
        public enum CompType
        {
            zlib_compress,
            lzo1x_compress,
            lz4_compress,
            deflate_compress,
            lzo1a_compress,
            lzss_compress
        }

        public enum EcryptType
        {
            aes_128_ecb
        }

        public byte[] Decompress(DeCompType algo, byte[] compressData, int zsize, int size = 0)
        {
            if (size == 0)
            {
                size = _buffer; // use 1 MB if not sure
            }

            byte[] outData = new byte[size];
            var return_data = quickbms_compression(algo.ToString(), compressData, zsize, outData, size);

            if (return_data < 0)
            {
                return_data = 0;// if 0 means some error not sure need to handle it!
            }

            var decompressedData = new byte[return_data];
            if (size > 0)
            {
                Array.Copy(outData, decompressedData, return_data);
            }

            return decompressedData;
        }


        public byte[] Decompress(DeCompType algo, byte[] dictionary, byte[] compressedData, int size = 0)
        {

            byte[] decompressedData;

            if (compressedData.Length == 0)
            {
                decompressedData = new byte[0];
                return [];
            }

            if (size == 0)
                size = _buffer; // Use 1 MB if not sure

            byte[] outData = new byte[size];

            int return_size = quickbms_compression2(algo.ToString(), dictionary, dictionary.Length, compressedData, compressedData.Length, outData, size);

            if (return_size < 0)
                return_size = 0; // Handle error codes simply - creates empty array in case of error

            decompressedData = new byte[return_size];

            // Copy data from native buffer to managed buffer
            if (return_size > 0)
            {
                Array.Copy(outData, decompressedData, return_size);
            }

            return decompressedData;
        }

        public byte[] Compress(CompType algo, byte[] decompressedData, int size = 0)
        {
            byte[] compressedData;

            if (decompressedData.Length == 0)
            {
                compressedData = new byte[0];
                return [];
            }

            if (size == 0)
                size = _buffer; // Use 1 MB if not sure

            byte[] outData = new byte[size];

            int return_size = quickbms_compression(algo.ToString(), decompressedData, decompressedData.Length, outData, size);

            if (return_size < 0)
                return_size = 0; // Handle error codes simply - creates empty array in case of error

            compressedData = new byte[return_size];

            // Copy data from native buffer to managed buffer
            if (return_size > 0)
            {
                Array.Copy(outData, compressedData, return_size);
            }

            return compressedData;


        }

        public byte[] Compress(CompType algo, byte[] dictionary, byte[] decompressedData, int size = 0)
        {
            byte[] compressedData;
            if (decompressedData.Length == 0)
            {
                compressedData = new byte[0];
                return [];
            }

            if (size == 0)
                size = _buffer; // Use 1 MB if not sure

            byte[] outData = new byte[size];

            int return_size = quickbms_compression2(algo.ToString(), dictionary, dictionary.Length, decompressedData, decompressedData.Length, outData, size);

            if (return_size < 0)
                return_size = 0; // Handle error codes simply - creates empty array in case of error

            compressedData = new byte[return_size];
            if (return_size > 0)
            {
                Array.Copy(outData, compressedData, return_size);
            }

            return compressedData;
        }


        public byte[] Encryption(EcryptType type, ref byte[] key, ref byte[] ivec, ref byte[] inputData)
        {
            byte[] outputData;
            if (inputData.Length == 0)
            {
                outputData = new byte[0];
                return [];
            }

            byte[] inoutData = new byte[inputData.Length];
            Array.Copy(inputData, inoutData, inputData.Length);

            int size = quickbms_encryption(type.ToString(), key, key.Length, ivec, ivec.Length, 1, inoutData, inputData.Length);

            if (size < 0) size = 0; // Handle error codes

            outputData = new byte[size];
            if (size > 0)
            {
                Array.Copy(inoutData, outputData, size);
            }

            return outputData;
        }


        public byte[] Decryption(EcryptType type, ref byte[] key, ref byte[] ivec, ref byte[] inputData)
        {
            byte[] outputData;
            if (inputData.Length == 0)
            {
                outputData = new byte[0];
                return [];
            }

            byte[] inoutData = new byte[inputData.Length];
            Array.Copy(inputData, inoutData, inputData.Length);

            int size = quickbms_encryption(type.ToString(), key, key.Length, ivec, ivec.Length, 0, inoutData, inputData.Length);

            if (size < 0) size = 0; // Handle error codes

            outputData = new byte[size];
            if (size > 0)
            {
                Array.Copy(inoutData, outputData, size);
            }

            return outputData;
        }
    }
}
