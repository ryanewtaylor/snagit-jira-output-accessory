// Encrypt/Descrypt methods obtained through:
// http://www.codeproject.com/Articles/36449/String-Encryption-using-DPAPI-and-Extension-Method

namespace SnagitJiraOutputAccessory.Models
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class StringExtensions
    {
        public static String Enrypt(this String unencryptedString)
        {
            if (unencryptedString == null)
            {
                throw new ArgumentNullException("unencryptedString");
            }

            byte[] data = Encoding.Unicode.GetBytes(unencryptedString);
            byte[] encryptedData = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        public static String Decrypt(this String encryptedString)
        {
            if (encryptedString == null)
            {
                throw new ArgumentNullException("encryptedString");
            }

            byte[] data = Convert.FromBase64String(encryptedString);
            byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(decrypted);
        }
    }
}
