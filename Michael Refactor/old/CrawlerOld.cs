using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CrawlerOld : Enemy
{
    private CrawlerFSM fsm;
    public NavMeshAgent navMeshAgent;
    private void Awake()
    {
        fsm = new CrawlerFSM();
        fsm.Initialize(this);
        fsm.AddState(EnemyStateType.Idle, new IdleState());
        fsm.AddState(EnemyStateType.Chase, new ChaseState());
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        fsm.GotoState(EnemyStateType.Idle);
    }

    private void Update()
    {
        fsm.UpdateState();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (!player.pIsHit)
            {
                player.TakeDamage();
            }
            Die();
        }
    }
}

