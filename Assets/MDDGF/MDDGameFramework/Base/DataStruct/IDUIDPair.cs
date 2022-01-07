using System;
using System.Runtime.InteropServices;

namespace MDDGameFramework
{
    /// <summary>
    /// ID和UID的组合值。
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public struct IDUIDPair : IEquatable<IDUIDPair>
    {
        private readonly int m_ID;
        private readonly int m_UID;
        private readonly string m_Layers;

        /// <summary>
        ///初始化IDUID的组合值的新实例。
        /// </summary>
        /// <param name="type">类型。</paramZ
        public IDUIDPair(int UID)
            : this(UID, 0, 1)
        {
        }

        /// <summary>
        ///初始化IDUID的组合值的新实例。
        /// </summary>
        /// <param name="type">类型。</paramZ
        public IDUIDPair(int UID, int Layer)
            : this(UID, 0, Layer)
        {
        }

        /// <summary>
        /// 初始化IDUID的组合值的新实例。
        /// </summary>
        /// <param UID="UID">唯一ID。</param>
        /// <param ID="ID">ID。</param>
        public IDUIDPair(int UID, int ID, int Layer)
        {
            if (UID == 0)
            {
                throw new MDDGameFrameworkException("UID is invalid.");
            }

            m_UID = UID;
            m_ID = ID;
            m_Layers = Layer.ToString();
        }

        /// <summary>
        /// 获取UID
        /// </summary>
        public int UID
        {
            get
            {
                return m_UID;
            }
        }

        /// <summary>
        /// 获取ID
        /// </summary>
        public int ID
        {
            get
            {
                return m_ID;
            }
        }

        /// <summary>
        /// 获取层数
        /// </summary>
        public string Layer
        {
            get
            {
                return m_Layers;
            }
        }

        /// <summary>
        /// 获取IDUID组合值字符串。
        /// </summary>
        /// <returns>IDUID的组合值字符串。</returns>
        public override string ToString()
        {
            if (UID == 0)
            {
                throw new MDDGameFrameworkException("UID is invalid.");
            }

            return Utility.Text.Format("{0}.{1}", m_ID, m_UID);
        }

        /// <summary>
        /// 获取对象的哈希值。
        /// </summary>
        /// <returns>对象的哈希值。</returns>
        public override int GetHashCode()
        {
            return m_Layers.GetHashCode();
        }

        /// <summary>
        /// 比较对象是否与自身相等。
        /// </summary>
        /// <param name="obj">要比较的对象。</param>
        /// <returns>被比较的对象是否与自身相等。</returns>
        public override bool Equals(object obj)
        {
            return obj is IDUIDPair && Equals((IDUIDPair)obj);
        }

        /// <summary>
        /// 比较对象是否与自身相等。
        /// </summary>
        /// <param name="value">要比较的对象。</param>
        /// <returns>被比较的对象是否与自身相等。</returns>
        public bool Equals(IDUIDPair value)
        {
            return m_ID == value.ID && m_UID == value.UID && m_Layers == value.Layer;
        }

        /// <summary>
        /// 判断两个对象是否相等。
        /// </summary>
        /// <param name="a">值 a。</param>
        /// <param name="b">值 b。</param>
        /// <returns>两个对象是否相等。</returns>
        public static bool operator ==(IDUIDPair a, IDUIDPair b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// 判断两个对象是否不相等。
        /// </summary>
        /// <param name="a">值 a。</param>
        /// <param name="b">值 b。</param>
        /// <returns>两个对象是否不相等。</returns>
        public static bool operator !=(IDUIDPair a, IDUIDPair b)
        {
            return !(a == b);
        }
    }
}
