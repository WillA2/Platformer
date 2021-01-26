using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ame_movement : MonoBehaviour
{
    [SerializeField] private LayerMask platforms;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private SpriteRenderer mSprite;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private Enemy_interaction interact;
    public float bounce_force = 10.0f;
    public float speed = 5.0f;
    public float jump_force = 60.0f;
    public float ground_pound_force = 100.0f;
    public float fall = 2.5f;
    public float low_jump = 2.0f;
    public int MAX_JUMP = 2;

    private Vector2 prev_pos;
    private bool is_pounding;
    private int jump_count;
    private bool re_pound;
    private bool pound_attack;

    // Start is called before the first frame update
    void Start()
    {

        prev_pos = Vector2.zero;
        is_pounding = false;
        jump_count = 1;
        re_pound = true;
        pound_attack = false;
    }

    // Update is called once per frame
    void Update()
    {


        Vector2 pos = transform.position;
        player.constraints = RigidbodyConstraints2D.FreezeRotation;
         
        if (player.velocity.y < 0 && !is_pounding)
        {
            player.velocity += Vector2.up * Physics2D.gravity.y * (fall - 1) * Time.deltaTime;
        }
        else if (player.velocity.y > 0 && !Input.GetKey("w"))
        {     
            player.velocity += Vector2.up * Physics2D.gravity.y * (low_jump - 1) * Time.deltaTime;
        }
     
        if (Input.GetKeyDown("w") && !is_pounding && (IsGrounded()  || jump_count < MAX_JUMP))
        {
            animator.Play("Jump");
            player.velocity = Vector2.up * jump_force;
            jump_count++;
        }
     
        if (Input.GetKey("s") && !IsGrounded() && !is_pounding && re_pound )
        {
            player.velocity = Vector2.zero;
            animator.Play("Ground_pound");
            player.gravityScale = 0;
            is_pounding = true;
 
        }
        if (Input.GetKey("a") && !is_pounding)
        {
            pos.x -= speed * Time.deltaTime;
            mSprite.flipX = false;
        }
       
        if (Input.GetKey("d") && !is_pounding)
        {
            pos.x += speed * Time.deltaTime;
            mSprite.flipX = true;
        }

        if (IsGrounded())
        {
            jump_count = 0;
            animator.speed = 1.5f;
            is_pounding = false;
            player.gravityScale = 1;
        }

        if (interact.attack() && is_pounding)
        {
            player.gravityScale = 1;
            animator.speed = 1.5f;
            is_pounding = false;
            pound_attack = true;
        }

        transform.position = pos;
        animator.SetBool("Double_Jump", jump_count > 0);
        animator.SetBool("Jump", player.velocity.y > 0);
        animator.SetBool("Grounded", IsGrounded());
        animator.SetFloat("Speed", Mathf.Abs(prev_pos.x - pos.x));
        prev_pos = pos;

        
    }

    /*Animation Event Handling*/
    void pound_now()
    {
        
        animator.speed = 0;
        player.velocity = Vector2.down * (ground_pound_force);
        
    }

    void bounce_back()
    {
        
        animator.speed = 4.5f;
        player.velocity = Vector2.up * (bounce_force);
        re_pound = false;

    }

    void pound_last_frame()
    {
        animator.speed = 1.5f;
        re_pound = true;

    }
    void fix_variables()
    {
        re_pound = true;
    }
   
    /*Check for if player is grounded*/
    private bool IsGrounded()
    {
        
        float extraHeight = 0.1f;
        RaycastHit2D ray = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeight, platforms);
        return ray.collider != null;
    
    }

    /*Getter and Setters for private variables*/
    public int getJump_count()
    {
        return jump_count;
    }

    public void setJump_count(int n)
    {
        jump_count = n;
    }
    public bool get_pound_attack()
    {
        if (pound_attack)
        {
            pound_attack = false;
            return true;
        }
        else
        {
            return false;
        }
    }
   


}
