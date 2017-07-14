﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace g3
{

    public interface IBinaryVoxelGrid
    {
        AxisAlignedBox3i GridBounds { get; }   // bounds are lower-inclusive, upper-exclusive
        bool Get(Vector3i i);
        IEnumerable<Vector3i> NonZeros();
    }



    public class Bitmap3d : IBinaryVoxelGrid
    {
        public BitArray Bits;
        public Vector3i Dimensions;

        int row_size, slab_size;

        public Bitmap3d(Vector3i dims)
        {
            int size = dims.x * dims.y * dims.z;
            Bits = new BitArray(size);

            Dimensions = dims;
            row_size = dims.x;
            slab_size = dims.x * dims.y;
        }


        public AxisAlignedBox3i GridBounds {
            get { return new AxisAlignedBox3i(Vector3i.Zero, Dimensions); }
        }

        public bool this[int i]
        {
            get { return Bits[i]; }
            set { Bits[i] = value; }
        }


        public bool this[Vector3i idx]
        {
            get {
                int i = idx.z * slab_size + idx.y * row_size + idx.x;
                return Bits[i];
            }
            set {
                int i = idx.z * slab_size + idx.y * row_size + idx.x;
                Bits[i] = value;
            }
        }

        public void Set(Vector3i idx, bool val)
        {
            int i = idx.z * slab_size + idx.y * row_size + idx.x;
            Bits[i] = val;
        }

        public bool Get(Vector3i idx)
        {
            int i = idx.z * slab_size + idx.y * row_size + idx.x;
            return Bits[i];
        }


        public Vector3i ToIndex(int i) {
            int c = i / slab_size;
            i -= c * slab_size;
            int b = i / row_size;
            i -= b * row_size;
            return new Vector3i(i, b, c);
        }
        public int ToLinear(Vector3i idx) {
            return idx.z * slab_size + idx.y * row_size + idx.x;
        }



        public IEnumerable<Vector3i> Indices()
        {
            for ( int z = 0; z < Dimensions.z; ++z) {
                for ( int y = 0; y < Dimensions.y; ++y ) {
                    for (int x = 0; x < Dimensions.x; ++x)
                        yield return new Vector3i(x, y, z);
                }
            }
        }


        public IEnumerable<Vector3i> NonZeros()
        {
            for ( int i = 0; i < Bits.Count; ++i ) {
                if (Bits[i])
                    yield return ToIndex(i);
            }
        }



    }
}
