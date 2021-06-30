using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public CharacterController myController;
    public MeshFilter myMesh;
    Vector3[] origVerts;
    Vector3[] newVerts;
    Matrix4x4 m;
    void Start()
    {
        myMesh = GetComponent<MeshFilter>();
        origVerts = myMesh.mesh.vertices;
        newVerts = new Vector3[origVerts.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            m = Quaternion.ToMatrix(Quaternion.CreateFromAxisAngle(ref transform.forward, 50f, out Quaternion result ), out m);
            Quaternion.ToMatrix()

            // For each vertex...
            for (int i = 0; i < origVerts.Length; i++)
            {
                // Apply the matrix to the vertex.
                newVerts[i] = m.MultiplyPoint3x4(origVerts[i]);
            }

            // Copy the transformed vertices back to the mesh.
            mf.mesh.vertices = newVerts;

            transform.rotation = Quaternion.ToMatrix(Quaternion.CreateFromAxisAngle(transform.forward, 50f));
        }
        
    }
}
