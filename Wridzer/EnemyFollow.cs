using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private GameObject playerInstance;

    public void GetPlayer(GameObject _playerInstance)
    {
        playerInstance = _playerInstance;
    }

    // Update is called once per frame
    void Update()
    {
        EnimMove();
    }

    private void EnimMove()
    {
        Vector3 dir = (playerInstance.transform.position - transform.position).normalized;
        GetComponent<Movement>().Move(dir);
    }
}
