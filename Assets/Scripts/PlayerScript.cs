using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour {

    //=========================================================================================================================================================================================================
    //                                                                                           VARIABLES
    //=========================================================================================================================================================================================================


    public static PlayerScript Instance;

    [Header("Components")]
    public Transform m_PointTop;
    public Transform m_PointRight;
    public Transform m_PointBottom;
    public Transform m_PointLeft;
    public Image m_HealthBar;
    public TextMeshProUGUI m_HealthTxt;
    

    [Header("Bullet Type")]
    public GameObject m_RegularPref;
    public GameObject m_LaserPref;

    [Header("Stats")]
    public float m_MovementSpeed;
    public int m_Damage;
    public int m_DamageThreshold;
    public float m_MaxHealth;
    public float m_CurrentHealth;


    [Header("Boosts")]
    public State_Manager.e_BulletType m_BulletType;
    public bool m_DiagonalShot;
    public bool m_RearShot;
    public bool m_SideShot;


    private Vector3 m_Diff;
    private float m_RotZ;
    private Transform t_TempTransform;
    private GameObject m_CurrentBullet;
    private float t_PercentageHP;


    //=========================================================================================================================================================================================================
    //                                                                                           MONOBEHAVIOR
    //=========================================================================================================================================================================================================


    private void OnEnable() { 
        Instance = this;
        m_CurrentBullet = m_RegularPref;
        m_CurrentHealth = m_MaxHealth;
        f_SetHPIndicator();
    }

    private void Update() {
        f_Move();
        f_Shoot();
    }

    //=========================================================================================================================================================================================================
    //                                                                                         PUBLIC FUNCTIONS
    //=========================================================================================================================================================================================================

    public void f_ReceiveBoost(State_Manager.e_BoostType p_Type, int p_Val) {
        switch(p_Type) {
            case State_Manager.e_BoostType.BonusDmg:
                f_DamageBoost(p_Val);
                break;

            case State_Manager.e_BoostType.DiagonalShot:
                m_DiagonalShot = true;
                break;

            case State_Manager.e_BoostType.Heal:
                f_Heal(p_Val);
                break;

            case State_Manager.e_BoostType.HPBoost:
                f_HPBoost(p_Val);
                break;

            case State_Manager.e_BoostType.Laser:
                f_ChangeBulletType(State_Manager.e_BulletType.Laser);
                break;

            case State_Manager.e_BoostType.RearShot:
                m_RearShot = true;
                break;

            case State_Manager.e_BoostType.SideShot:
                m_SideShot = true;
                break;

        }
    }

    public int f_CalculateDamage() { return m_Damage + Random.Range(-1 * m_DamageThreshold, m_DamageThreshold + 1); }

    //=========================================================================================================================================================================================================
    //                                                                                         PRIVATE FUNCTIONS
    //=========================================================================================================================================================================================================

    //============================================================================================
    //                                     BASICS                                               //
    //============================================================================================

    private void f_Move() {
        transform.rotation = Master_Utility.Instance.f_LookAt2D(transform).rotation;
        if (Input.GetKey(KeyCode.Mouse0)) transform.Translate(Vector3.up * m_MovementSpeed * Time.deltaTime);

    }

    private void f_Shoot() {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {

            if (m_DiagonalShot) f_DiagonalShot();
            if (m_RearShot) f_RearShot();
            if (m_SideShot) f_SideShot();

            f_RegularShot();
        }
    }

    private void f_SetHPIndicator() {
        m_HealthBar.fillAmount = m_CurrentHealth / m_MaxHealth;
        m_HealthTxt.text = m_CurrentHealth + " / " + m_MaxHealth;
    }


    private void f_ReceiveDmg(int p_Dmg) {
        m_CurrentHealth -= p_Dmg;
        if (m_CurrentHealth <= 0) gameObject.SetActive(false);
        else f_SetHPIndicator();

    }

    private void f_RegularShot() { Master_Utility.Instance.f_ObjPool(m_CurrentBullet, m_PointTop.position, m_PointTop.rotation, false).GetComponent<BulletScript>().f_SetDamage(f_CalculateDamage()); }


    //============================================================================================
    //                                   POWER UPS                                              //
    //============================================================================================

    private void f_ChangeBulletType(State_Manager.e_BulletType p_BulletType) {

        if (p_BulletType == State_Manager.e_BulletType.Laser) m_CurrentBullet = m_LaserPref;
        else if (p_BulletType == State_Manager.e_BulletType.Laser) m_CurrentBullet = m_RegularPref;
    }

    private void f_HPBoost(float p_Addition) {
        t_PercentageHP = m_CurrentHealth / m_MaxHealth;
        m_MaxHealth += p_Addition;
        m_CurrentHealth = t_PercentageHP * m_MaxHealth;

        f_SetHPIndicator();
    }

    private void f_Heal(float p_Addition) {
        if (m_CurrentHealth + p_Addition <= m_MaxHealth) m_CurrentHealth += p_Addition;
        else m_CurrentHealth = m_MaxHealth;

        f_SetHPIndicator();
    }

    private void f_DiagonalShot() {
        m_PointTop.localRotation = Quaternion.Euler(0f, 0f, -45f);
        Master_Utility.Instance.f_ObjPool(m_CurrentBullet, m_PointTop.position, m_PointTop.rotation, false).GetComponent<BulletScript>().f_SetDamage(f_CalculateDamage());
        m_PointTop.localRotation = Quaternion.Euler(0f, 0f, 45f);
        Master_Utility.Instance.f_ObjPool(m_CurrentBullet, m_PointTop.position, m_PointTop.rotation, false).GetComponent<BulletScript>().f_SetDamage(f_CalculateDamage());

        m_PointTop.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void f_SideShot() {
        Master_Utility.Instance.f_ObjPool(m_CurrentBullet, m_PointLeft.position, m_PointLeft.rotation, false).GetComponent<BulletScript>().f_SetDamage(f_CalculateDamage());
        Master_Utility.Instance.f_ObjPool(m_CurrentBullet, m_PointRight.position, m_PointRight.rotation, false).GetComponent<BulletScript>().f_SetDamage(f_CalculateDamage());

    }

    private void f_RearShot() { Master_Utility.Instance.f_ObjPool(m_CurrentBullet, m_PointBottom.position, m_PointBottom.rotation, false).GetComponent<BulletScript>().f_SetDamage(f_CalculateDamage()); }

    private void f_DamageBoost(int p_Addition) { m_Damage += p_Addition; }

}
