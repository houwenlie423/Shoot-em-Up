using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyScript : MonoBehaviour {
    public float m_MaxHealth;
    public float m_CurrentHealth;

    public Image m_HealthBar;
    public TextMeshProUGUI m_HealthTxt;
    

    public GameObject[] m_Drops;
    private int m_Rand;

    private void OnEnable() { 
        m_CurrentHealth = m_MaxHealth;
        f_SetHPIndicator();
    }

    private void OnTriggerEnter2D(Collider2D p_Col) {
        if (p_Col.gameObject.tag == "LaserBullet" || p_Col.gameObject.tag == "RegularBullet") {
            f_ReceiveDmg(p_Col.gameObject.GetComponent<BulletScript>().f_GetDamage());
        }
    }

    private void OnDisable() {
        f_Drop();
    }

    private void f_Drop() {
        m_Rand = Random.Range(-m_Drops.Length, m_Drops.Length);
        if (m_Rand >= 0) Master_Utility.Instance.f_ObjPool(m_Drops[m_Rand], transform.position, transform.rotation, false);
    }

    private void f_SetHPIndicator() {
        m_HealthBar.fillAmount = m_CurrentHealth / m_MaxHealth;
        m_HealthTxt.text = m_CurrentHealth + " / " + m_MaxHealth;
    }


    private void f_ReceiveDmg(int p_Dmg) {
        m_CurrentHealth -= p_Dmg;
        if (m_CurrentHealth <= 0)   gameObject.SetActive(false);
        else                        f_SetHPIndicator();

    }
}
