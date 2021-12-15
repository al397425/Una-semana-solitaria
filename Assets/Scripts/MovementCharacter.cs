using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    public float speed = 0.2f;
    private Vector3 targetPosition;
    private bool isMoving = false;
	Animator animator;
    Rigidbody2D rigidbody2d;
	SpriteRenderer sr;
    float scale = 0.01f;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
       /* if (Input.GetMouseButtonDown(0)) 
        {
            SetTargetPosition();
        }
*/
        if(isMoving)
        {
            Move();
        }
        void SetTargetPosition()
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;
            isMoving = true;
        }
        void Move()
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPosition);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if(transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }
    void FixedUpdate(){
        
       float horizontal = Input.GetAxis("Horizontal");
       float vertical = Input.GetAxis("Vertical");
	   
		if(horizontal == 0.0f && vertical  == 0.0f ){
			animator.SetBool("Andando", false);
	   }else{
		   animator.SetBool("Andando", true);
	   }
	   
	   if(horizontal > 0){
		   sr.flipX = true;
	   }else if(horizontal < 0){
		   sr.flipX = false;
	   }
	   
       Vector2 position = rigidbody2d.position;
       position.x = position.x + 5.0f * horizontal * Time.deltaTime;
       position.y = position.y + 5.0f * vertical * Time.deltaTime;
       if(Input.GetKeyDown(KeyCode.UpArrow)&& scale < 0.5){
           scale += 0.1f;
       transform.localScale = new Vector2(scale, scale);
       }
       if(Input.GetKeyDown(KeyCode.DownArrow) && scale > 0.3){
           scale -= 0.1f;
       transform.localScale = new Vector2(scale, scale);
       }
       rigidbody2d.MovePosition(position);
    }
}
