using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public CharacterController myController;
    public MeshFilter myMesh;
    private Vector3[] _origVerts;
    private Vector3[] _newVerts;
    private Matrix4x4 _m;
    void Start()
    {
        myMesh = GetComponent<MeshFilter>();
        _origVerts = myMesh.mesh.vertices;
        _newVerts = new Vector3[_origVerts.Length];
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var quat = Quaternion.CreateFromAxisAngle(transform.forward, 50f);

            // For each vertex...
            for (var i = 0; i < _origVerts.Length; i++)
            {
                // Apply the matrix to the vertex.
                _newVerts[i] = _m.MultiplyPoint3x4(_origVerts[i]);
            }

            // Copy the transformed vertices back to the mesh.
            myMesh.mesh.vertices = _newVerts;
            
        }
        
    }
}
