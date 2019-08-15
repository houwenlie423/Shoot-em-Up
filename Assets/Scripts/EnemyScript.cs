using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public float m_MaxHealth;
    public float m_CurrentHealth;

    public GameObject[] m_Drops;
    private int m_Rand;

    private void OnEnable() { m_CurrentHealth = m_MaxHealth;}

    private void OnTriggerEnter2D(Collider2D p_Col) {
        if (p_Col.gameObject.tag == "LaserBullet" || p_Col.gameObject.tag == "RegularBullet") {
            m_CurrentHealth -= p_Col.gameObject.GetComponent<BulletScript>().f_GetDamage();
            if (m_CurrentHealth <= 0) gameObject.SetActive(false);
        }
    }


    private void OnDisable() {
        //misalnya length nya = 10
        //jadi rand = Random.range (-11, 11) --> -11 - 10
        //karena 0 dihitung, jadi punya 50% chance buat keluar drop dan ga
        m_Rand = Random.Range(0, m_Drops.Length);
        //m_Rand = Random.Range(-m_Drops.Length-1, m_Drops.Length + 1);
        if (m_Rand >= 0) Master_Utility.Instance.f_ObjPool(m_Drops[m_Rand], transform.position, transform.rotation, false);
    }
}
