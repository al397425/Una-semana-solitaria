using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinijuegoObstaculos : MonoBehaviour
{

    public float speed = 3.0f;
    public float jumpforce = 5.0f;
    public GameObject CanvasObject;
    public int maxHealth = 7;
    public int points = 0;
    public float timeInvincible = 2.0f;
    public Animator animator;
    
    //public int health {  get { return currentHealth; }}
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
        animator.SetBool("Correr", true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3 ((speed-1.5f)*Time.deltaTime,0,0);
        if(points>=10)
        {
            SceneManager.LoadScene("Level1");
        }
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
            GetComponent<Rigidbody2D> ().velocity = new Vector3 (speed,jumpforce,0);
            IJ = true;
            animator.SetBool("Saltando", true);
            
        }
    }

    void OnCollisionEnter2D(Collision2D Coll)
    {
        if(currentHealth >= 0)
            {
            //gana puntos   
            if(Coll.gameObject.tag=="GameController")
            {
                points = points+1;
            }
            //evitar saltar en el aire
            if(Coll.gameObject.tag=="tuberias")
            {
                IJ = false;
                animator.SetBool("Saltando", false);

            }

            if(Coll.gameObject.tag=="Finish")
            {
                IJ = false;
                animator.SetBool("Saltando", false);
                //muere
                currentHealth = currentHealth-1;
                Debug.Log(currentHealth);
                if(currentHealth <= 0)
                {
                    CanvasObject.SetActive(true);
                }
        }

   /*if(Coll.gameObject.tag=="Respawn")
        {
            Application.LoadLevel ("MinijuegoObstaculos");
        }*/
    }else{
        IJ = true;
    }
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

