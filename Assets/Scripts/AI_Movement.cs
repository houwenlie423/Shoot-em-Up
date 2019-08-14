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

    public float m_WaitTime;
    public float m_PatrolRange;
    private int t_I;
    private Vector3 m_Destination;
    private Vector3 m_PatrolCenter;
    private float m_CurrentTime;

    void Start(){
        m_PatrolCenter = transform.position;
        m_CurrentTime = m_WaitTime;
        m_Destination = m_PatrolCenter + Master_Utility.Instance.f_SetVector3(Random.Range(-m_PatrolRange, m_PatrolRange+1), Random.Range(-m_PatrolRange, m_PatrolRange+1),0);
    }


    void Update(){
        f_CheckBehaviour();
    }

    void f_CheckBehaviour() {
        if(m_State == State_Manager.e_STATE.TARGETING) {
            if(m_Type == State_Manager.e_TYPE.RANGE) {
                m_Collider = Physics2D.OverlapCircleAll(transform.position, 0.1f, m_Layer);
                for(t_I = 0; t_I < m_Collider.Length; t_I++) m_State = State_Manager.e_STATE.ATTACK;

;            }
        }

        if (m_State == State_Manager.e_STATE.ATTACK) {

        }

        if (m_State == State_Manager.e_STATE.TARGETING) {
            f_Approach();
        }

        if(m_State == State_Manager.e_STATE.PATROL) {
            f_Patrol();
        }
    }

    private void f_Targetting() {

    }

    private void f_Approach() {
        m_Dir = m_Target.position - transform.position;
        t_Position = Vector3.MoveTowards(transform.position, m_Target.position, m_MovementSpeed * Time.fixedDeltaTime);
        transform.position = Master_Utility.Instance.f_SetVector3(t_Position.x, t_Position.y, transform.position.z);
    }

    private void f_Patrol() {
        transform.position = Vector2.MoveTowards(transform.position, m_Destination, m_MovementSpeed * Time.deltaTime);
        if(Vector2.Distance(transform.position, m_Destination) <= 0.3f) {
            if (m_CurrentTime <= 0) {
                m_PatrolCenter = transform.position;
                m_Destination = m_PatrolCenter + Master_Utility.Instance.f_SetVector3(Random.Range(-m_PatrolRange, m_PatrolRange + 1), Random.Range(-m_PatrolRange, m_PatrolRange + 1), 0);
                m_CurrentTime = m_WaitTime;
            
            }else m_CurrentTime -= Time.deltaTime;
        }
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
