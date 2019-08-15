using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour{

    public State_Manager.e_BoostType m_Type;
    public int m_Value;

    private void OnTriggerEnter2D(Collider2D p_Col) {
        if (p_Col.gameObject.tag == "Player") {
            PlayerScript.Instance.f_ReceiveBoost(m_Type, m_Value);
            gameObject.SetActive(false);
        }
    }
}
