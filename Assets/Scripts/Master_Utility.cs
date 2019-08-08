using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master_Utility : MonoBehaviour {
    static Vector3 m_Diff;
    static float m_RotZ;
    static Transform t_Transform;

    public static Transform f_LookAt2D(Transform p_YourTransform) {
        t_Transform = p_YourTransform;
        m_Diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - t_Transform.position;
        if (Mathf.Abs(m_Diff.x) > 0.5f && Mathf.Abs(m_Diff.y) > 0.5f) {
            m_Diff.Normalize();
            m_RotZ = Mathf.Atan2(m_Diff.y, m_Diff.x) * Mathf.Rad2Deg;
            t_Transform.rotation = Quaternion.Euler(0f, 0f, m_RotZ - 90);
        }
        return t_Transform;
    }
}
