using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;
using System.Text;

public static class StringExtension
{
    private static byte[] Key => FrameworkGlobals.ENCRYPTION_KEY;

    public static string Encrypt(this string ctx) { 
        SymmetricAlgorithm algo = Aes.Create();
        algo.GenerateIV();
        byte[] IV = algo.IV;
        DevUtils.Log($"{Encoding.Unicode.GetString(IV)}");
        ICryptoTransform transform = algo.CreateEncryptor(Key, IV);
        byte[] ctxBuffer = Encoding.Unicode.GetBytes(ctx);
        byte[] inputBuffer = new byte[ctxBuffer.Length + 16];
        IV.CopyTo(inputBuffer, 0);
        ctxBuffer.CopyTo(inputBuffer, 16);
        byte[] outputBuffer = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
        return Convert.ToBase64String(outputBuffer);
    }

    public static string Decrypt(this string ctx) {
        SymmetricAlgorithm algo = Aes.Create();
        byte[] inputBuffer = Convert.FromBase64String(ctx);
        byte[] IV = inputBuffer[0..16];
        ICryptoTransform transform = algo.CreateDecryptor(Key, IV);
        byte[] outputBuffer = transform.TransformFinalBlock(inputBuffer, 16, inputBuffer.Length - 16);
        return Encoding.Unicode.GetString(outputBuffer);
    }
}
