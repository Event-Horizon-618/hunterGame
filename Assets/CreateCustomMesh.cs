using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCustomMesh : MonoBehaviour
{
    void Start()
    {
        // Define the vertices of the mesh
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(1, 1, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 1),
            new Vector3(1, 1, 1),
            new Vector3(0, 1, 1)
        };

        // Define the triangles of the mesh
        int[] triangles = new int[]
        {
            0, 2, 1, // front
            0, 3, 2,
            1, 2, 6, // top
            1, 6, 5,
            0, 7, 3, // bottom
            0, 4, 7,
            2, 3, 6, // back
            3, 7, 6,
            1, 5, 4, // right
            1, 4, 0,
            5, 6, 7, // left
            5, 7, 4
        };

        // Define the normals of the mesh
        Vector3[] normals = new Vector3[]
        {
            Vector3.back,
            Vector3.back,
            Vector3.up,
            Vector3.up,
            Vector3.forward,
            Vector3.forward,
            Vector3.right,
            Vector3.right
        };

        // Create a new Mesh object and assign its properties
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;

        // Create a new GameObject and add a MeshFilter and MeshRenderer component to it
        GameObject obj = new GameObject("CustomMesh");
        obj.AddComponent<MeshFilter>().mesh = mesh;
        obj.AddComponent<MeshRenderer>().material.color = Color.green;
    }
}
