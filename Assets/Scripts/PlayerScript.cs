using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public static PlayerScript Instance;

    [Header("Fire Points")]
    public Transform m_PointTop;
    public Transform m_PointRight;
    public Transform m_PointBottom;
    public Transform m_PointLeft;

    [Header("Bullet Type")]
    public GameObject m_RegularPref;
    public GameObject m_LaserPref;

    [Header("Stats")]
    public float m_MovementSpeed;
    public int m_Damage;
    public int m_DamageThreshold;
    public float m_MaxHealth;
    public float m_CurrentHealth;

    [Header("Bullet Type")]
    public bool m_Laser;

    [Header("Power Ups")]
    public bool m_DiagonalShot;
    public bool m_RearShot;
    public bool m_SideShot;
    public bool m_DoubleDamage;


    private Vector3 m_Diff;
    private float m_RotZ;
    private Transform t_TempTransform;
    private GameObject m_Bullet;
    private int t_TempDamage;
    private float t_PercentageHP;

    private void Start() {
        t_TempDamage = m_Damage; 
        m_Bullet = m_RegularPref;
    }
    private void OnEnable() { Instance = this;}

    private void Update() {
        f_Move();
        f_Shoot();

        if (m_DoubleDamage) f_DoubleDamage();
        else                m_Damage = t_TempDamage;

    }

    public int f_CalculateDamage() { return m_Damage + Random.Range(-1 * m_DamageThreshold, m_DamageThreshold + 1);}

    private void f_Move() {
        transform.rotation = Master_Utility.Instance.f_LookAt2D(transform).rotation;
        if (Input.GetKey(KeyCode.Mouse0)) transform.Translate(Vector3.up * m_MovementSpeed * Time.deltaTime);

    }

    private void f_Shoot() {
        if(Input.GetKeyDown(KeyCode.Mouse1)) {
            if (m_Laser)            m_Bullet = m_LaserPref;
            else                    m_Bullet = m_RegularPref;

            if (m_DiagonalShot)     f_DiagonalShot();
            if (m_RearShot)         f_RearShot();
            if (m_SideShot)         f_SideShot();

            f_RegularShot();
        }
    }

    private void f_RegularShot() { Master_Utility.Instance.f_ObjPool(m_Bullet, m_PointTop.position, m_PointTop.rotation, false).GetComponent<BulletScript>().f_SetDamage(f_CalculateDamage()); }

    private void f_DiagonalShot() {
        m_PointTop.localRotation = Quaternion.Euler(0f, 0f, -45f);
        Master_Utility.Instance.f_ObjPool(m_Bullet, m_PointTop.position, m_PointTop.rotation, false).GetComponent<BulletScript>().f_SetDamage(f_CalculateDamage());
        m_PointTop.localRotation = Quaternion.Euler(0f, 0f, 45f);
        Master_Utility.Instance.f_ObjPool(m_Bullet, m_PointTop.position, m_PointTop.rotation, false).GetComponent<BulletScript>().f_SetDamage(f_CalculateDamage());

        m_PointTop.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void f_RearShot() { Master_Utility.Instance.f_ObjPool(m_Bullet, m_PointBottom.position, m_PointBottom.rotation, false).GetComponent<BulletScript>().f_SetDamage(f_CalculateDamage()); }

    private void f_SideShot() {
        Master_Utility.Instance.f_ObjPool(m_Bullet, m_PointLeft.position, m_PointLeft.rotation, false).GetComponent<BulletScript>().f_SetDamage(f_CalculateDamage());
        Master_Utility.Instance.f_ObjPool(m_Bullet, m_PointRight.position, m_PointRight.rotation, false).GetComponent<BulletScript>().f_SetDamage(f_CalculateDamage());
        
    }

    private void f_DoubleDamage() {
        t_TempDamage = m_Damage;
        m_Damage *= 2;
    }

    private void f_HPBoost(float p_Addition) {
        t_PercentageHP = m_CurrentHealth / m_MaxHealth;
        m_MaxHealth += p_Addition;
        m_CurrentHealth = t_PercentageHP * m_MaxHealth;
    }

    private void f_Heal(float p_Addition) {
        if (m_CurrentHealth + p_Addition <= m_MaxHealth)    m_CurrentHealth += p_Addition;
        else                                                m_CurrentHealth = m_MaxHealth;
    }

    private void f_DamageBoost(int p_Addition) { m_Damage += p_Addition; }

}
