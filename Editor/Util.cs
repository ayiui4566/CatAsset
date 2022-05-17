﻿using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CatAsset.Editor
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// 要排除的文件后缀名集合
        /// </summary>
        public static readonly HashSet<string> ExcludeSet = new HashSet<string>();

        /// <summary>
        /// 默认资源组
        /// </summary>
        public const string DefaultGroup = "Base";
        
        /// <summary>
        /// 资源清单文件名
        /// </summary>
        public const string ManifestFileName = "CatAssetManifest.json";
        
        static Util()
        {
            ExcludeSet.Add(".meta");
            ExcludeSet.Add(".cs");
            ExcludeSet.Add(".asmdef");
            ExcludeSet.Add(".giparams");
        }


        [MenuItem("Assets/添加为资源包构建目录（可多选）", false)]
        private static void AddToBundleBuildDirectory()
        {
            BundleBuildConfigSO config = GetConfigAsset<BundleBuildConfigSO>();

            foreach (string guid in Selection.assetGUIDs)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                if (Directory.Exists(path) && config.CanAddDirectory(path))
                {
                    BundleBuildDirectory directory = new BundleBuildDirectory(path,nameof(NAssetToOneBundle),DefaultGroup);
                    config.Directories.Add(directory);
                }
            }
            config.Directories.Sort();
            EditorUtility.SetDirty(config);
            AssetDatabase.SaveAssets();
        }

        [MenuItem("Assets/添加为资源包构建目录（可多选）", true)]
        private static bool AddToBundleBuildDirectoryValidate()
        {
            foreach (string guid in Selection.assetGUIDs)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                if (Directory.Exists(path))
                {
                    return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// 获取资源包名
        /// </summary>
        public static string GetBundleName(string directory)
        {
            string bundleName = directory.Replace("Assets/","").Replace('/','_') + ".bundle";
            bundleName = bundleName.ToLower();
            return bundleName;
        }
        
        /// <summary>
        /// 获取SO配置
        /// </summary>
        public static T GetConfigAsset<T>() where T : ScriptableObject
        {

            string typeName = typeof(T).Name;
            string[] paths = AssetDatabase.FindAssets("t:" + typeName);
            if (paths.Length == 0)
            {
                Debug.LogError("不存在" + typeName);
                return null;
            }
            if (paths.Length > 1)
            {
                Debug.LogError(typeName + "数量大于1");
                return null;

            }
            string path = AssetDatabase.GUIDToAssetPath(paths[0]);
            T config = AssetDatabase.LoadAssetAtPath<T>(path);

            return config;
        }

        /// <summary>
        /// 将完整目录/文件名转换为Assets开头的目录/文件名
        /// </summary>
        public static string FullNameToAssetName(string fullName)
        {
            int assetsIndex = fullName.IndexOf("Assets\\");
            string assetsDir = fullName.Substring(assetsIndex).Replace('\\', '/');
            return assetsDir;
        }
        
        /// <summary>
        /// 获取排除了自身和csharp代码文件的依赖资源列表
        /// </summary>
        public static List<string> GetDependencies(string assetName,bool recursive = true)
        {
            List<string> result = new List<string>();
            
            string[] dependencies = AssetDatabase.GetDependencies(assetName,recursive);

            if (dependencies.Length == 0)
            {
                return result;
            }

        
            for (int i = 0; i < dependencies.Length; i++)
            {
                string dependencyName = dependencies[i];
                if (dependencyName == assetName || dependencyName.EndsWith(".cs"))
                {
                    continue;
                }

                result.Add(dependencyName);
            }

            return result;
        }

        /// <summary>
        /// 删除指定目录
        /// </summary>
        public static void DeleteDirectory(DirectoryInfo dirInfo)
        {
            //删除当前目录下的所有文件
            foreach (FileInfo fileInfo in dirInfo.GetFiles())
            {
                fileInfo.Delete();
            }

            //递归删除子目录
            foreach (DirectoryInfo childDirInfo in dirInfo.GetDirectories())
            {
                DeleteDirectory(childDirInfo);
            }
            
            //删除自身
            dirInfo.Delete();
        }

    }
}