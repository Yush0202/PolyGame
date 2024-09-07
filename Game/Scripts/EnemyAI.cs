using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    public enum State { Idle,  Following,  Hitting }
    public State state;
    public GameObject m_Target;
    public float m_VisibleRange;
    public float m_HittingRange;
    public int m_DPS = 1;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    void Awake()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    void OnEnable()
    {
        StartCoroutine(FSM());//When the gameobject is activated, start the finite state machine
    }
    private bool WithinVisibleRange()
    {
        return Vector3.Distance(transform.position,m_Target.transform.position) < m_VisibleRange;
    }
    private bool WithinHittingRange()
    {
        return Vector3.Distance(transform.position, m_Target.transform.position) < m_HittingRange;
    }
    IEnumerator Idle()
    {   //Remain idle until the target gets too close
        while (state == State.Idle)
        {
            navMeshAgent.isStopped = true;
            if (WithinVisibleRange() && !m_Target.GetComponent<Health>().IsDead) state = State.Following;
            yield return null;
        }
    }
    IEnumerator Following()
    {
        while (state == State.Following)
        {   //Follow the target, if the target moves out of visible range go idle, if we get within hitting distance, start hitting the target
            if (!WithinVisibleRange()) state = State.Idle;
            navMeshAgent.SetDestination(m_Target.transform.position);
            navMeshAgent.isStopped = false;
            if (WithinHittingRange())
            {
                state = State.Hitting;
            }
            yield return null;
        }
    }
    IEnumerator Hitting()
    {
        Health health = m_Target.GetComponent<Health>();
        while (state == State.Hitting )
        {   //Every second do damage to the target
            navMeshAgent.isStopped = true;
            if (!WithinHittingRange()) state = State.Idle;
            if (health) health.TakeDamage(m_DPS, gameObject);
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator FSM() { while (true) yield return StartCoroutine(state.ToString()); } //Sets up the finite state machine
}
