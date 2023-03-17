using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer sprite; 
    public Rigidbody2D rb2d;
    public Animator anim;
    public float mov;
    bool canJump = true;
    bool isRight = true;
    [SerializeField]
    int HP = 100;
    bool canMove = true;
    private Vector3 velocity = Vector3.zero;
    [Range(0, .3f)] private float movementSmoothing = .05f;
    float move;
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) {
            move = Input.GetAxisRaw("Horizontal");
        }
        Vector2 targetVelocity = new Vector2(move * mov, rb2d.velocity.y);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, targetVelocity, ref velocity, movementSmoothing);

        if (Input.GetButtonUp("Jump") & canJump){                    
            rb2d.AddRelativeForce(Vector2.up*25, ForceMode2D.Impulse);
            canJump = false;
        }

        if(rb2d.velocity.x > 0 & !isRight)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            isRight = true;
        }

        if (rb2d.velocity.x < 0 & isRight)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            isRight = false;
        }

        anim.SetFloat("mov", rb2d.velocity.sqrMagnitude);
        if(Input.GetButtonUp("Jump"))
        {
            anim.SetBool("jumpStart", true);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            canMove = false;
            anim.SetTrigger("atk1");
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Ой!");
        canJump = true;
        anim.SetBool("jumpStart", false);
    }
    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Не ой!!");
        canJump = false;
    }

    public void TakeDmg(int _dmg, float _enemyX)
    {
        int dir = 1;
        if(_enemyX > transform.position.x)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }
        HP -= _dmg;
        rb2d.AddRelativeForce(new Vector2(10 * dir, 2.5f), ForceMode2D.Impulse);
        canMove = false;
        StartCoroutine("CanDo");
    }
    IEnumerator CanDo()
    {
        yield return new WaitForSeconds(2f);
        canMove = true;
        Debug.Log("Can move now!");
    }
    public void CanMove()
    {
        canMove = true;
    }
}