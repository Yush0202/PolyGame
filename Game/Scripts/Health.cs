using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    public int m_StartingHealth = 10;
    public int m_CurrentHealth;
    private bool m_IsDead;

    public bool IsDead { get { return m_IsDead; }  }

    void OnEnable () {
        m_CurrentHealth = m_StartingHealth;
        m_IsDead = false;
    }
    public void TakeDamage(int amount,GameObject damager)
    {
        if (m_IsDead) return;
        Debug.Log("Taking Damage:" + amount + " from:" + damager.name );
        m_CurrentHealth -= amount;
        if (m_CurrentHealth <= 0 && !m_IsDead) Death();
    }
    void Death()
    {
        Debug.Log("Death!");
        m_IsDead = true;
        gameObject.SendMessage("OnDeath");//Call the OnDeath() method on any script that is also attached to this gameobject
    }
}


