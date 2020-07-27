using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 20f;
    [SerializeField] float rotationSpeed = 5f;

    float distanceFromTarget = Mathf.Infinity;
    NavMeshAgent navMeshAgent;
    Transform target;

    bool isProvoked = false;

    EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform; 
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.IsDead())
        {
            this.enabled = false;
            navMeshAgent.enabled = false;
        }
        distanceFromTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            EngageWithTarget();
        }
        else if(distanceFromTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void EngageWithTarget()
    {
        FaceTarget();
        if(distanceFromTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else if(distanceFromTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("Attack", true);
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("Move");
        navMeshAgent.SetDestination(target.position);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
