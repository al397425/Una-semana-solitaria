using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinijuegoObstaculos : MonoBehaviour
{

    public float speed = 3.15f;
    public float jumpforce = 5.0f;
    public GameObject CanvasObject;
    public int maxHealth = 7;
    int points = 0;
    public int pointsMax = 8;
    public float timeInvincible = 2.0f;
    public Animator animator;
    AudioSource audioDataJump;
    //public int health {  get { return currentHealth; }}
    int currentHealth;
    bool isInvincible;
    float invincibleTimer;
    public Image imagelife;
    public Image coinBar;
    Rigidbody2D rigidbody2d;
    public bool IJ;
    //Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Start()
    {
        IJ = false;
        currentHealth = maxHealth;
        audioDataJump = GetComponent<AudioSource>();
        animator.SetBool("Correr", true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3 ((speed-1.5f)*Time.deltaTime,0,0);
        if(points>=pointsMax)
        {
            SceneManager.LoadScene("Minijuego-Minerales");
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
            GetComponent<Rigidbody2D> ().velocity = new Vector3 (0.3f,jumpforce,0);
            audioDataJump.Play(0);
            IJ = true;
            animator.SetBool("Saltando", true);
            
        }
    }
    /*void OnTriggerEnter2D(Collider2D End)
    {
        if(End.gameObject.name.Contains("LimiteDeNivel"))
            {
                Debug.Log("EndLevel");
                transform.position += new Vector3(-5.0f, 0.65f, 0.0f);
                
            }
    }*/
    void OnTriggerEnter2D(Collider2D Coll)
    {
     //gana puntos   
            if(Coll.gameObject.tag=="GameController")
            {
                points = points+1;
                coinBar.fillAmount += 0.1f;
            }
    }
    void OnCollisionEnter2D(Collision2D Coll)
    {

        if(currentHealth >= 0)
            {
            if(Coll.gameObject.name.Contains("LimiteDeNivel"))
            {
                Debug.Log("EndLevel");
                transform.position = new Vector3(-5.0f, 0.65f, 0.0f);
                
            }

           
            //evitar saltar en el aire
            if(Coll.gameObject.tag=="tuberias")
            {
                IJ = false;
                animator.SetBool("Saltando", false);

            }

            if(Coll.gameObject.tag=="Finish")
            {
                imagelife.fillAmount -= 0.2f;
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
    
}

