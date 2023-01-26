﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatAsset.Runtime
{
    /// <summary>
    /// 资源清单信息
    /// </summary>
    [Serializable]
    public class AssetManifestInfo : IComparable<AssetManifestInfo>,IEquatable<AssetManifestInfo>
    {
        /// <summary>
        /// 资源名
        /// </summary>
        [NonSerialized]
        public string Name;
        
        public int NameNodeID;
        
        /// <summary>
        /// 依赖资源名列表
        /// </summary>
        [NonSerialized]
        public List<string> Dependencies = new List<string>();
        
        public List<int> DependencyNodeIDs;

        /// <summary>
        /// 是否是图集散图
        /// </summary>
        public bool IsAtlasPackable;

        public int CompareTo(AssetManifestInfo other)
        {
            return Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(AssetManifestInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AssetManifestInfo)obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }

}
