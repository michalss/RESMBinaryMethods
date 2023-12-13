using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

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

        public enum DeCompType
        {
            zlib,
            lzo1x
        }       
        public enum CompType
        {
            zlib_compress
        }


        public byte[] Decompress(DeCompType algo, byte[] inData, int zsize, int size = 0)
        {
            if (size == 0)
            {
                size = 1000000; // use 1 MB if not sure
            }

            byte[] outData = new byte[size];
            var return_data = quickbms_compression(algo.ToString(), inData, zsize, outData, size);

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

        public byte[] Compress(CompType algo, byte[] decompressedData, int size = 0)
        {
            byte[] compressedData; ;

            if (decompressedData.Length == 0)
            {
                compressedData = new byte[0];
                return [];
            }

            if (size == 0)
                size = 1000000; // Use 1 MB if not sure

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


    }
}
