using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullMagnet : MonoBehaviour
{
    [SerializeField] float magnetStrength = 5f;
    [SerializeField] float distanceStretch = 10f; // Strength, based on the distance

    private int magnetDirection; // 1 = attact, -1 = repel

    public bool looseMagnet = true;

    private float range = 1000f;

    public Camera cam;

    public Transform playerTransform;
    public Transform objectTransform;

    public Rigidbody playerRB, objectRB;

    public bool magnetInZone;
    public bool objectMode = true;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        cam = Camera.main;
        magnetDirection = 1;
    }

    private void Update()
    {
        ShootMagnetRay();
        HeleboelIfStatements();
    }

    private void HeleboelIfStatements()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            magnetDirection = magnetDirection * -1;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            objectMode = !objectMode;
        }

        if (objectMode)
        {
            MoveObject(objectRB, playerRB);
        }
        else
        {
            MoveObject(playerRB, objectRB);
        }
    }

    private void MoveObject(Rigidbody _objectThatStays, Rigidbody _objectThatMoves)
    {
        Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.green);
        if (magnetInZone)
        {
            Vector3 directionToMagnet = _objectThatStays.position - _objectThatMoves.position;
            float distance = Vector3.Distance(_objectThatStays.position, _objectThatMoves.position);
            float magnetDistanceStr = (distanceStretch / distance) * magnetStrength;

            _objectThatMoves.AddForce(magnetDistanceStr * (directionToMagnet * magnetDirection), ForceMode.Impulse);
        }
    }

    public void ShootMagnetRay()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                if (hit.transform.CompareTag("Magnet"))
                {
                    objectRB = hit.rigidbody;
                    magnetInZone = true;
                }
            }
        }
        else
        {
            objectRB = null;
            magnetInZone = false;
        }
        Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.green);
    }
}