using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : MonoBehaviour {

    private Animator animator;
    public float speed;
    private Rigidbody rb;
    int floorMask;
    int wallMask;
    int enemyMask;
    Vector3 direction;



    float wMin = 0;
    float wMax = 360;
    float a = 270;
    float sPositive = 180;
    float sNegative = -180;
    float d = 90;
    float rotation;
    Transform chest;

    public void Start()
    {
       
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
        wallMask = LayerMask.GetMask("Wall");
        enemyMask = LayerMask.GetMask("Enemy");
        Physics.IgnoreLayerCollision(14, 10);
        //chest = animator.GetBoneTransform(HumanBodyBones.Head);
    }


    public void Update()
    {
        Move();
        Rotation();
        Animation();       
    }

    public void Move()
    {
        direction = new Vector3(0,0,0);

        if (Input.GetKey(KeyCode.W))
        {
            //rb.MovePosition(transform.position + transform.forward * speed *Time.deltaTime);
            //direction += transform.forward;
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //rb.MovePosition(transform.position - transform.forward * speed *Time.deltaTime);
            //direction -= transform.forward;
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //rb.MovePosition(transform.position - transform.right * speed * Time.deltaTime);
            //direction -= transform.right;
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            ///rb.MovePosition(transform.position + transform.right * speed * Time.deltaTime);
            //direction += transform.right;
            direction += Vector3.right;
        }

	
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);


    }

    public void Rotation()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

		if (Physics.Raycast(camRay, out hit, 1000, enemyMask))
        {

			Debug.DrawRay(camRay.origin,camRay.direction * 1000,Color.red);

			Vector3 playerToMouse = hit.collider.transform.position - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
            //chest.LookAt(floorHit.transform);
        }
		else if (Physics.Raycast(camRay, out hit, 1000, wallMask))
        {

			Debug.DrawRay(camRay.origin,camRay.direction * 1000,Color.blue);

			Vector3 playerToMouse = hit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
            //chest.LookAt(floorHit.transform);
        }
		else if (Physics.Raycast(camRay, out hit, 1000, floorMask))
		{
			Debug.DrawRay(camRay.origin,camRay.direction * 1000,Color.yellow);

			Vector3 playerToMouse = hit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			rb.MoveRotation(newRotation);
			//chest.LookAt(floorHit.transform);
		}
		 
    }

    public void Animation()
    {
        bool run = false;
        bool reverse = false;
        bool strafeLeft = false;
        bool strafeRight = false;
        bool idle = false;



        rotation = gameObject.transform.eulerAngles.y;
        if (rotation + wMin >= -45 && rotation + wMin <= 45)
        {
            //Facing up
            if (Input.GetKey(KeyCode.W))
            {
                run = true;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                strafeLeft = true;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                reverse = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                strafeRight = true;
            }
            else
            {
                idle = true;
            }


        }
        else if (rotation - wMax >= -45 && rotation - wMax <= 45)
        {
            //Facing up
            if (Input.GetKey(KeyCode.W))
            {
                strafeRight = true;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                run = true;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                strafeLeft = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                reverse = true;
            }
            else
            {
                idle = true;
            }

        }
        else if (rotation - a >= -45 && rotation - a <= 45)
        {
            //Facing left
            if (Input.GetKey(KeyCode.W))
            {
                strafeRight = true;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                run = true;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                strafeLeft = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                reverse = true;
            }
            else
            {
                idle = true;
            }

        }
        else if (rotation - d >= -45 && rotation - d <= 45)
        {
            //Facing right
            if (Input.GetKey(KeyCode.W))
            {
                strafeLeft = true;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                reverse = true;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                strafeRight = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                run = true;
            }
            else
            {
                idle = true;
            }

        }
        else if (rotation - sPositive >= -45 && rotation - sPositive <= -45)
        {
            //facing down
            if (Input.GetKey(KeyCode.W))
            {
                reverse = true;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                strafeRight = true;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                run = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                strafeLeft = true;
            }
            else
            {
                idle = true;
            }

        }
        else if (rotation + sNegative >= -45 && rotation + sNegative <= 45)
        {
            //facing down
            if (Input.GetKey(KeyCode.W))
            {
                reverse = true;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                strafeRight = true;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                run = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                strafeLeft = true;
            }
            else
            {
                idle = true;
            }

        }


       

        animator.SetBool("IsRunning", run);
        animator.SetBool("IsReversing", reverse);
        animator.SetBool("IsStrafingLeft", strafeLeft);
        animator.SetBool("IsStrafingRight", strafeRight);
        animator.SetBool("IsIdle", idle);

    }

}
