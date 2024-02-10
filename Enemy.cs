using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    private Vector2 movement;
    PlayerMovement Movement;
    PlayerHealth playerHealth;
    public Transform player;
    [SerializeField] private Light torch;
    [SerializeField] public float followspeed;
    [SerializeField] private float range;
    private Rigidbody2D rb;
    public bool isOnGround;	
    public float groundDistance = 1;
    public float footOffset = 1;
    public LayerMask groundLayer;
    bool triger=false;
    



    void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        Movement=GetComponent<PlayerMovement>();
        playerHealth=GetComponent<PlayerHealth>();
    }

    void Update()
    {

        Vector3 direction=player.position-transform.position;
        direction.Normalize();
        movement=direction;
        if(transform.position.x>player.position.x)
        transform.localScale=new Vector3(-1*Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
        else
        transform.localScale=new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);

        
    }

    void FixedUpdate()
    {
        
        if(torch.enabled&&((Mathf.Abs(player.position.x-transform.position.x)<=range&&Mathf.Abs(player.position.y-transform.position.y)<=0.5f)||triger))
        {
        moveCharacter(movement);  
        triger=true;
        }
    }

    void moveCharacter(Vector2 direction)
    {


        float horizontalMovement = direction.x;

        isOnGround=true;
        rb.MovePosition(new Vector2(transform.position.x + (horizontalMovement * followspeed * Time.deltaTime), transform.position.y));
    }

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
	{
		return Raycast(offset, rayDirection, length, groundLayer);
	}

	RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
	{
		Vector2 pos = transform.position;

		RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);


		return hit;
	}




}
