using UnityEngine;

namespace DJM.CoreTools.Utilities
{
    public static class MeshUtils
    {
        public static Mesh GenerateQuadMesh(Rect area, Vector3 positionOffset = default)
        {
            var mesh = new Mesh();
            var vertices = new Vector3[4];
            var triangles = new int[6];
            var normals = new Vector3[4];
            var uv = new Vector2[4];

            vertices[0] = new Vector3(area.xMin, 0, area.yMin) + positionOffset;
            vertices[1] = new Vector3(area.xMin, 0, area.yMax) + positionOffset;
            vertices[2] = new Vector3(area.xMax, 0, area.yMax) + positionOffset;
            vertices[3] = new Vector3(area.xMax, 0, area.yMin) + positionOffset;

            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            
            triangles[3] = 0;
            triangles[4] = 2;
            triangles[5] = 3;

            normals[0] = Vector3.up;
            normals[1] = Vector3.up;
            normals[2] = Vector3.up;
            normals[3] = Vector3.up;

            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(0, 1);
            uv[2] = new Vector2(1, 1);
            uv[3] = new Vector2(1, 0);

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.normals = normals;
            mesh.uv = uv;

            return mesh;
        }
    }
}