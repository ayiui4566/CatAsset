﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatAsset.Runtime
{
    /// <summary>
    /// 资源清单信息
    /// </summary>
    public class AssetManifestInfo : IComparable<AssetManifestInfo>,IEquatable<AssetManifestInfo>
    {
        /// <summary>
        /// 资源名
        /// </summary>
        public string Name;

        /// <summary>
        /// 资源类型
        /// </summary>
        public Type Type;
        
        /// <summary>
        /// 依赖资源名列表
        /// </summary>
        public List<string> Dependencies;

        public int CompareTo(AssetManifestInfo other)
        {
            return Name.CompareTo(other.Name);
        }

        public bool Equals(AssetManifestInfo other)
        {
            return Name.Equals(other.Name);
        }

        public override string ToString()
        {
            return Name;
        }
    }

}
