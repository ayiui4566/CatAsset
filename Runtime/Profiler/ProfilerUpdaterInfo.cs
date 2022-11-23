﻿using System;

namespace CatAsset.Runtime
{
    /// <summary>
    /// 分析器更新器信息
    /// </summary>
    [Serializable]
    public class ProfilerUpdaterInfo : IReference,IComparable<ProfilerUpdaterInfo>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 资源包总数
        /// </summary>
        public int TotalCount;

        /// <summary>
        /// 资源包总长度
        /// </summary>
        public ulong TotalLength;

        /// <summary>
        /// 已更新资源包总数
        /// </summary>
        public int UpdatedCount;

        /// <summary>
        /// 已更新资源包总长度
        /// </summary>
        public ulong UpdatedLength;

        /// <summary>
        /// 下载速度
        /// </summary>
        public ulong Speed;

        /// <summary>
        /// 状态
        /// </summary>
        public GroupUpdaterState State;

        public static ProfilerUpdaterInfo Create(string name, int totalCount, ulong totalLength, int updatedCount, ulong updatedLength, ulong speed, GroupUpdaterState state)
        {
            ProfilerUpdaterInfo info = ReferencePool.Get<ProfilerUpdaterInfo>();
            info.Name = name;
            info.TotalCount = totalCount;
            info.TotalLength = totalLength;
            info.UpdatedCount = updatedCount;
            info.UpdatedLength = updatedLength;
            info.Speed = speed;
            info.State = state;
            return info;
        }

        public void Clear()
        {
            Name = default;
            TotalCount = default;
            TotalLength = default;
            UpdatedCount = default;
            UpdatedLength = default;
            Speed = default;
            State = default;
        }

        public int CompareTo(ProfilerUpdaterInfo other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
