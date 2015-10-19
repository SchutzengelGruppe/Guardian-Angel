using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	//Don't delete!!!
	private Animator anim;
	private Rigidbody2D rigid;
	private AudioSource audioCom;

	//AudioClips
	public AudioClip WalkClip;
	public AudioClip JumpClip;

	//Some Variables for the movement. Please change that in Inspector!!!
	public bool DoubleJump = false;
	public float WalkSpeed = 10f;
	public float jumpForce = 1000f;
    public float flyMass = 1f;
    public float normalMass = 5f;
	private bool facingRight = true;
	private bool secondJump = false;

	//For ground-detection
	private bool grounded = false;
	public Transform groundCheck;
	private float groundRadius = 0.2f;
	public LayerMask whatIsGround;


	void Start () {
	
		//anim = gameObject.GetComponent<Animator>();
		rigid = gameObject.GetComponent<Rigidbody2D> ();
		audioCom = gameObject.GetComponent<AudioSource> ();

	}
	

	void FixedUpdate () {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		if (grounded) {
			secondJump =false;
		}

		//anim.SetBool ("Ground",grounded);

		Jump ();
		Walk ();

	}

	public void Walk() {
	
		float move = Input.GetAxis ("Horizontal");
		//anim.SetFloat ("Speed", Mathf.Abs (move));

		rigid.velocity = new Vector2 (move * WalkSpeed, rigid.velocity.y);

		if (move > 0 && !facingRight) {
			Flip ();
		} else if(move < 0 && facingRight) {
			Flip ();
		}

	}

	public void Jump() {
	
		if (grounded && Input.GetKeyDown (KeyCode.Space)) {
		
			rigid.AddForce(new Vector2(0, jumpForce));
			audioCom.PlayOneShot(JumpClip, 1F);
		}

		if (DoubleJump == true) {
			if (!grounded && !secondJump && Input.GetKeyDown (KeyCode.Space)) {
		
				secondJump = true;
				rigid.AddForce (new Vector2 (0, jumpForce));
				audioCom.PlayOneShot (JumpClip, 1F);
			}
		}

        if(!grounded && Input.GetKey(KeyCode.LeftShift))
        {
            rigid.gravityScale = flyMass;
        } else
        {
            rigid.gravityScale = normalMass;
        }

	}

	public void Flip(){
	
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;

		theScale.x *= -1;

		transform.localScale = theScale;

	}

	public void FootStep(){


			audioCom.PlayOneShot(WalkClip, 1F);

	}
}
