using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//handles movement, health ect. of the player character
[RequireComponent(typeof(Rigidbody))]
public class PlayerGeneral : MonoBehaviour {

	public float healthMax;
	private float heathCurrent;
	public float armor;
	public float speed;
	public float speedCap;


	private Animator animator;
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


	//UI elements
	public Text healthText;



	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		floorMask = LayerMask.GetMask("Floor");
		wallMask = LayerMask.GetMask("Wall");
		enemyMask = LayerMask.GetMask("Enemy");
		Physics.IgnoreLayerCollision(14, 10);	
		heathCurrent = healthMax;
		updateUI ();
	}


	void FixedUpdate()
	{
		Move();
		Rotation();
	}

	// Update is called once per frame
	void Update () 
	{
		

		//Animation(); 
	}


	void updateUI()
	{
		string healthPoint = char.ConvertFromUtf32(9829);
		string text = "Heatlh: ("+ heathCurrent +") ";
		for (int i = 0; i < healthMax / 10; i++)
		{
			if (i < heathCurrent / 10)
			{
				text += healthPoint;
			} 
		}

		//healthText.text = text;

	}
	//character takes damage
	public void TakeDamage(float damage)
	{
		if (armor >= damage)
		{
			heathCurrent--;
		} else
		{
			heathCurrent -= (damage - armor);
		}
		updateUI ();
		if (heathCurrent <= 0)
		{
			Die();
		}

	}

	//character dies
	public void Die()
	{
		Destroy(gameObject);
	}



	public void Move()
	{
		direction = new Vector3();
		direction.x = Input.GetAxis ("Horizontal");
		direction.z = Input.GetAxis ("Vertical");

		//creates a stoping force on a moving character equal to 1/10 of its speed
		if (Input.GetAxis ("Horizontal") == 0 && Input.GetAxis ("Vertical") == 0)
		{
			rb.AddForce (-(rb.velocity * speed/10f));

		}

		//if current dirrection of movement and desired one are apart less then x degrees act like they are equal otherwise subtract undesired movement
		if (Vector3.Angle(direction,rb.velocity) < 5f)
		{
			rb.AddForce (direction * speed);
		} 
		else
		{
			rb.AddForce (direction * speed - (rb.velocity));
		}

		//keeps character from exceeding its speed limit
		if(rb.velocity.magnitude > speedCap)
		{
			rb.velocity = rb.velocity.normalized * speedCap;
		}



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
