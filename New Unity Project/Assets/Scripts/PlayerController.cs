
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb2d;
	public float speed;
	private Animator animator;

	private Vector2 _movementForce;
	private Vector3 _mousePosition;
	private bool _shoot;
	
	public Rigidbody2D bulletPrefab;

	private Transform LeftArm;
	private Transform RightArm;

	public float bulletRadiusX;
	public float bulletRadiusY;
	public float bulletSpeed;

	public Camera cam;

	private List <Rigidbody2D> firedBullets = new List <Rigidbody2D>();

	private List <Quaternion> bulletAngles = new List <Quaternion>();

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		speed = 0.2f;
		bulletSpeed = 10f;
		bulletRadiusX = 100f;
		bulletRadiusY = 50f;

		LeftArm = transform.GetChild(0);
		RightArm = transform.GetChild(1);
	}

	void Update()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		_movementForce = new Vector2( moveHorizontal * speed, moveVertical * speed );

		_mousePosition = cam.ScreenToWorldPoint( Input.mousePosition );

		if(Input.GetButtonDown("Fire1")){
			_shoot = true;
		}

	}

	void FixedUpdate()
	{
		Movement();

		if( _shoot ){

			Vector2 orientation = _mousePosition - transform.position;

			Vector2 position = ( orientation.x < 0f )? LeftArm.GetChild(0).position : RightArm.GetChild(0).position;

			Vector2 direction =  ( orientation.x < 0f )? 
									(LeftArm.GetChild(0).position - LeftArm.GetChild(1).position) : 
									(RightArm.GetChild(0).position - RightArm.GetChild(1).position);
			
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			Quaternion rotation = Quaternion.Euler(0, 0, angle );

			Shoot( position , rotation );
			_shoot = false;
		}
		
		leadBullets();
	}

	void Movement ()
	{
		transform.Translate( _movementForce );

		if(_movementForce.y > 0f){
			animator.SetBool("walkingUp",true);
			animator.SetBool("walkingDown", false);
		}
		else if(_movementForce.y < 0f){
			animator.SetBool("walkingUp",false);
			animator.SetBool("walkingDown", true);
		}
		else if (_movementForce.x != 0f){
			animator.SetBool("walkingUp",false);
			animator.SetBool("walkingDown", true);
		}
		else if(_movementForce.x == 0f && _movementForce.y == 0f){
			animator.SetBool("walkingUp",false);
			animator.SetBool("walkingDown", false);
		}
	}

	void Shoot (Vector2 position, Quaternion rotation)
	{

		Rigidbody2D bulletInstance = 
									Instantiate(
										bulletPrefab, 
										position , 
										transform.rotation
									) as Rigidbody2D;
		
		firedBullets.Add(bulletInstance);
		bulletAngles.Add(rotation);
	}

	void leadBullets()
	{
		foreach(Rigidbody2D bullet in firedBullets){

			var index = firedBullets.IndexOf(bullet);

			//bulletAngles[index] += bulletSpeed * Time.deltaTime;

        	//var offset = new Vector2(Mathf.Sin(bulletAngles[index]) * bulletRadiusX, Mathf.Cos(bulletAngles[index]) * bulletRadiusY) * Time.deltaTime;

			//bullet.transform.Translate(offset);
			
			//bullet.transform.Translate( Vector2.up * 0.3f);

			bullet.transform.Translate( bulletAngles[index] * Vector2.right * 0.3f );
			
		}
	}

}
