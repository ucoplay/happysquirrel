﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Animator animator;
    float moveForce = 8f;
    public float jumpForce = 6.5f;
    bool isJumpping = false;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        playIdle();
        jump();
        move();
        crouch();
        //playCrouch();
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
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
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
    void playCrouch() {
        animator.SetInteger("state",3);
        Debug.Log(animator.GetInteger("state"));
    }
    //控制跳跃
    void jump() {
        if (standOnSomething()) {
            isJumpping = false;
            if (Input.GetAxis("Vertical")>0)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
                isJumpping = true;
            }
        }

        if (rigidbody2D.velocity.y > 0 && isJumpping)
        {
            playJump();
        }
           
    }

    bool standOnSomething() {
        return rigidbody2D.IsTouchingLayers();
    }

    void crouch() {     
        if (standOnSomething())
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                playCrouch();
            } else if (Input.GetButtonUp("up")) {
                playIdle();
            }
        }
    }
}
