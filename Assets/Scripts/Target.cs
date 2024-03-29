using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public float checkRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    public HealthBar healthbar;

    public void Start()
    {
        healthbar.SetMaxHealth(health);
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= checkRadius)
        {
            agent.SetDestination(target.position);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthbar.SetHealth(health);

        if(health <= 0f)
        {
            Die();
        }

        void Die()
        {
            Destroy(gameObject);
        }
    }
}
