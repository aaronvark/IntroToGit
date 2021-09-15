using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnetPull : MonoBehaviour
{
    // Public Variables
    public float magnetStrength = 5f;
    public float distanceStretch = 10f; // Strength, based on the distance
    public int magnetDirection; // 1 = attact, -1 = repel
    public bool looseMagnet = true;
    private float range = 1000f;
    public Camera cam;

    // Private Variables
    public Transform trans;
    public Transform magnetTrans;
    public Rigidbody thisRd;
    public bool magnetInZone;


    void Awake()
    {
        trans = transform;

        thisRd = trans.GetComponent<Rigidbody>();
        cam = Camera.main;
        magnetDirection = 1;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            magnetDirection = magnetDirection * -1;
        }          
 
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                if (hit.transform.CompareTag("Magnet"))
                {
                    magnetTrans = hit.transform;
                    magnetInZone = true;
                }
            }
        }
        else
        {
            magnetInZone = false;
        }

        Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.green);
        if (magnetInZone)
        {
            Vector3 directionToMagnet = magnetTrans.position - trans.position;
            float distance = Vector3.Distance(magnetTrans.position, trans.position);
            float magnetDistanceStr = (distanceStretch / distance) * magnetStrength;

            thisRd.AddForce(magnetDistanceStr * (directionToMagnet * magnetDirection), ForceMode.Force);
        }

    }
}
