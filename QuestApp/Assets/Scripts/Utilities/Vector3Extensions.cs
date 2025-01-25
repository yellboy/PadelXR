﻿using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public static class Vector3Extensions
    {
        public static Vector3 Flattened(this Vector3 vector)
        {
            return new Vector3(vector.x, 0, vector.z);
        }
    }

}
