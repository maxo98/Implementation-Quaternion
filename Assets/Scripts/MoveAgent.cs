using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAgent : MonoBehaviour
{
    public double angle;
    public MeshFilter myMesh;
    private Vector3[] _origVerts;
    private Vector3[] _newVerts;
    private Matrix4x4 _m;
    public enum Direction { face, arriere, gauche, droite};
    public Direction myDirection = Direction.face;
    void Start()
    {
        //myMesh = GetComponent<MeshFilter>();
        _origVerts = myMesh.mesh.vertices;
        _newVerts = new Vector3[_origVerts.Length];
    }

    // Update is called once per frame
    void Update()
    {
        switch (myDirection)
        {
            case Direction.face:
                angle += 5f * Time.deltaTime;
                RotateFromAxis(transform.forward);
                UpdateMesh();
                break;
            case Direction.arriere:
                angle += 5f * Time.deltaTime;
                RotateFromAxis(-transform.forward);
                UpdateMesh();
                break;
            case Direction.gauche:
                angle += 5f * Time.deltaTime;
                RotateFromAxis(-transform.right);
                UpdateMesh();
                break;
            case Direction.droite:
                angle += 5f * Time.deltaTime;
                RotateFromAxis(transform.right);
                UpdateMesh();
                break;
            default:
                break;
        }
    }

    private void RotateFromAxis(Vector3 axis)
    {
        Quaternion.ToMatrix(Quaternion.CreateFromAxisAngle(axis, angle), out _m);
    }

    private void UpdateMesh()
    {
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
