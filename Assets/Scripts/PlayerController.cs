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
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            transform.Rotate(new Vector3(0, 180, 0));
        }

        if (canJump)
        {
            rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * mov, rb2d.velocity.y);
        }

        if (Input.GetButtonUp("Jump") & canJump){                    
            rb2d.AddRelativeForce(Vector2.up*25, ForceMode2D.Impulse);
            canJump = false;
        }
        if(rb2d.velocity.x > 0 & !isRight)
        {
            sprite.flipX = !sprite.flipX;
            isRight = true;
        }
        if (rb2d.velocity.x < 0 & isRight)
        {
            sprite.flipX = !sprite.flipX;
            isRight = false;
        }
        anim.SetFloat("mov", rb2d.velocity.sqrMagnitude);
        if(Input.GetButtonUp("Jump"))
        {
            anim.SetBool("jumpStart", true);
            anim.SetBool("jumpStop", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Ой!");
        canJump = true;
        anim.SetBool("jumpStart", false);
        anim.SetBool("jumpStop", true);
    }
    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Не ой!!");
        canJump = false;
    }

    public void TakeDmg(int _dmg)
    {
        HP -= _dmg;
        canMove = false;
    }
}