using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager : MonoBehaviour
{
    public static VisualManager instance;
    private void OnEnable()
    {
        instance = this;

        Eventmanager.Subscribe(EventType.ON_PLAYER_STOMP, Stomp);
        Eventmanager<GameObject>.Subscribe(EventType.ON_PLAYER_SLAP, Slap);
    }
    private void OnDisable()
    {
        Eventmanager.UnSubscribe(EventType.ON_PLAYER_STOMP, Stomp);
        Eventmanager<GameObject>.UnSubscribe(EventType.ON_PLAYER_SLAP, Slap);
    }

    public Animator moves;
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject stompRange;

    public void Slap(GameObject _objHand)
    {
        SlapEnumerator(_objHand);
    }

    public IEnumerator SlapEnumerator(GameObject _objHand)
    {
        yield return new WaitForSeconds(.05f);

        _objHand.SetActive(true);

        yield return new WaitForSeconds(.05f);

        _objHand.SetActive(false);
    }

    public void Stomp()
    {
        StartCoroutine(StompEnumerator());
    }

    public IEnumerator StompEnumerator()
    {
        moves.SetTrigger("Stomp");

        yield return new WaitForSeconds(.4f);

        stompRange.SetActive(true);

        yield return new WaitForSeconds(.2f);

        stompRange.SetActive(false);
    }
}
