
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    float moveX;
    float moveZ;
    public Animator playerrig;
   

    public float speed;
    float savedspeed;

    public float JumpForce;

    bool switchcontrols = true;

    Rigidbody rb;
    new Vector3 transform;

   public  int cooldown = 1;

    enum controls
    {
        tplerp=0,tp=1,ball=2
    }

    controls a = controls.ball;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        savedspeed = speed;
    }
    
    // Update is called once per frame
    void Update()
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
            if (Input.GetKeyDown(KeyCode.Z))
        {
            switchcontrols = !switchcontrols;
        }
        if (switchcontrols)
        {
            Orientation(true);
            Movement();
            

        }
        if (!switchcontrols)
        {
            MovePlayer();

        }
      
        if (Input.GetKey(KeyCode.LeftShift))
        {
            cooldown++;
            //UIScript.changevalue(null,cooldown);
            Debug.Log(cooldown);
            if (cooldown > 200) { speed = Mathf.Lerp(speed,0f,Time.deltaTime); }
            if(cooldown < 200)
            {
                speed = Mathf.Min( 14,Mathf.Lerp(0.03f, speed, Time.deltaTime)+ speed);
            }
            
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed -= speed;
            cooldown = 1;
            speed = savedspeed;
        }      
    }
    public void Cooldown()
    {
       // cooldown;
    }

    void MovePlayer() //exponentially increases with time
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("W");
            rb.AddForce(Vector3.forward * (Time.deltaTime *1000f));
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back * (Time.deltaTime * 1000f));
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * (Time.deltaTime * 1000f));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * (Time.deltaTime * 1000f));

        }
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * (Time.deltaTime * 1000f));
        }
        rb.AddForce(Vector3.down * 10f);
    }
    void TpPlayerMotion()//tp controlls
    {
        moveX = this.gameObject.transform.position.x;
        moveZ = this.gameObject.transform.position.z;
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform = this.gameObject.transform.position;
            moveZ += speed;
            transform.z = moveZ;
            this.gameObject.transform.position = transform;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform = this.gameObject.transform.position;
            moveZ -= speed;
            transform.z = moveZ;
            this.gameObject.transform.position = transform;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform = this.gameObject.transform.position;
            moveX -= speed;
            transform.x = moveX;
            this.gameObject.transform.position = transform;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform = this.gameObject.transform.position;
            moveX += speed;
            transform.x = moveX;
            this.gameObject.transform.position = transform;

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform = this.gameObject.transform.position;
            JumpForce += speed;
            transform.y = moveX;
            this.gameObject.transform.position = transform;
        }
    }
    //to do lerp
    void TpPlayerlerp()//tp controlls
    {
        moveX = this.gameObject.transform.position.x;
        moveZ = this.gameObject.transform.position.z;
        if (Input.GetKey(KeyCode.W))
        {
            transform = this.gameObject.transform.position;
            moveZ += speed;
            transform.z = moveZ;
            this.gameObject.transform.position = new Vector3 (transform.x,transform.y, Mathf.Lerp(this.gameObject.transform.position.z, moveZ,Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform = this.gameObject.transform.position;
            moveZ -= speed;
            transform.z = moveZ;
            this.gameObject.transform.position = new Vector3(transform.x, transform.y, Mathf.Lerp(this.gameObject.transform.position.z, moveZ, Time.deltaTime)); ;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform = this.gameObject.transform.position;
            moveX -= speed;
            transform.x = moveX;
            this.gameObject.transform.position = new Vector3(Mathf.Lerp(this.gameObject.transform.position.x, moveX, Time.deltaTime), transform.y,transform.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform = this.gameObject.transform.position;
            moveX += speed;
            transform.x = moveX;
            this.gameObject.transform.position = new Vector3(Mathf.Lerp(this.gameObject.transform.position.x, moveX, Time.deltaTime), transform.y, transform.z);

        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform = this.gameObject.transform.position;
            JumpForce += speed;
            transform.y = moveX;
            this.gameObject.transform.position = new Vector3(transform.x,Mathf.Lerp(this.gameObject.transform.position.y, JumpForce, Time.deltaTime), transform.z);
        }
    }
    void Orientation(bool yes)//tp controlls
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
    void Movement()//tp controlls
    {
        //o = 1;
        moveX = this.gameObject.transform.position.x;
        moveZ = this.gameObject.transform.position.z;
        if (Input.GetKey(KeyCode.W))
        {
            transform = this.gameObject.transform.position;
            moveZ += speed;   
            this.gameObject.transform.position = new Vector3(transform.x, transform.y, Mathf.Lerp(this.gameObject.transform.position.z, moveZ, Time.deltaTime));
            
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform = this.gameObject.transform.position;
            moveZ -= speed;
            this.gameObject.transform.position = new Vector3(transform.x, transform.y, Mathf.Lerp(this.gameObject.transform.position.z, moveZ, Time.deltaTime));
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX -= speed;
            transform = this.gameObject.transform.position;
            this.gameObject.transform.position = new Vector3(Mathf.Lerp(this.gameObject.transform.position.x, moveX, Time.deltaTime), transform.y, transform.z);
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX += speed;
            transform = this.gameObject.transform.position;
            this.gameObject.transform.position = new Vector3(Mathf.Lerp(this.gameObject.transform.position.x, moveX, Time.deltaTime), transform.y, transform.z);
            

        }
        if (Input.GetKey(KeyCode.Space))
        {
            this.gameObject.transform.position = new Vector3(transform.x, Mathf.Lerp(this.gameObject.transform.position.y, speed, Time.deltaTime), transform.z);
        }
    }

}
