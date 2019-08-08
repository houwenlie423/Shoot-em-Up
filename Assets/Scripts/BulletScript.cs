using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public Rigidbody2D m_Rb;
    public float m_TraverseSpeed = 20f;

    private void Start() { m_Rb.velocity = transform.right * m_TraverseSpeed;}
}
