using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace APPClasses
{
	public static class PasswordEncrypt
	{
		static public object Encrypt(byte[] data, string password)
		{
			SymmetricAlgorithm sa = null;
			try
			{
				sa = Rijndael.Create();
				ICryptoTransform ct = sa.CreateEncryptor(
					(new PasswordDeriveBytes(password, null)).GetBytes(16),
					new byte[16]);
				MemoryStream ms = new MemoryStream();
				CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
				cs.Write(data, 0, data.Length);
				cs.FlushFinalBlock();
				return ms.ToArray();
			}
			catch (Exception e)
			{
				return e;
			}
		}
		static public string Encrypt(string password, string data = "шифр")
		{
			object tmp = Encrypt(Encoding.UTF8.GetBytes(data), password);
			if (!(tmp is Exception))
			{
				return Convert.ToBase64String((byte[])tmp);
			}
			else return null;
		}
	}
}
