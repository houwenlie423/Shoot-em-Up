using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletScript : MonoBehaviour {

    public Rigidbody2D m_Rb;
    public float m_TraverseSpeed = 20f;
    public float m_TimeToDisappear;
    public GameObject m_DamagePop;

    private int m_DamageBrought;
    private float t_TempTime;

    private void OnEnable() {
        t_TempTime = m_TimeToDisappear; 
        m_Rb.velocity =  transform.up * m_TraverseSpeed; 
    }

    private void OnTriggerEnter2D(Collider2D p_Col) {
        if (p_Col.tag == "Enemy") {
            Debug.Log("Damage in bullet : " + m_DamageBrought);
            Master_Utility.Instance.f_ObjPool(m_DamagePop, p_Col.transform.position, p_Col.transform.rotation, true).GetComponent<DmgPop>().f_SetDmg(m_DamageBrought);
            gameObject.SetActive(false);
        }


    }

    private void FixedUpdate() {
        t_TempTime -= Time.fixedDeltaTime;
        if (t_TempTime <= 0) {
            m_Rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }

    public void f_SetDamage(int p_Dmg) { m_DamageBrought = p_Dmg;  }
}
