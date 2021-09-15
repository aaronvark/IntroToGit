using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Alles wat met de speler te maken heeft
public class Movement : MonoBehaviour
{
    public GameObject redVeil; //redveil voor ondamage
    public float speed = 3; //snelheid van speler
    public float jumpPower = 5; //springhoogte van speler

    private Vector3 _hitVector =  new Vector3(0,0,0); //de hitvector wanneer geraakt
    public Vector3 hitVector { //custom setter voor het resetten van de timer en aanzetten van de redveil
        get{return _hitVector;}
        set{hitTimer = hitTimerDefault;
            redVeil.SetActive(true);
            _hitVector = value;}

    }
    private float hitTimerDefault = .2f; //hittimer values
    private float hitTimer = 0;

    private Rigidbody rb;

    private void Awake(){
        rb = GetComponent<Rigidbody>();
    }

    private void Update(){
        Vector3 velocity = Vector3.zero; //velocity initializeren

        velocity += Input.GetAxis("Horizontal") * transform.right; //horizontal input toevoegen
        velocity += Input.GetAxis("Vertical") * transform.forward; //verticale input toevoegen

        velocity = velocity.normalized * speed; //vermeelvoudegen met snelheid en absolut maken
        if (Input.GetKey(KeyCode.Space) && Mathf.Abs(rb.velocity.y) <= .05) //als spatie ingedrukt is en val snelheid lager of gelijk is aan .05
            velocity += Vector3.up * jumpPower; //springen 

        if(hitTimer > 0) //hitTimer bijhouden en redveil uitzetten
            hitTimer -= Time.deltaTime;
        else if(hitVector != Vector3.zero){
            hitVector = Vector3.zero;
            redVeil.SetActive(false);
        }

        rb.velocity = velocity + new Vector3(0,rb.velocity.y,0) + hitVector; //velocity aanpassen
    }
}