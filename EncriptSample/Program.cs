using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace EncriptSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "abc";
            input = "银行密码统统都给我";
            string key = "justdoit";
            string result = string.Empty;
            result = Encrypter.EncryptByMD5(input);
            Console.WriteLine("MD5加密结果：{0}",result);

            result = Encrypter.EncryptBySHA1(input);
            Console.WriteLine("SHA1加密结果：{0}", result);

            result = Encrypter.EncryptString(input, key);
            Console.WriteLine("DES加密结果：{0}", result);


            result = Encrypter.DecryptString(result, key);
            Console.WriteLine("DES解密结果：{0}", result);

            result = Encrypter.EncryptByDES(input,key);
            Console.WriteLine("DES加密结果：{0}", result);
            

            result = Encrypter.DecryptByDES(result, key);
            Console.WriteLine("DES解密结果：{0}", result); //结果："银行密码统统都给我�\nJn7"，与明文不一致，为什么呢？在加密后，通过base64编码转为字符串，可能是这个问题。

            key = "111111111111111111111111111111111111111111111111111111111111111111111111111111111111111";

            result = Encrypter.EncryptByAES(input, key);
            Console.WriteLine("AES加密结果：{0}", result);

            result = Encrypter.DecryptByAES(result, key);
            Console.WriteLine("AES解密结果：{0}", result);


            KeyValuePair<string,string> keyPair = Encrypter.CreateRSAKey();
            string privateKey = keyPair.Value;
            string publicKey = keyPair.Key;

            result = Encrypter.EncryptByRSA(input, publicKey);
            Console.WriteLine("RSA私钥加密后的结果：{0}",result);

            result = Encrypter.DecryptByRSA(result, privateKey);
            Console.WriteLine("RSA公钥解密后的结果：{0}", result);

            //密钥加密，公钥解密
            result = Encrypter.EncryptByRSA(input, privateKey);
            Console.WriteLine("RSA私钥加密后的结果：{0}", result);

            result = Encrypter.DecryptByRSA(result, publicKey);
            Console.WriteLine("RSA公钥解密后的结果：{0}", result);

            TestSign();

            Console.WriteLine("输入任意键退出！");
            Console.ReadKey();
        }

        /// <summary>
        /// 测试数字签名
        /// </summary>
        public static void TestSign()
        {
            string originalData = "文章不错，这是我的签名：奥巴马！";
            Console.WriteLine("签名数为：{0}",originalData);
            KeyValuePair<string,string> keyPair = Encrypter.CreateRSAKey();
            string privateKey = keyPair.Value;
            string publicKey = keyPair.Key;

            //1、生成签名，通过摘要算法
            string signedData = Encrypter.HashAndSignString(originalData, privateKey);
            Console.WriteLine("数字签名:{0}",signedData);

            //2、验证签名
            bool verify = Encrypter.VerifySigned(originalData, signedData,publicKey);
            Console.WriteLine("签名验证结果：{0}",verify);
        }
    }
}
