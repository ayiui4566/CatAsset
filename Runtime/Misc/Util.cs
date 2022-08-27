﻿
using System.IO;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.Profiling;

namespace CatAsset.Runtime
{
    public class Util
    {
        /// <summary>
        /// 资源清单文件名
        /// </summary>
        public const string ManifestFileName = "CatAssetManifest.json";

        private const int oneKB = 1024;
        private const int oneMB = oneKB * 1024;
        private const int oneGB = oneMB * 1024;
        
        private static StringBuilder CachedSB = new StringBuilder();
        
        /// <summary>
        /// 获取在只读区下的完整路径
        /// </summary>
        public static string GetReadOnlyPath(string path)
        {
            string result = Path.Combine(Application.streamingAssetsPath, path);
            return result;
        }
        
        
        /// <summary>
        /// 获取在读写区下的完整路径
        /// </summary>
        public static string GetReadWritePath(string path)
        {
            string result = Path.Combine(Application.persistentDataPath, path);
            return result;
        }

        /// <summary>
        /// 获取在远端下的完整路径
        /// </summary>
        public static string GetRemotePath(string path)
        {
            string result = Path.Combine(CatAssetUpdater.UpdateUriPrefix, path);
            return result;
        }
        
        /// <summary>
        /// 根据字节长度获取合适的描述信息
        /// </summary>
        public static string GetByteLengthDesc(long length)
        {
            if (length > oneGB)
            {
                return (length / (oneGB * 1f)).ToString("0.00") + "G" ;
            }
            if (length > oneMB)
            {
                return (length / (oneMB * 1f)).ToString("0.00") + "M";
            }
            if (length > oneKB)
            {
                return (length / (oneKB * 1f)).ToString("0.00") + "K";
            }

            return length + "B";
        }

        /// <summary>
        /// 获取资源类别
        /// </summary>
        public static AssetCategory GetAssetCategory(string assetName)
        {
            if (assetName.StartsWith("Assets/"))
            {
                return AssetCategory.InternalUnityAsset;
            }

            if (assetName.StartsWith("raw:Assets/"))
            {
                return AssetCategory.InternalRawAsset;
            }

            return AssetCategory.ExternalRawAsset;
        }
        
        /// <summary>
        /// 获取内置原生资源的真实资源名
        /// </summary>
        public static string GetRealInternalRawAssetName(string assetName)
        {
            return assetName.Substring(4);
        }


        /// <summary>
        /// 获取文件MD5
        /// </summary>
        public static string GetFileMD5(string filePath)
        {
            Profiler.BeginSample("GetFileMD5");
            using (FileStream fs = new FileStream(filePath,FileMode.Open))
            {
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                   byte[] bytes = md5.ComputeHash(fs);
                   CachedSB.Clear();
                   foreach (byte b in bytes)
                   {
                       CachedSB.Append(b.ToString("x2"));
                   }
                   string result = CachedSB.ToString();
                   Profiler.EndSample();
                   return result;
                }
            }
          
        }
    }
}