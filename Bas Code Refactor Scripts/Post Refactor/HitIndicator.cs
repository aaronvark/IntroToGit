using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitIndicator : MonoBehaviour
{
    public Camera hitIndicatorCamera;

    private void OnEnable()
    {
        EventManager.Subscribe(EventType.PLAYER_HIT, IndicateDamage);
    }

    private void OnDisable()
    {
        EventManager.UnSubscribe(EventType.PLAYER_HIT, IndicateDamage);

    }

    public void IndicateDamage()
    {
        StartCoroutine(indicateDamage());
    }

    private IEnumerator indicateDamage()
    {
        hitIndicatorCamera.enabled = true;
        yield return new WaitForSeconds(0.09f);
        hitIndicatorCamera.enabled = false;

    }
}
