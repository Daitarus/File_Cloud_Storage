using System.Security.Cryptography;

namespace CryptL
{

    public sealed class CryptRSA : ICrypt
    {
        private RSACryptoServiceProvider rsa;

        private int allKeyStandardLength = 2324;
        private int publicKeyStandardLength = 532;

        public byte[] AllKey
        {
            get
            {
                return rsa.ExportCspBlob(true);
            }
            set
            {
                if (value == null || value.Length != allKeyStandardLength)
                    throw new Exception($"Array {nameof(AllKey)} must be {allKeyStandardLength} bytes");

                rsa.ImportCspBlob(value);
            }
        }
        public byte[] PublicKey
        {
            get
            {
                return rsa.ExportCspBlob(false);
            }
            set
            {
                if (value == null || value.Length != publicKeyStandardLength)
                    throw new Exception($"Array {nameof(PublicKey)} must be {publicKeyStandardLength} bytes");

                rsa.ImportCspBlob(value);
            }
        }

        public CryptRSA()
        {
            rsa = new RSACryptoServiceProvider(4096);
        }
        public CryptRSA(byte[] keys, bool ifAllKey)
        {
            if (keys == null || keys.Length == 0)
                throw new ArgumentNullException(nameof(keys));

            rsa = new RSACryptoServiceProvider(4096);
            if (ifAllKey)
            {
                AllKey = keys;
            }
            else
            {
                PublicKey = keys;
            }
        }

        public byte[] Encrypt(byte[] originalData)
        {
            if (originalData == null || originalData.Length == 0)
                throw new ArgumentNullException(nameof(originalData));

            return rsa.Encrypt(originalData, false);
        }
        public byte[] Decrypt(byte[] encryptData)
        {
            if (encryptData == null || encryptData.Length == 0)
                throw new ArgumentNullException(nameof(encryptData));

            return rsa.Decrypt(encryptData, false);
        }
    }

}
