using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinijuegoObstaculos : MonoBehaviour
{

    public float speed = 3.0f;
    public float jumpforce = 5.0f;
    
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    
    public int health {  get { return currentHealth; }}
    int currentHealth;
    bool isInvincible;
    float invincibleTimer;
    
    Rigidbody2D rigidbody2d;
    public bool IJ;
    //Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Start()
    {
        IJ = false;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        /*float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump");*/


        //Movement
        
        //Vector2 position = rigidbody2d.position;
        //position.x = position.x + speed * horizontal * Time.deltaTime;
        //position.y = position.y + speed * vertical * Time.deltaTime;
        //Jump
        //position.y = position.y + jumpforce * jump *Time.deltaTime;
        
        //rigidbody2d.MovePosition(position);



        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if(Input.GetKey(KeyCode.Space) && IJ==false)
        {
            GetComponent<Rigidbody2D> ().velocity = new Vector3 (10,35,0);
            IJ = true;
            Debug.Log(IJ);
        }
    }

    void OnCollisionEnter2D(Collision2D Coll)
    {
        Debug.Log("pipo");
        if(Coll.gameObject.tag=="Suelo")
        {
            IJ = false;
            Debug.Log(IJ);
        }

        if(Coll.gameObject.tag=="Obstacle")
        {
            IJ = false;
            Debug.Log(IJ);
        }

   /*if(Coll.gameObject.tag=="Respawn")
        {
            Application.LoadLevel ("MinijuegoObstaculos");
        }*/
    }
    
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}

