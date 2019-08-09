using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float m_MovementSpeed;
    public Transform m_FirePoint;
    public GameObject m_BulletPrefab;

    private Vector3 m_Diff;
    private float m_RotZ;
    private Transform t_TempTransform;

    private void Update() {
        if(Input.GetKey(KeyCode.Mouse0))f_Move();
        if(Input.GetKeyDown(KeyCode.Mouse1)) f_Shoot();
    }

    private void f_Move() {
        transform.rotation = Master_Utility.Instance.f_LookAt2D(transform).rotation;
        transform.Translate(Vector3.up * m_MovementSpeed * Time.deltaTime);

    }

    private void f_Shoot() {
        //if (Input.GetKeyDown(KeyCode.Space)) Instantiate(m_BulletPrefab, m_FirePoint.position, m_FirePoint.rotation);
        Master_Utility.Instance.f_ObjPool(m_BulletPrefab, m_FirePoint.position, m_FirePoint.rotation);
    }


}
