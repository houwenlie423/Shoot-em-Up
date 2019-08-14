using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Movement : MonoBehaviour {
    public State_Manager.e_TYPE m_Type;
    public State_Manager.e_STATE m_State;
    public float m_MaxHealth;
    public float m_CurrHealth;
    public float m_Damage;
    public float m_MovementSpeed;
    public LayerMask m_Layer;
    Vector3 t_Position;
    Vector3 m_Dir;
    Transform m_Target;
    Collider2D[] m_Collider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        f_CheckBehaviour();
    }

    void f_CheckBehaviour() {
        if(m_State == State_Manager.e_STATE.TARGETING) {
            if(m_Type == State_Manager.e_TYPE.RANGE) {
                m_Collider = Physics2D.OverlapCircleAll(transform.position, 0.1f, m_Layer);
                for(int i = 0; i < m_Collider.Length; i++) {
                    m_State = State_Manager.e_STATE.ATTACK;
                }
;            }
        }

        if (m_State == State_Manager.e_STATE.ATTACK) {

        }

        if (m_State == State_Manager.e_STATE.TARGETING) {
            f_Move();
        }
    }

    void f_Move() {
        m_Dir = m_Target.position - transform.position;
        t_Position = Vector3.MoveTowards(transform.position, m_Target.position, m_MovementSpeed * Time.fixedDeltaTime);
        transform.position = new Vector3(t_Position.x, t_Position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D p_Collision) {
        if(p_Collision.tag == "Player") {
            m_State = State_Manager.e_STATE.TARGETING;
            m_Target = p_Collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D p_Collision) {
        if (p_Collision.tag == "Player") {
            m_State = State_Manager.e_STATE.PATROL;
        }
    }

}
