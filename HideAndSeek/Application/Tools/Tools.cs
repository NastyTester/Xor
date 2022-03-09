﻿using System.Text;

namespace Application
{
    public class Tools
    {
        private static string StringToBinary(string key)
        {
            StringBuilder binaryKey = new StringBuilder();

            foreach (char c in key.ToCharArray())
            {
                binaryKey.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return binaryKey.ToString();
        }

        public static bool IsKeyValid(string key)
        {
            foreach(char c in key.ToLower())
            {
                if (!"0123456789abcdef".Contains(c)) { return false; }
            }
            return true;
        }

        private static byte[] XORCipher(byte[] data, string binaryKey)
        {
            byte[] xor = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                xor[i] = (byte)(data[i] ^ binaryKey[i % data.Length]);
            }
            return xor;
        }

        public static void ProcesFile(IFormFile fileToEncrypt, string key)
        {
            using var fileStream = fileToEncrypt.OpenReadStream();
            byte[] bytes = new byte[fileToEncrypt.Length];
            fileStream.Read(bytes, 0, (int)fileToEncrypt.Length);

            string binaryKey = StringToBinary(key);
            string binaryFile = FileToBinary(fileToEncrypt);

            byte[] encryptedBytes = XORCipher(bytes,binaryKey);
            System.IO.File.WriteAllBytes("Foood.enc", encryptedBytes); // Requires System.IO

        }

        private static string FileToBinary(IFormFile fileToEncrypt)
        {
            using var fileStream = fileToEncrypt.OpenReadStream();
            byte[] bytes = new byte[fileToEncrypt.Length];

            return StringToBinary(bytes.ToString());
        }

        private static byte[] BinaryToFile(string encryptedBinary)
        {
            int numOfBytes = encryptedBinary.Length / 8;
            byte[] bytes = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; ++i)
            {
                bytes[i] = Convert.ToByte(encryptedBinary.Substring(8 * i, 8), 2);
            }
            return bytes;
        }
    }
}