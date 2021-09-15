
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    public Animator playerrig;
    public float Speed;  
    public float JumpForce;

    private float MoveX;
    private float MoveZ;
    private float SavedSpeed;
    Rigidbody rb;
    new Vector3 transform;

    controllerInputs ControllerIndex = controllerInputs.walking;
    enum controllerInputs
    {
        walking=0,Running=1,Crouch=2
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SavedSpeed = Speed;
    }
    
    void FixedUpdate()
    {
        InputHandler();
    }

    public void InputHandler() //mixing diffrent control sets allowing for crouch,running,walking
    {
        MouseHandler();
        #region ControllerIndex switch for multiple enum based controls (Z button)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (ControllerIndex == controllerInputs.Crouch) //loop enum with last and first
            {
                ControllerIndex = controllerInputs.walking;
            }
            else
            {
                ControllerIndex += 1;
            }
        }

        if (ControllerIndex == controllerInputs.walking) //link controller to different ways of movement 
        {
            Orientation(ControllerIndex);
            Movement(ControllerIndex);
            //add animations
        }
        if (ControllerIndex == controllerInputs.Running)
        {
            // Fly(ControllerIndex);
            Orientation(ControllerIndex);
        }
        if (ControllerIndex == controllerInputs.Crouch)
        {
            TpPlayerMotion(ControllerIndex);
            //toDo after animation controller
        }
        #endregion
        #region shift controls
        if (Input.GetKey(KeyCode.LeftShift)) //hold shift for going faster //ToDo run animations, enum control... //UIScript.changevalue(null,Cooldown);
        {
            if (Speed < SavedSpeed * 4)
            {
                Speed = Speed * 1.1f;
                Speed = Mathf.Lerp(Speed, 0f, Time.deltaTime);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) //release key to reset
        {
            Speed = SavedSpeed;
        }
    }
    #endregion 
    
    public void MouseHandler()
    {
        if (Input.GetMouseButton(0))
        {
            if (playerrig != null)
            {
                // play Bounce but start at a quarter of the way though
                playerrig.Play("Stunned", 0, 0.25f);
            }
            //playerrig.Play("Mixamo_com");
        }

    }
    void Fly(controllerInputs input) //exponentially increases with time
    {
        float weight = 1000;
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("W");
            rb.AddForce(Vector3.forward * (Time.deltaTime * weight));
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back * (Time.deltaTime * weight));
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * (Time.deltaTime * weight));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * (Time.deltaTime * weight));

        }
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * (Time.deltaTime * weight));
        }
       // rb.AddForce(Vector3.down * 10f); //extra gravity for ground control
    }
    void TpPlayerMotion(controllerInputs input)//tp controlls
    {
        MoveX = this.gameObject.transform.position.x;
        MoveZ = this.gameObject.transform.position.z;
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform = this.gameObject.transform.position;
            MoveZ += Speed;
            transform.z = MoveZ;
            this.gameObject.transform.position = transform;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform = this.gameObject.transform.position;
            MoveZ -= Speed;
            transform.z = MoveZ;
            this.gameObject.transform.position = transform;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform = this.gameObject.transform.position;
            MoveX -= Speed;
            transform.x = MoveX;
            this.gameObject.transform.position = transform;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform = this.gameObject.transform.position;
            MoveX += Speed;
            transform.x = MoveX;
            this.gameObject.transform.position = transform;

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform = this.gameObject.transform.position;
            JumpForce += Speed;
            transform.y = MoveX;
            this.gameObject.transform.position = transform;
        }
    }
    void Orientation(controllerInputs input)//tp controlls
    {   
        if (Input.GetKey(KeyCode.W))
        {
            this.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            
        }
        if (Input.GetKey(KeyCode.S))
        {       
            this.gameObject.transform.rotation = new Quaternion(0, 1, 0, 0);
           
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.rotation = new Quaternion(0, 90, 0, -90);
            
        }
        if (Input.GetKey(KeyCode.D))
        {
             this.gameObject.transform.rotation = new Quaternion(0, 90, 0, 90);
            
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.rotation = new Quaternion(0, -45, 0, 90);
           

        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.rotation = new Quaternion(0, 45, 0, 90);

        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.rotation = new Quaternion(0, -135, 0, -45);

        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.rotation = new Quaternion(0, -135, 0, 45);

        }
        
    }
    void Movement(controllerInputs input)//tp controlls
    {
        //o = 1;
        MoveX = this.gameObject.transform.position.x;
        MoveZ = this.gameObject.transform.position.z;
        if (Input.GetKey(KeyCode.W))
        {
            transform = this.gameObject.transform.position;
            MoveZ += Speed;   
            this.gameObject.transform.position = new Vector3(transform.x, transform.y, Mathf.Lerp(this.gameObject.transform.position.z, MoveZ, Time.deltaTime));
            
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform = this.gameObject.transform.position;
            MoveZ -= Speed;
            this.gameObject.transform.position = new Vector3(transform.x, transform.y, Mathf.Lerp(this.gameObject.transform.position.z, MoveZ, Time.deltaTime));
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveX -= Speed;
            transform = this.gameObject.transform.position;
            this.gameObject.transform.position = new Vector3(Mathf.Lerp(this.gameObject.transform.position.x, MoveX, Time.deltaTime), transform.y, transform.z);
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveX += Speed;
            transform = this.gameObject.transform.position;
            this.gameObject.transform.position = new Vector3(Mathf.Lerp(this.gameObject.transform.position.x, MoveX, Time.deltaTime), transform.y, transform.z);
            

        }
        if (Input.GetKey(KeyCode.Space))
        {
            this.gameObject.transform.position = new Vector3(transform.x, Mathf.Lerp(this.gameObject.transform.position.y, Speed, Time.deltaTime), transform.z);
        }
    }

}
