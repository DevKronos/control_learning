using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System;

public abstract class Enemy : MonoBehaviour , IDamagable
{ 
    public abstract float Health { get; set; }
    public abstract float CheckRadius { get; }
    public abstract float AttackCD { get; }
    public abstract bool CanAttack { get; }

    public Action<Enemy> DiedCallBackAction; 

    private float nextTimeToAttack = 0f;

    Transform target;
    NavMeshAgent agent;
    public HealthBar healthbar;
    public Animator animator;
    //public Transform cam;

    public void Start()
    {
        healthbar.SetMaxHealth(Health);
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(GetDistance() <= CheckRadius)
        {
            agent.SetDestination(target.position);

            if(InFieldAction())
            {
                if (CanAttack)
                {
                    FaceTarget();
                }
                if (Time.time >= nextTimeToAttack && CanAttack)
                {
                    nextTimeToAttack = Time.time + AttackCD;
                    StartCoroutine(Attack());
                }
            }
        }
    }

    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        healthbar.SetHealth(Health);

        if(Health <= 0f)
        {
            Die();
        }      
    }
    private void Die()
    {
        DiedCallBackAction?.Invoke(this);
        Destroy(gameObject);
    }

    public float GetDistance()
    {
        return Vector3.Distance(target.position, transform.position);
    }

    public bool InFieldAction()
    {
        if(GetDistance() <= agent.stoppingDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public abstract IEnumerator Attack();
}
