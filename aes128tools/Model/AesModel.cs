using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using aes128tools.Annotations;
using aes128tools.Converters;
using Microsoft.Build.Framework;

namespace aes128tools.Model
{
    class AesModel : IDataErrorInfo,INotifyPropertyChanged
    {
         private byte[] _iv;
         private byte[] _plainData;
         private byte[] _encrypData;
         private byte[] _key;
       


        public byte[] Key
        {
            set
            {
                _key = value;
                OnPropertyChanged();
            }
            get { return _key; }
        }

        public byte[] IV
        {
            set
            {
               _iv = value;
                OnPropertyChanged();
            }
            get { return _iv; }
        }
        
        public byte[] PlainData {
            set
            {
               _plainData = value;
                OnPropertyChanged();
            }
            get { return _plainData; }
        }

        public byte[] EncrypData
        {
            set
            {
                _encrypData = value;
                OnPropertyChanged();
            }
            get { return _encrypData; }
        }




       #region AESDecode Methods

        public  static byte[] Encrytp(byte[] Key, byte[] IV, byte[] plainData)
        {
            byte[] encrypted;
           
            using (Aes aesAlg = Aes.Create())
            {

                aesAlg.Padding = PaddingMode.None;
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        
                            
                            csEncrypt.Write(plainData,0,plainData.Length);
                            csEncrypt.FlushFinalBlock();
                            encrypted = msEncrypt.ToArray();
                        
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        public  static byte[] Decrypt(byte[] Key, byte[] IV, byte[] encrypData)
        {
            
    
            byte[] plain = null;

            
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Padding = PaddingMode.None;
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
               ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream())
                {   
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                    {
                        csDecrypt.Write(encrypData,0,encrypData.Length);
                        csDecrypt.FlushFinalBlock();
                        plain = msDecrypt.ToArray();
                    }
                }

            }


            return plain;
        }

        #endregion */

        #region INotifyPropertyChanged Memebers
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error => null;
        string IDataErrorInfo.this[string propertyName] => ValidationError(propertyName);

        #endregion

        #region Validation


        public bool IsValidToEn
        {
            get
            {

                foreach (var property in ValidatePropertiesToEn)
                {
                    if (ValidationError(property) != null)
                        return false;
                }

                return true;
            }

        }
        public bool IsValidToDe
        {
            get
            {

                foreach (var property in ValidatePropertiesToDe)
                {
                    if (ValidationError(property) != null)
                        return false;
                }

                return true;
            }

        }

        static readonly string[] ValidatePropertiesToEn =
        {
            "Key",
            "IV",
            "PlainData"
        };

        static readonly string[] ValidatePropertiesToDe =
        {
            "Key",
            "IV",
            "EncrypData"
        };
        private string ValidationError(string propertyName)
        {
            string error = null;
            switch (propertyName)
            {
                case "Key":
                    error = ValidateKey();
                    break;
                case "IV":
                    error = ValidateIV();
                    break;
                case "PlainData":
                    error = ValidatePlainData();
                    break;
                case "EncrypData":
                    error = ValidateEncrpData();
                    break;
            }

            return error;
        }

        private string ValidateEncrpData()
        {
            if (EncrypData.Length == 0)
            {
                return "no empty to decryption process";
            }

            return null;
        }

        private string ValidatePlainData()
        {   



            if (PlainData.Length == 0)
            {
                return "no empty to encryption process";
            }

            
            
            return null;
        }

        private string ValidateIV()
        {

            if (IV.Length == 16)
            {

                return null;
            }

            return "must be 16 byte";
          
            
        }

        private string ValidateKey()
        {
            if (Key.Length == 16 || Key.Length == 24 || Key.Length == 32)
            {
               
                return null;
            }

            return "must be 16 byte"; 
        }

        #endregion



    }
}
