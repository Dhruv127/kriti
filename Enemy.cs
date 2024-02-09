using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update


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

    // Update is called once per frame
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
        //Cast rays for the left and right foot
		RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, 0f), Vector2.down, groundDistance);
		RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, 0f), Vector2.down, groundDistance);

		//If either ray hit the ground, the player is on the ground
		if (!leftCheck && !rightCheck)
        {
			isOnGround = false;
            // Instantiate(playerHealth.deathVFXPrefab, transform.position, transform.rotation);

            //Disable player game object
            gameObject.SetActive(false);
            Debug.Log("hi");
        }
        else if(torch.enabled&&((Mathf.Abs(player.position.x-transform.position.x)<=range&&Mathf.Abs(player.position.y-transform.position.y)<=0.5f)||triger))
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
		//Call the overloaded Raycast() method using the ground layermask and return 
		//the results
		return Raycast(offset, rayDirection, length, groundLayer);
	}

	RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
	{
		//Record the player's position
		Vector2 pos = transform.position;

		//Send out the desired raycasr and record the result
		RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

		// //If we want to show debug raycasts in the scene...
		// if (drawDebugRaycasts)
		// {
		// 	//...determine the color based on if the raycast hit...
		// 	Color color = hit ? Color.red : Color.green;
		// 	//...and draw the ray in the scene view
		// 	Debug.DrawRay(pos + offset, rayDirection * length, color);
		// }

		//Return the results of the raycast
		return hit;
	}




}
