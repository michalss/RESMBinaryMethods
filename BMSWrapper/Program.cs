using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace QuickBMSWrapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ReverseEngineering.QuickBMS quickBMS = null;

            try
            {
                quickBMS = new ReverseEngineering.QuickBMS();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.ReadLine();

                return;
            }

            byte[] compressed_data_zlib = new byte[] { 0x78, 0xDA, 0xAD, 0x8F, 0x31, 0x0E, 0xC3, 0x20, 0x10, 0x04, 0x7B, 0x24, 0xFE, 0x80, 0x94, 0x3A,
        0xB8, 0xA7, 0x8A, 0x53, 0x27, 0x45, 0x94, 0x17, 0x00, 0x3A, 0xC1, 0x29, 0x5C, 0x70, 0x1C, 0x23,
        0x27, 0x7E, 0xBD, 0x39, 0xB9, 0xE1, 0x01, 0xEC, 0x76, 0x33, 0xD2, 0x4A, 0x7B, 0xEA, 0x13, 0x29,
        0xB8, 0x8F, 0x82, 0xFE, 0x75, 0xBD, 0x3F, 0xA5, 0x70, 0x7F, 0x75, 0x2B, 0x18, 0x50, 0x8D, 0x65,
        0x46, 0x20, 0xB2, 0x52, 0xC0, 0x99, 0x2C, 0x26, 0xA3, 0x08, 0x2E, 0x36, 0xB1, 0xD3, 0x79, 0x0E,
        0x52, 0xAC, 0xE0, 0x8C, 0xAA, 0x69, 0x59, 0xCC, 0x04, 0x0C, 0xE3, 0xB2, 0x4C, 0x66, 0x18, 0x3E,
        0x3C, 0xEB, 0xE8, 0xAB, 0x7D, 0xA6, 0x2A, 0x21, 0x4D, 0x8D, 0xDC, 0xE0, 0x1D, 0xED, 0xEF, 0x50,
        0xDC, 0x6E, 0x87, 0x76, 0xE7, 0x38, 0x3D, 0x1C};
            byte[] compressed_data_lzo1x = new byte[] { 0x12, 0x23, 0x20, 0x25, 0x02, 0x00, 0x0D, 0x0A, 0x64, 0x00, 0x00, 0x18, 0x51, 0x75, 0x69, 0x63,
        0x6B, 0x42, 0x4D, 0x53, 0x0D, 0x0A, 0x62, 0x79, 0x20, 0x4C, 0x75, 0x69, 0x67, 0x69, 0x20, 0x41,
        0x75, 0x72, 0x69, 0x65, 0x6D, 0x6D, 0x61, 0x0D, 0x0A, 0x65, 0x2D, 0x6D, 0x61, 0x69, 0x6C, 0x3A,
        0x20, 0x6D, 0x65, 0x40, 0x61, 0x6C, 0x6C, 0x03, 0x08, 0x2E, 0x6F, 0x72, 0x67, 0x0D, 0x0A, 0x77,
        0x65, 0x62, 0x3A, 0x20, 0x40, 0x00, 0x2A, 0x4E, 0x00, 0x68, 0x6F, 0x00, 0x09, 0x70, 0x02, 0x05,
        0x68, 0x74, 0x74, 0x70, 0x3A, 0x2F, 0x2F, 0x71, 0x78, 0x0A, 0x04, 0x62, 0x6D, 0x73, 0x2E, 0x63,
        0x6F, 0x6D, 0x53, 0x03, 0x65, 0x6C, 0x70, 0x29, 0x70, 0x00, 0x03, 0x7A, 0x65, 0x6E, 0x68, 0x61,
        0x78, 0xA8, 0x03, 0x64, 0x00, 0x20, 0x28, 0x40, 0x03, 0x11, 0x00, 0x00};
            byte[] compressed_data_deflate = new byte[] { 0xAD, 0x8F, 0x31, 0x0E, 0xC3, 0x20, 0x10, 0x04, 0x7B, 0x24, 0xFE, 0x80, 0x94, 0x3A, 0xB8, 0xA7,
        0x8A, 0x53, 0x27, 0x45, 0x94, 0x17, 0x00, 0x3A, 0xC1, 0x29, 0x5C, 0x70, 0x1C, 0x23, 0x27, 0x7E,
        0xBD, 0x39, 0xB9, 0xE1, 0x01, 0xEC, 0x76, 0x33, 0xD2, 0x4A, 0x7B, 0xEA, 0x13, 0x29, 0xB8, 0x8F,
        0x82, 0xFE, 0x75, 0xBD, 0x3F, 0xA5, 0x70, 0x7F, 0x75, 0x2B, 0x18, 0x50, 0x8D, 0x65, 0x46, 0x20,
        0xB2, 0x52, 0xC0, 0x99, 0x2C, 0x26, 0xA3, 0x08, 0x2E, 0x36, 0xB1, 0xD3, 0x79, 0x0E, 0x52, 0xAC,
        0xE0, 0x8C, 0xAA, 0x69, 0x59, 0xCC, 0x04, 0x0C, 0xE3, 0xB2, 0x4C, 0x66, 0x18, 0x3E, 0x3C, 0xEB,
        0xE8, 0xAB, 0x7D, 0xA6, 0x2A, 0x21, 0x4D, 0x8D, 0xDC, 0xE0, 0x1D, 0xED, 0xEF, 0x50, 0xDC, 0x6E,
        0x87, 0x76 };
            byte[] decompressed_data;
            byte[] compressed_data;
            string text;

            FileStream file = new FileStream("test_original.zlib", FileMode.Create);
            BinaryWriter bw = new BinaryWriter(file);

            bw.Write(compressed_data_zlib);

            bw.Close();

            file = new FileStream("test_original.lzo1x", FileMode.Create);
            bw = new BinaryWriter(file);

            bw.Write(compressed_data_lzo1x);

            bw.Close();

            file = new FileStream("test_original.deflate", FileMode.Create);
            bw = new BinaryWriter(file);

            bw.Write(compressed_data_deflate);

            bw.Close();

            Console.WriteLine("Decompressing (zlib)...");
            Console.WriteLine("Input = " + compressed_data_zlib.Length.ToString() + " bytes");

            try
            {
                quickBMS.QuickBMSDecompression("zlib", ref compressed_data_zlib, out decompressed_data, 10000);

                file = new FileStream("test_decompressed_zlib.dat", FileMode.Create);
                bw = new BinaryWriter(file);

                bw.Write(decompressed_data);

                bw.Close();

                Console.WriteLine("Output = " + decompressed_data.Length.ToString() + " bytes");
                Console.WriteLine("Decompressed data:");

                text = System.Text.Encoding.ASCII.GetString(decompressed_data);

                Console.WriteLine(text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.ReadLine();

                return;
            }

            Array.Clear(compressed_data_zlib, 0, compressed_data_zlib.Length);
            Array.Clear(decompressed_data, 0, decompressed_data.Length);

            Console.WriteLine("Decompressing (lzo1x)...");
            Console.WriteLine("Input = " + compressed_data_lzo1x.Length.ToString() + " bytes");

            try
            {
                quickBMS.QuickBMSDecompression("lzo1x", ref compressed_data_lzo1x, out decompressed_data, 10000);

                file = new FileStream("test_decompressed_lzo1x.dat", FileMode.Create);
                bw = new BinaryWriter(file);

                bw.Write(decompressed_data);

                bw.Close();

                Console.WriteLine("Output = " + decompressed_data.Length.ToString() + " bytes");
                Console.WriteLine("Decompressed data:");

                text = System.Text.Encoding.ASCII.GetString(decompressed_data);

                Console.WriteLine(text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.ReadLine();

                return;
            }

            Array.Clear(compressed_data_lzo1x, 0, compressed_data_lzo1x.Length);
            Array.Clear(decompressed_data, 0, decompressed_data.Length);

            Console.WriteLine("Decompressing (deflate)...");
            Console.WriteLine("Input = " + compressed_data_deflate.Length.ToString() + " bytes");

            try
            {
                quickBMS.QuickBMSDecompression("deflate", ref compressed_data_deflate, out decompressed_data, 10000);

                file = new FileStream("test_decompressed_deflate.dat", FileMode.Create);
                bw = new BinaryWriter(file);

                bw.Write(decompressed_data);

                bw.Close();

                Console.WriteLine("Output = " + decompressed_data.Length.ToString() + " bytes");
                Console.WriteLine("Decompressed data:");

                text = System.Text.Encoding.ASCII.GetString(decompressed_data);

                Console.WriteLine(text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.ReadLine();

                return;
            }

            Array.Clear(compressed_data_deflate, 0, compressed_data_deflate.Length);
            //Array.Clear(decompressed_data, 0, decompressed_data.Length); // not here reuse for test

            Console.WriteLine("Compressing (zlib)...");
            Console.WriteLine("Input = " + decompressed_data.Length.ToString() + " bytes");

            try
            {
                quickBMS.QuickBMSCompression("zlib_compress", ref decompressed_data, out compressed_data, 10000);

                file = new FileStream("test_compressed.zlib", FileMode.Create);
                bw = new BinaryWriter(file);

                bw.Write(compressed_data);

                bw.Close();

                Console.WriteLine("Output = " + compressed_data.Length.ToString() + " bytes");
                Console.WriteLine("Compressed data:");

                text = string.Join(string.Empty, Array.ConvertAll(compressed_data, b => b.ToString("X2")));

                Console.WriteLine(text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.ReadLine();

                return;
            }

            Array.Clear(compressed_data, 0, compressed_data.Length);

            Console.WriteLine("Compressing (lzo1x)...");
            Console.WriteLine("Input = " + decompressed_data.Length.ToString() + " bytes");

            try
            {
                quickBMS.QuickBMSCompression("lzo1x_compress", ref decompressed_data, out compressed_data, 10000);

                file = new FileStream("test_compressed.lzo1x", FileMode.Create);
                bw = new BinaryWriter(file);

                bw.Write(compressed_data);

                bw.Close();

                Console.WriteLine("Output = " + compressed_data.Length.ToString() + " bytes");
                Console.WriteLine("Compressed data:");

                text = string.Join(string.Empty, Array.ConvertAll(compressed_data, b => b.ToString("X2")));

                Console.WriteLine(text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.ReadLine();

                return;
            }

            Array.Clear(compressed_data, 0, compressed_data.Length);

            Console.WriteLine("Compressing (deflate)...");
            Console.WriteLine("Input = " + decompressed_data.Length.ToString() + " bytes");

            try
            {
                quickBMS.QuickBMSCompression("deflate_compress", ref decompressed_data, out compressed_data, 10000);

                file = new FileStream("test_compressed.deflate", FileMode.Create);
                bw = new BinaryWriter(file);

                bw.Write(compressed_data);

                bw.Close();

                Console.WriteLine("Output = " + compressed_data.Length.ToString() + " bytes");
                Console.WriteLine("Compressed data:");

                text = string.Join(string.Empty, Array.ConvertAll(compressed_data, b => b.ToString("X2")));

                Console.WriteLine(text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.ReadLine();

                return;
            }

            Array.Clear(compressed_data, 0, compressed_data.Length);

            Console.WriteLine("Encrypting...");
            Console.WriteLine("Input = " + decompressed_data.Length.ToString() + " bytes");

            byte[] key = new byte[] { 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44, 0x44 };
            byte[] iv = new byte[] { };

            byte[] crypted;

            try
            {
                quickBMS.QuickBMSEncryption("aes_128_ecb", ref key, ref iv, ref decompressed_data, out crypted);

                file = new FileStream("test_encrypted.dat", FileMode.Create);
                bw = new BinaryWriter(file);

                bw.Write(crypted);

                bw.Close();

                Console.WriteLine("Output = " + crypted.Length.ToString() + " bytes");
                Console.WriteLine("Encrypted data:");

                text = System.Text.Encoding.GetEncoding(1250).GetString(crypted);

                Console.WriteLine(text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.ReadLine();

                return;
            }

            Console.WriteLine("Decrypting...");
            Console.WriteLine("Input = " + crypted.Length.ToString() + " bytes");

            byte[] decrypted;

            try
            {
                quickBMS.QuickBMSDecryption("aes_128_ecb", ref key, ref iv, ref crypted, out decrypted);

                file = new FileStream("test_decrypted.dat", FileMode.Create);
                bw = new BinaryWriter(file);

                bw.Write(decrypted);

                bw.Close();

                Console.WriteLine("Output = " + decrypted.Length.ToString() + " bytes");
                Console.WriteLine("Decrypted data:");

                text = System.Text.Encoding.ASCII.GetString(decrypted);

                Console.WriteLine(text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.ReadLine();

                return;
            }

            Array.Clear(crypted, 0, crypted.Length);
            Array.Clear(decrypted, 0, decrypted.Length);
            Array.Clear(key, 0, key.Length);
            Array.Clear(iv, 0, iv.Length);

            Console.WriteLine("Hashing...");
            Console.WriteLine("Input = " + decompressed_data.Length.ToString() + " bytes");

            byte[] hash;

            iv = new byte[] { };
            key = new byte[] { };

            try
            {
                quickBMS.QuickBMSDecryption("md5", ref key, ref iv, ref decompressed_data, out hash);

                file = new FileStream("test_hash.dat", FileMode.Create);
                bw = new BinaryWriter(file);

                bw.Write(hash);

                bw.Close();

                Console.WriteLine("Output = " + hash.Length.ToString() + " bytes");
                Console.WriteLine("Hash data:");

                text = System.Text.Encoding.ASCII.GetString(hash);

                Console.WriteLine(text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.ReadLine();

                return;
            }

            Array.Clear(hash, 0, hash.Length);


            Array.Clear(decompressed_data, 0, decompressed_data.Length);

            Console.ReadLine();

        }
    }
}
