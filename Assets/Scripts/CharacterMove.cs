using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public CharacterController Controller;
    public double angle;
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
        var quat = new Quaternion();
        if (Input.GetKey(KeyCode.Z))
        {
            var TOLERANCE = 0.0001;
            if (Math.Abs(angle - 5) > TOLERANCE)
            {
                angle = 5;
            }
            Controller.Move(transform.forward * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            var TOLERANCE = 0.0001;
            if (Math.Abs(angle - 10) > TOLERANCE)
            {
                angle = 10;
            }
            
            Controller.Move(-transform.forward * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.Q))
        {
            angle -= 5f * Time.deltaTime;
            angle = Mathf.Clamp((float)angle, 3.5f, 6.5f);
            Controller.Move(-transform.right * Time.deltaTime);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            angle += 5f * Time.deltaTime;
            angle = Mathf.Clamp((float)angle, 3.5f, 6.5f);
            Controller.Move(transform.right * Time.deltaTime);
            
        }

        RotateFromAxis(transform.up);
        UpdateMesh();
        
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
