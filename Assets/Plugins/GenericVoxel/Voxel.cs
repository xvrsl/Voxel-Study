using System;
using System.Collections.Generic;
using UnityEngine;
namespace XVRSL.Voxels
{
    [System.Serializable]
    public class Voxel<T>
    {
        [SerializeField]
        Vector3Int size;

        Vector3Int cachedSize = default;
        int layerCount = default;
        int count = default;

        public Vector3Int Size => size;
        bool Dirty
        {
            get => cachedSize != size;
        }
        public int LayerCount
        {
            get
            {
                RefreshIfNeeded();
                return layerCount;
            }
        }
        public int Count
        {
            get
            {
                RefreshIfNeeded();
                return count;
            }
        }

        private void RefreshIfNeeded()
        {
            if (!Dirty) return;
            cachedSize = size;
            layerCount = size.x * size.z;
            count = layerCount * size.y;
        }


        [SerializeField]
        List<T> data;

        public T this[int index]
        {
            get
            {
                if (index < 0) return default;
                if (index >= data.Count) return default;
                return data[index];
            }
            set
            {
                if (index >= data.Count)
                {
                    throw new System.IndexOutOfRangeException();
                }
                if (data.Count < index)
                {
                    ExpandTo(index);
                }
                data[index] = value;
            }
        }
        public T this[Vector3Int coord]
        {
            get
            {
                int index = CoordToIndex(coord);
                return this[index];
            }
            set
            {
                int index = CoordToIndex(coord);
                this[index] = value;
            }
        }

        void ExpandTo(int index)
        {
            while (data.Count <= index)
            {
                data.Add(default);
            }
        }

        public int CoordToIndex(Vector3Int coord)
        {
            return coord.x + coord.y * layerCount + coord.z * size.x;
        }
        public Vector3Int IndexToCoord(int index)
        {
            int y = index / layerCount;
            int z = index % layerCount / size.x;
            int x = index % layerCount % size.x;
            return new Vector3Int(x, y, z);
        }

        public Voxel(Vector3Int size)
        {
            this.size = size;
            data = new List<T>();
        }
    }
}
