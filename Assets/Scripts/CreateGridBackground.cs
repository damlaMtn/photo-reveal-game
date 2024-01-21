using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class CreateGridBackground : MonoBehaviour
{

    public float speed;
    public static float area = 0f;

    void Start()
    {
        GetComponent<MeshFilter>().mesh = CreatePlaneMesh();
    }

    Mesh CreatePlaneMesh()
    {
        Mesh mesh = new Mesh();
        float z = 0.05f;

        Vector3[] vertices = new Vector3[]
        {
        new Vector3( 1,  1, z),
        new Vector3( 1, -1, z),
        new Vector3(-1,  1, z),
        new Vector3(-1, -1, z),
        };

        Vector2[] uv = new Vector2[]
        {
        new Vector2(1, 1),
        new Vector2(1, 0),
        new Vector2(0, 1),
        new Vector2(0, 0),
        };

        int[] triangles = new int[]
        {
        0, 1, 2,
        2, 1, 3,
        };

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        //Calculate Area
        for (int p = 0; p < triangles.Length; p += 3)
        {
            area += (Vector3.Cross(vertices[triangles[p + 1]] - vertices[triangles[p]],
                        vertices[triangles[p + 2]] - vertices[triangles[p]])).magnitude;
        }
        area *= 0.5f;

        return mesh;
    }

    void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] normals = mesh.normals;
        Quaternion rotation = Quaternion.AngleAxis(Time.deltaTime * speed, Vector3.up);
        int i = 0;
        while (i < normals.Length)
        {
            normals[i] = rotation * normals[i];
            i++;
        }
        mesh.normals = normals;
    }
}
