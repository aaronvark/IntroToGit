using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateType { Idle, Chase, Attack }
public abstract class CrawlerState
{
    protected CrawlerFSM owner;
    protected CrawlerOld enemy;
    protected Player player;

    public void Initialize(CrawlerFSM owner)
    {
        this.owner = owner;
        enemy = owner.owner;
        player = Player.pInstance;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class IdleState : CrawlerState
{
    public override void Enter()
    {

    }
    public override void Update()
    {
        //Debug.Log(Vector3.Distance(owner.owner.transform.position, Player.Instance.transform.position));
        if (Vector3.Distance(owner.owner.transform.position, Player.pInstance.transform.position) < owner.owner.detectionDistance)
        {
            owner.GotoState(EnemyStateType.Chase);
        }
    }
    public override void Exit()
    {

    }
}
public class ChaseState : CrawlerState
{
    public override void Enter()
    {
    }
    public override void Update()
    {
        enemy.navMeshAgent.SetDestination(Player.pInstance.transform.position);
    }
    public override void Exit()
    {

    }
}
public class AttackState : CrawlerState
{
    public override void Enter()
    {
    }
    public override void Update()
    {
    }
    public override void Exit()
    {

    }
}