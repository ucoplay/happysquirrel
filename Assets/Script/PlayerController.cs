using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Animator animator;
    float moveForce = 8f;
    float jumpForce = 6.5f;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        move();
        jump();
    }

    //移动
    void move() {
        float keyInput = Input.GetAxis("Horizontal");
        if (keyInput < 0)
        {
            playRunLeft();
            rigidbody2D.velocity = new Vector2(-moveForce, rigidbody2D.velocity.y);
        }
        else if (keyInput > 0)
        {
            playRunRight();
            rigidbody2D.velocity = new Vector2(moveForce, rigidbody2D.velocity.y);
        }
        else {
            //rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            playIdle();
        } 
    }
    //播放向左奔跑的动画
    void playRunLeft() {
        animator.SetInteger("state",1);
        transform.localScale = new Vector3(-1,1,1);
    }
    //播放向右奔跑的动画
    void playRunRight()
    {
        animator.SetInteger("state", 1);
        transform.localScale = new Vector3(1, 1, 1);
    }
    //播放放空的动画
    void playIdle() {
        animator.SetInteger("state", 0);
    }
    //播放跳跃的动画
    void playJump() {
        animator.SetInteger("state", 2);
    }
    //控制跳跃
    void jump() {
        if (standOnSomething()) {
            if (Input.GetButtonDown("Vertical"))
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
                //playJump();
            }
        }
            
        if (rigidbody2D.velocity.y > 0)
            playJump();
    }

    bool standOnSomething() {
        Debug.Log(rigidbody2D.IsTouchingLayers());
        return rigidbody2D.IsTouchingLayers();
    }
}
