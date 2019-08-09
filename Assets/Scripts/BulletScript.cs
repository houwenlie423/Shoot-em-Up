using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public Rigidbody2D m_Rb;
    public float m_TraverseSpeed = 20f;

    public Vector3 m_Direction;

    private void OnEnable() { m_Rb.velocity =  transform.up * m_TraverseSpeed; }

    private void OnTriggerEnter2D(Collider2D p_Col) {
        if (p_Col.tag == "Enemy") gameObject.SetActive(false);
    }
}
