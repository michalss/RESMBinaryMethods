using System;
using System.Runtime.InteropServices;
using RESM.AlgosComps;
class Program
{

    static void Main(string[] args)
    {
        var algo = new Algos();
        //algo.Buffer = 200000;

        byte[] compressedData = new byte[] { 0x78, 0xDA, 0xAD, 0x8F, 0x31, 0x0E, 0xC3, 0x20, 0x10, 0x04, 0x7B, 0x24, 0xFE, 0x80, 0x94, 0x3A,
        0xB8, 0xA7, 0x8A, 0x53, 0x27, 0x45, 0x94, 0x17, 0x00, 0x3A, 0xC1, 0x29, 0x5C, 0x70, 0x1C, 0x23,
        0x27, 0x7E, 0xBD, 0x39, 0xB9, 0xE1, 0x01, 0xEC, 0x76, 0x33, 0xD2, 0x4A, 0x7B, 0xEA, 0x13, 0x29,
        0xB8, 0x8F, 0x82, 0xFE, 0x75, 0xBD, 0x3F, 0xA5, 0x70, 0x7F, 0x75, 0x2B, 0x18, 0x50, 0x8D, 0x65,
        0x46, 0x20, 0xB2, 0x52, 0xC0, 0x99, 0x2C, 0x26, 0xA3, 0x08, 0x2E, 0x36, 0xB1, 0xD3, 0x79, 0x0E,
        0x52, 0xAC, 0xE0, 0x8C, 0xAA, 0x69, 0x59, 0xCC, 0x04, 0x0C, 0xE3, 0xB2, 0x4C, 0x66, 0x18, 0x3E,
        0x3C, 0xEB, 0xE8, 0xAB, 0x7D, 0xA6, 0x2A, 0x21, 0x4D, 0x8D, 0xDC, 0xE0, 0x1D, 0xED, 0xEF, 0x50,
        0xDC, 0x6E, 0x87, 0x76, 0xE7, 0x38, 0x3D, 0x1C};


        Console.WriteLine($"Input size {compressedData.Length}");

        var bouter = algo.Decompress(Algos.DeCompType.zlib, compressedData, compressedData.Length);

        Console.WriteLine($"Output after size {bouter.Length}");

        if (bouter.Length >= 0)
        {
            Console.WriteLine(System.Text.Encoding.Default.GetString(bouter, 0, bouter.Length));
        }

        Console.WriteLine($"Start compressing Data..");
        var compData = algo.Compress(Algos.CompType.zlib_compress, bouter);
        Console.WriteLine($"Compression done with size: {compData.Length}");


        var decompressed = algo.Decompress(Algos.DeCompType.zlib, compData, compData.Length);
        Console.WriteLine($"DeCompression done with size: {decompressed.Length}");

        if (bouter.Length >= 0)
        {
            Console.WriteLine(System.Text.Encoding.Default.GetString(decompressed, 0, decompressed.Length));
        }


        ///ENCRYPTION
        ///
        Console.WriteLine("Encrypting...");
        Console.WriteLine("Input = " + decompressed.Length.ToString() + " bytes");

        byte[] key = new byte[] { 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44 };
        byte[] iv = new byte[] { };

        var crypted = algo.Encryption(Algos.EcryptType.aes_128_ecb, ref key, ref iv, ref decompressed);


        Console.WriteLine("Decrypting...");
        Console.WriteLine("Input = " + crypted.Length.ToString() + " bytes");

        var decrypted = algo.Decryption(Algos.EcryptType.aes_128_ecb, ref key, ref iv, ref crypted);
        if (decrypted.Length >= 0)
        {
            Console.WriteLine("######### Decrypted #######");
            Console.WriteLine(System.Text.Encoding.Default.GetString(decrypted, 0, decrypted.Length));
        }
    }
}
