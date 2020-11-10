using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace LSPFramework
{
    public class RSACryption
    {
        public static RSACryption Instance { get; } = new RSACryption();




        /// <summary>  
        /// RSA产生密钥  
        /// </summary>  
        /// <param name="xmlKeys">私钥</param>  
        /// <param name="xmlPublicKey">公钥</param>  
        public void RSAKey(out string xmlKeys, out string xmlPublicKey)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                xmlKeys = rsa.ToXmlString(true);
                xmlPublicKey = rsa.ToXmlString(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// RSA的加密函数  
        /// </summary>  
        /// <param name="xmlPublicKey">公钥</param>  
        /// <param name="encryptString">待加密的字符串</param>  
        /// <returns></returns>  
        public string RSAEncrypt(string xmlPublicKey, string encryptString)
        {
            try
            {
                if (string.IsNullOrEmpty(encryptString))
                {
                    return string.Empty;
                }

                if (string.IsNullOrWhiteSpace(xmlPublicKey))
                {
                    throw new ArgumentException("Invalid Public Key");
                }

                using (var rsaProvider = new RSACryptoServiceProvider())
                {
                    var inputBytes = Encoding.UTF8.GetBytes(encryptString);
                    rsaProvider.FromXmlString(xmlPublicKey);
                    int bufferSize = (rsaProvider.KeySize / 8) - 11;
                    var buffer = new byte[bufferSize];
                    using (MemoryStream inputStream = new MemoryStream(inputBytes),
                         outputStream = new MemoryStream())
                    {
                        while (true)
                        { //分段加密
                            int readSize = inputStream.Read(buffer, 0, bufferSize);
                            if (readSize <= 0)
                            {
                                break;
                            }

                            var temp = new byte[readSize];
                            Array.Copy(buffer, 0, temp, 0, readSize);
                            var encryptedBytes = rsaProvider.Encrypt(temp, false);
                            outputStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                        }
                        return Convert.ToBase64String(outputStream.ToArray());//转化为字节流方便传输
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>  
        /// RSA的解密函数  
        /// </summary>  
        /// <param name="xmlPrivateKey">私钥</param>  
        /// <param name="decryptString">待解密的字符串</param>  
        /// <returns></returns>  
        public string RSADecrypt(string xmlPrivateKey, string decryptString)
        {
            try
            {
                if (string.IsNullOrEmpty(decryptString))
                {
                    return string.Empty;
                }

                if (string.IsNullOrWhiteSpace(xmlPrivateKey))
                {
                    throw new ArgumentException("Invalid Private Key");
                }
                using (var rsaProvider = new RSACryptoServiceProvider())
                {
                    var inputBytes = Convert.FromBase64String(decryptString);
                    rsaProvider.FromXmlString(xmlPrivateKey);
                    int bufferSize = rsaProvider.KeySize / 8;
                    var buffer = new byte[bufferSize];
                    using (MemoryStream inputStream = new MemoryStream(inputBytes),
                         outputStream = new MemoryStream())
                    {
                        while (true)
                        {
                            int readSize = inputStream.Read(buffer, 0, bufferSize);
                            if (readSize <= 0)
                            {
                                break;
                            }
                            var temp = new byte[readSize];
                            Array.Copy(buffer, 0, temp, 0, readSize);
                            var rawBytes = rsaProvider.Decrypt(temp, false);
                            outputStream.Write(rawBytes, 0, rawBytes.Length);
                        }
                        return Encoding.UTF8.GetString(outputStream.ToArray());
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #region RSA数字签名  
        #region 获取Hash描述表          
        /// <summary>  
        /// 获取Hash描述表  
        /// </summary>  
        /// <param name="strSource">待签名的字符串</param>  
        /// <param name="HashData">Hash描述</param>  
        /// <returns></returns>  
        public bool GetHash(string strSource, ref byte[] HashData)
        {
            try
            {
                byte[] Buffer;
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(strSource);
                HashData = MD5.ComputeHash(Buffer);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// 获取Hash描述表  
        /// </summary>  
        /// <param name="strSource">待签名的字符串</param>  
        /// <param name="strHashData">Hash描述</param>  
        /// <returns></returns>  
        public bool GetHash(string strSource, ref string strHashData)
        {
            try
            {
                //从字符串中取得Hash描述   
                byte[] Buffer;
                byte[] HashData;
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(strSource);
                HashData = MD5.ComputeHash(Buffer);
                strHashData = Convert.ToBase64String(HashData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// 获取Hash描述表  
        /// </summary>  
        /// <param name="objFile">待签名的文件</param>  
        /// <param name="HashData">Hash描述</param>  
        /// <returns></returns>  
        public bool GetHash(System.IO.FileStream objFile, ref byte[] HashData)
        {
            try
            {
                //从文件中取得Hash描述   
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                HashData = MD5.ComputeHash(objFile);
                objFile.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// 获取Hash描述表  
        /// </summary>  
        /// <param name="objFile">待签名的文件</param>  
        /// <param name="strHashData">Hash描述</param>  
        /// <returns></returns>  
        public bool GetHash(System.IO.FileStream objFile, ref string strHashData)
        {
            try
            {
                //从文件中取得Hash描述   
                byte[] HashData;
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                HashData = MD5.ComputeHash(objFile);
                objFile.Close();
                strHashData = Convert.ToBase64String(HashData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA签名  
        /// <summary>  
        /// RSA签名  
        /// </summary>  
        /// <param name="strKeyPrivate">私钥</param>  
        /// <param name="HashbyteSignature">待签名Hash描述</param>  
        /// <param name="EncryptedSignatureData">签名后的结果</param>  
        /// <returns></returns>  
        public bool SignatureFormatter(string strKeyPrivate, byte[] HashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter

    (RSA);
                //设置签名的算法为MD5   
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名   
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// RSA签名  
        /// </summary>  
        /// <param name="strKeyPrivate">私钥</param>  
        /// <param name="HashbyteSignature">待签名Hash描述</param>  
        /// <param name="m_strEncryptedSignatureData">签名后的结果</param>  
        /// <returns></returns>  
        public bool SignatureFormatter(string strKeyPrivate, byte[] HashbyteSignature, ref string strEncryptedSignatureData)
        {
            try
            {
                byte[] EncryptedSignatureData;
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter

    (RSA);
                //设置签名的算法为MD5   
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名   
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// RSA签名  
        /// </summary>  
        /// <param name="strKeyPrivate">私钥</param>  
        /// <param name="strHashbyteSignature">待签名Hash描述</param>  
        /// <param name="EncryptedSignatureData">签名后的结果</param>  
        /// <returns></returns>  
        public bool SignatureFormatter(string strKeyPrivate, string strHashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            try
            {
                byte[] HashbyteSignature;

                HashbyteSignature = Convert.FromBase64String(strHashbyteSignature);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();


                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter

    (RSA);
                //设置签名的算法为MD5   
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名   
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// RSA签名  
        /// </summary>  
        /// <param name="strKeyPrivate">私钥</param>  
        /// <param name="strHashbyteSignature">待签名Hash描述</param>  
        /// <param name="strEncryptedSignatureData">签名后的结果</param>  
        /// <returns></returns>  
        public bool SignatureFormatter(string strKeyPrivate, string strHashbyteSignature, ref string strEncryptedSignatureData)
        {
            try
            {
                byte[] HashbyteSignature;
                byte[] EncryptedSignatureData;
                HashbyteSignature = Convert.FromBase64String(strHashbyteSignature);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter

    (RSA);
                //设置签名的算法为MD5   
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名   
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA 签名验证  
        /// <summary>  
        /// RSA签名验证  
        /// </summary>  
        /// <param name="strKeyPublic">公钥</param>  
        /// <param name="HashbyteDeformatter">Hash描述</param>  
        /// <param name="DeformatterData">签名后的结果</param>  
        /// <returns></returns>  
        public bool SignatureDeformatter(string strKeyPublic, byte[] HashbyteDeformatter, byte[] DeformatterData)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new

    System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5   
                RSADeformatter.SetHashAlgorithm("MD5");
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>  
        /// RSA签名验证  
        /// </summary>  
        /// <param name="strKeyPublic">公钥</param>  
        /// <param name="strHashbyteDeformatter">Hash描述</param>  
        /// <param name="DeformatterData">签名后的结果</param>  
        /// <returns></returns>  
        public bool SignatureDeformatter(string strKeyPublic, string strHashbyteDeformatter, byte[] DeformatterData)
        {
            try
            {
                byte[] HashbyteDeformatter;
                HashbyteDeformatter = Convert.FromBase64String(strHashbyteDeformatter);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new

    System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5   
                RSADeformatter.SetHashAlgorithm("MD5");
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>  
        /// RSA签名验证  
        /// </summary>  
        /// <param name="strKeyPublic">公钥</param>  
        /// <param name="HashbyteDeformatter">Hash描述</param>  
        /// <param name="strDeformatterData">签名后的结果</param>  
        /// <returns></returns>  
        public bool SignatureDeformatter(string strKeyPublic, byte[] HashbyteDeformatter, string strDeformatterData)
        {
            try
            {
                byte[] DeformatterData;
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new

    System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5   
                RSADeformatter.SetHashAlgorithm("MD5");
                DeformatterData = Convert.FromBase64String(strDeformatterData);
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>  
        /// RSA签名验证  
        /// </summary>  
        /// <param name="strKeyPublic">公钥</param>  
        /// <param name="strHashbyteDeformatter">Hash描述</param>  
        /// <param name="strDeformatterData">签名后的结果</param>  
        /// <returns></returns>  
        public bool SignatureDeformatter(string strKeyPublic, string strHashbyteDeformatter, string strDeformatterData)
        {
            try
            {
                byte[] DeformatterData;
                byte[] HashbyteDeformatter;
                HashbyteDeformatter = Convert.FromBase64String(strHashbyteDeformatter);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new

    System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5   
                RSADeformatter.SetHashAlgorithm("MD5");
                DeformatterData = Convert.FromBase64String(strDeformatterData);
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion
    }
}
