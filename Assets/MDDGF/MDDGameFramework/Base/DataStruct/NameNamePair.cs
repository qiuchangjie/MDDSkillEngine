using System;
using System.Runtime.InteropServices;

namespace MDDGameFramework
{
    /// <summary>
    /// 名称和名称的组合值。
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    internal struct NameNamePair : IEquatable<NameNamePair>
    {
        private readonly string m_Name;
        private readonly string m_OwnerName;

        /// <summary>
        /// 初始化类型和名称的组合值的新实例。
        /// </summary>
        /// <param name="type">类型。</paramZ
        public NameNamePair(string Name)
            : this(Name, string.Empty)
        {
        }

        /// <summary>
        /// 初始化类型和名称的组合值的新实例。
        /// </summary>
        /// <param name="name">类型。</param>
        /// <param ownerName="ownerName">名称。</param>
        public NameNamePair(string name, string ownerName)
        {
            if (name == null)
            {
                throw new MDDGameFrameworkException("name is invalid.");
            }

            m_Name = name;
            m_OwnerName = ownerName ?? string.Empty;
        }

        /// <summary>
        /// 获取类型。
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        /// <summary>
        /// 获取名称。
        /// </summary>
        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
        }

        /// <summary>
        /// 获取名称和名称的组合值字符串。
        /// </summary>
        /// <returns>名称和名称的组合值字符串。</returns>
        public override string ToString()
        {
            if (m_Name == null)
            {
                throw new MDDGameFrameworkException("m_Name is invalid.");
            }
       
            return string.IsNullOrEmpty(m_OwnerName) ? m_Name : Utility.Text.Format("{0}.{1}", m_Name, m_OwnerName);
        }

        /// <summary>
        /// 获取对象的哈希值。
        /// </summary>
        /// <returns>对象的哈希值。</returns>
        public override int GetHashCode()
        {
            return m_Name.GetHashCode() ^ m_OwnerName.GetHashCode();
        }

        /// <summary>
        /// 比较对象是否与自身相等。
        /// </summary>
        /// <param name="obj">要比较的对象。</param>
        /// <returns>被比较的对象是否与自身相等。</returns>
        public override bool Equals(object obj)
        {
            return obj is TypeNamePair && Equals((TypeNamePair)obj);
        }

        /// <summary>
        /// 比较对象是否与自身相等。
        /// </summary>
        /// <param name="value">要比较的对象。</param>
        /// <returns>被比较的对象是否与自身相等。</returns>
        public bool Equals(NameNamePair value)
        {
            return m_Name == value.m_Name && m_OwnerName == value.m_OwnerName;
        }

        /// <summary>
        /// 判断两个对象是否相等。
        /// </summary>
        /// <param name="a">值 a。</param>
        /// <param name="b">值 b。</param>
        /// <returns>两个对象是否相等。</returns>
        public static bool operator ==(NameNamePair a, NameNamePair b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// 判断两个对象是否不相等。
        /// </summary>
        /// <param name="a">值 a。</param>
        /// <param name="b">值 b。</param>
        /// <returns>两个对象是否不相等。</returns>
        public static bool operator !=(NameNamePair a, NameNamePair b)
        {
            return !(a == b);
        }
    }
}
