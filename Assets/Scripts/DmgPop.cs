using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DmgPop : MonoBehaviour {
    public TextMeshProUGUI m_DmgText;
    public int m_Damage;


    public void f_Disactive() { gameObject.SetActive(false); }

    public void f_SetDmg(int p_Damage) { 
        m_Damage = p_Damage;
        m_DmgText.text = "" + m_Damage;
    }
}
