using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletScript : MonoBehaviour {

    public Rigidbody2D m_Rb;
    public float m_TraverseSpeed = 20f;
    public float m_TimeToDisappear;

    private float t_TempTime;

    private void OnEnable() {
        t_TempTime = m_TimeToDisappear; 
        m_Rb.velocity =  transform.up * m_TraverseSpeed; 
    }

    private void OnTriggerEnter2D(Collider2D p_Col) {
        if (p_Col.tag == "Enemy") gameObject.SetActive(false);  

    }

    private void FixedUpdate() {
        t_TempTime -= Time.fixedDeltaTime;
        if (t_TempTime <= 0) gameObject.SetActive(false);
    }
}
