using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform m_Target;
    public float m_SmoothSpeed = 0.125f;
    public Vector3 m_Offset;

    private void LateUpdate() {
        transform.position = Vector3.Lerp(transform.position, m_Target.position + m_Offset, m_SmoothSpeed);
        //transform.LookAt(m_Target);
    }
}
