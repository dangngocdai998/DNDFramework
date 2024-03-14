using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class AesEncode
{
    const string keyHex = "33962e58b9265274ab113023286380d8cc19b3abbcea5b356498b01784c2383e";
    const string IVHex = "8bd06a054eb525f46b5da76fdca5406f";



    public static Keys CreateKey()
    {
        Keys keys = new Keys();
        using (Aes aesAlg = Aes.Create())
        {
            keys.key = aesAlg.Key;
            keys.IV = aesAlg.IV;
        }

        return keys;
    }

    public static string EncodeAes(string plainText, Keys _keys)
    {
        /* Debug.Log("PlainText: " + plainText);
        Debug.Log("Descr: " + DecryptStringFromBytes_Aes(encrypted, HexToByteArray(keyHex), HexToByteArray(IVHex)));
        string encode = BytesToString(encrypted); */
        /* Debug.Log("Descr2: " + DecryptStringFromBytes_Aes(HexToByteArray(encode), HexToByteArray(keyHex), HexToByteArray(IVHex))); */
        // Debug.Log("Keys: " + BytesToString(_keys.key) + " / " + BytesToString(_keys.IV));

        return BytesToString(EncryptStringToBytes_Aes(plainText, _keys.key, _keys.IV));
    }



    static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");
        byte[] encrypted;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }

    static byte[] HexToByteArray(String hexString)
    {
        byte[] retval = new byte[hexString.Length / 2];
        for (int i = 0; i < hexString.Length; i += 2)
            retval[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
        return retval;
    }

    public static string BytesToString(byte[] value)
    {
        return String.Concat(Array.ConvertAll(value, x => x.ToString("X2")));
    }
    static string BytesToBase64(byte[] value)
    {
        return Convert.ToBase64String(value);
    }
    const String pempubheader = "-----BEGIN PUBLIC KEY-----";
    const String pempubfooter = "-----END PUBLIC KEY-----";


    // static bool verbose = false;

    static RSA GetRSAProviderFromPemFile()
    {
        // bool isPrivateKeyFile = true;
        // string pemstr = File.ReadAllText(@"Assets/AmetaGameProject/Resources/Server/server_publickey.pem").Trim();
        TextAsset file = Resources.Load<TextAsset>("Server/server_publickey");
        if (file == null)
            return null;
        string pemstr = file.text.Trim();
        Debug.Log("pemstring: " + pemstr);
        // if (pemstr.StartsWith(pempubheader) && pemstr.EndsWith(pempubfooter))
        //     isPrivateKeyFile = false;
        byte[] pemkey;

        pemkey = DecodeOpenSSLPublicKey(pemstr);

        if (pemkey == null)
            return null;
        return DecodeX509PublicKey(pemkey);

    }
    static byte[] DecodeOpenSSLPublicKey(String instr)
    {
        const String pempubheader = "-----BEGIN PUBLIC KEY-----";
        const String pempubfooter = "-----END PUBLIC KEY-----";
        String pemstr = instr.Trim();
        byte[] binkey;
        if (!pemstr.StartsWith(pempubheader) || !pemstr.EndsWith(pempubfooter))
            return null;
        StringBuilder sb = new StringBuilder(pemstr);
        sb.Replace(pempubheader, "");  //remove headers/footers, if present
        sb.Replace(pempubfooter, "");

        String pubstr = sb.ToString().Trim();   //get string after removing leading/trailing whitespace

        try
        {
            binkey = Convert.FromBase64String(pubstr);
        }
        catch (System.FormatException)
        {       //if can't b64 decode, data is not valid
            return null;
        }
        return binkey;
    }
    static RSA DecodeX509PublicKey(byte[] x509Key)
    {
        // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
        byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
        // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
        using (var mem = new MemoryStream(x509Key))
        {
            using (var binr = new BinaryReader(mem))    //wrap Memory Stream with BinaryReader for easy reading
            {
                try
                {
                    var twobytes = binr.ReadUInt16();
                    switch (twobytes)
                    {
                        case 0x8130:
                            binr.ReadByte();    //advance 1 byte
                            break;
                        case 0x8230:
                            binr.ReadInt16();   //advance 2 bytes
                            break;
                        default:
                            return null;
                    }

                    var seq = binr.ReadBytes(15);
                    if (!CompareBytearrays(seq, seqOid))  //make sure Sequence for OID is correct
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8203)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    var bt = binr.ReadByte();
                    if (bt != 0x00)     //expect null byte next
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    twobytes = binr.ReadUInt16();
                    byte lowbyte = 0x00;
                    byte highbyte = 0x00;

                    if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                        lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                    else if (twobytes == 0x8202)
                    {
                        highbyte = binr.ReadByte(); //advance 2 bytes
                        lowbyte = binr.ReadByte();
                    }
                    else
                        return null;
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                    int modsize = BitConverter.ToInt32(modint, 0);

                    byte firstbyte = binr.ReadByte();
                    binr.BaseStream.Seek(-1, SeekOrigin.Current);

                    if (firstbyte == 0x00)
                    {   //if first byte (highest order) of modulus is zero, don't include it
                        binr.ReadByte();    //skip this null byte
                        modsize -= 1;   //reduce modulus buffer size by 1
                    }

                    byte[] modulus = binr.ReadBytes(modsize); //read the modulus bytes

                    if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                        return null;
                    int expbytes = binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                    byte[] exponent = binr.ReadBytes(expbytes);

                    // We don't really need to print anything but if we insist to...
                    //showBytes("\nExponent", exponent);
                    //showBytes("\nModulus", modulus);

                    // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                    RSA rsa = RSA.Create();

                    RSAParameters rsaKeyInfo = new RSAParameters
                    {
                        Modulus = modulus,
                        Exponent = exponent
                    };
                    rsa.ImportParameters(rsaKeyInfo);
                    return rsa;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }

    static bool CompareBytearrays(byte[] a, byte[] b)
    {
        if (a.Length != b.Length)
            return false;
        int i = 0;
        foreach (byte c in a)
        {
            if (c != b[i])
                return false;
            i++;
        }
        return true;
    }
    static RSA RSAalg;
    public static string EncryptBase64(byte[] data)
    {
        try
        {
            if (RSAalg == null)
                RSAalg = GetRSAProviderFromPemFile();
            Debug.Log($"A new key pair of legth {RSAalg.KeySize} was created");
            return BytesToBase64(RSAEncrypt(data));

        }
        catch (ArgumentNullException)
        {
            Debug.LogError("Encryption failed.");
            return null;
        }
    }

    static byte[] RSAEncrypt(byte[] DataToEncrypt)
    {
        try
        {
            // RSAalg rSACng;
            return RSAalg.Encrypt(DataToEncrypt, RSAEncryptionPadding.OaepSHA1);
        }
        //Catch and display a CryptographicException
        //to the console.
        catch (CryptographicException e)
        {
            Debug.LogError(e.Message);

            return null;
        }
    }
}
[Serializable]
public class Keys
{
    public byte[] key;
    public byte[] IV;
}
