using System.Collections.Generic;
using System;

using UnityEngine;

namespace VipereSolide.Subnautica.MessingAroundMod.Utility.Math
{
    public static class Mthv
    {
        public static Vector3 DirectionTowards(this Vector3 from, Vector3 to) => (to - from).normalized;
        public static float Distance(this Vector3 from, Vector3 to) => Vector3.Distance(from, to);
        public static Vector2[] GetPointsInCircle(this Vector2 center, float radius, int positionCount)
        {
            Vector2[] points = new Vector2[positionCount];
            float slice = 2 * Mathf.PI / positionCount;

            for (int i = 0; i < positionCount; i++)
            {
                float angle = slice * i;
                float x = center.x + radius * Mathf.Cos(angle);
                float y = center.y + radius * Mathf.Sin(angle);

                points[i] = new Vector2(x, y);
            }

            return points;
        }
        public static Vector3[] GetPointsInCircle3D(this Vector3 center, float radius, int positionCount)
        {
            Vector2[] points = GetPointsInCircle(new Vector2(center.x, center.z), radius, positionCount);
            Vector3[] points3D = new Vector3[positionCount];
            for (int i = 0; i < positionCount; i++) points3D[i] = new Vector3(points[i].x, center.y, points[i].y);

            return points3D;
        }
    }
}
