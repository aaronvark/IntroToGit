using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public float range;
    public float damage;
    public float stompRange;

    private float slapcooldown;
    private float stompcooldown;
    private float slapMultiplier;

    private GameManager gameManager = GameManager.instance;
    private VisualManager visualManager = VisualManager.instance;

    InputMouseManager inputMouseManager = new InputMouseManager();

    private void OnEnable()
    {
        inputMouseManager.RegisterKey(InputMouseManager.MouseButtonType.Left, SlapAttack, null, null);
        inputMouseManager.RegisterKey(InputMouseManager.MouseButtonType.Right, StompAttack, null, null);
    }
    private void OnDisable()
    {
        inputMouseManager.UnRegisterKey(InputMouseManager.MouseButtonType.Left, SlapAttack, null, null);
        inputMouseManager.UnRegisterKey(InputMouseManager.MouseButtonType.Right, StompAttack, null, null);
    }

    private void SlapAttack()
    {
        if (slapcooldown < 1)
        {
            Eventmanager<GameObject>.Invoke(EventType.ON_PLAYER_SLAP, visualManager.rightHand);

            slapMultiplier = 1.5f;
            slapcooldown = 2;

            Slap();
        }

        if (slapMultiplier > 0 && slapMultiplier < .7)
        {
            Eventmanager<GameObject>.Invoke(EventType.ON_PLAYER_SLAP, visualManager.leftHand);

            slapcooldown = 3;

            Slap();
        }

        if (slapcooldown > 0) slapcooldown -= Time.deltaTime * 5;
        if (slapMultiplier > 0) slapMultiplier -= Time.deltaTime * 5;
    }

    public void Slap()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, range))
        {
            var target = hit.transform.GetComponent<IDamagable>();

            if(target != null)
            {
                float dealt = Random.Range(damage + gameManager.damageIncrease, damage + gameManager.damageIncrease * 2);
                target.TakeDamage(dealt);
            }
        }
    }

    private void StompAttack()
    {
        if (stompcooldown < 1)
        {
            Eventmanager.Invoke(EventType.ON_PLAYER_STOMP);
            Invoke("Stomp", .4f);

            stompcooldown = 5;
        }
        if (stompcooldown > 0) stompcooldown -= Time.deltaTime;
    }

    public void Stomp()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, stompRange);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            var target = hitColliders[i].transform.GetComponent<IDamagable>();

            if (target != null)
            {
                float dealt = Random.Range(damage * 1.72f - gameManager.damageIncrease, damage * 1.72f + gameManager.damageIncrease);
                target.TakeDamage(dealt);
            }
        }
    }
}
