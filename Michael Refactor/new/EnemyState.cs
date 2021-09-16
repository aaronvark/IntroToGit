using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState : State<Enemy>
{
    public FSM<Enemy> owner;
    public EnemyState(FSM<Enemy> _owner)
    {
        owner = _owner;
    }

    public override void OnEnter()
    {
    }

    public override void OnUpdate()
    {
    }

    public override void OnExit()
    {
    }
}

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(FSM<Enemy> _owner) : base(_owner) { }

    public override void OnEnter()
    {
        Debug.Log("idle");
    }

    public override void OnUpdate()
    {
        if (Vector3.Distance(owner.pOwner.transform.position, Player.pInstance.transform.position) < owner.pOwner.detectionDistance)
        {
            owner.SwitchState(typeof(EnemyChaseState));
        }

        // below is more loosely coupled but too much of a performance hit

        //Collider[] hitColliders = Physics.OverlapSphere(owner.Owner.transform.position, owner.Owner.detectionDistance);
        //foreach (Collider c in hitColliders)
        //{
        //    IDetectable detectable = c.GetComponent<IDetectable>();
        //    Debug.Log(detectable);
        //    if (detectable != null)
        //    {
        //        // go to chase state
        //        owner.Owner.target = detectable;
        //        owner.SwitchState(typeof(EnemyChaseState));
        //    }
        //}
        //Debug.Log("idle updating");
    }

    public override void OnExit()
    {
    }
}

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(FSM<Enemy> _owner) : base(_owner) { }
    private NavMeshAgent navMeshAgent;

    public override void OnEnter()
    {
        navMeshAgent = owner.pOwner.GetComponent<NavMeshAgent>();
    }

    public override void OnUpdate()
    {
        navMeshAgent.SetDestination(Player.pInstance.transform.position);
    }

    public override void OnExit()
    {

    }
}