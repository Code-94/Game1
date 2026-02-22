using System;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float  moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashForce;
    [SerializeField] private Detector groundDetector;
    [SerializeField] private float gravityMod;

    private Rigidbody2D _rb;
    private PlayerI _input;
    private SpriteRenderer _sprite;
    private Animator _animator;
    private bool isIt;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerI>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
            Vector2 run = new Vector2(moveSpeed * _input.moveInput, Time.deltaTime);
            _rb.linearVelocityX = run.x;
            
            if (_rb.linearVelocityX > 0)
            {
                _animator.SetBool("isRunning", true);
                
            } else if (_rb.linearVelocityX < 0)
            {
                _animator.SetBool("isRunning", true);
            }
            else
            {
                _animator.SetBool("isRunning", false);
            }
            
            if (run.x < 0)
            {
                _sprite.flipX = true;
            }
            else if(run.x > 0)
            {
                _sprite.flipX = false;
            }

            if (_input.slashInput)
            {
                //_animator.SetBool("isSlashing", true);
                _animator.SetTrigger("slash");
            }
            // else
            // {
            //     _animator.SetBool("isSlashing", false);
            // }

            if (_input.dashInput)
            {
                if(run.x >= 0)
                {
                    _rb.AddForce(dashForce * Vector2.right, ForceMode2D.Impulse);
                    
                } else if (run.x <= 0)
                {
                    _rb.AddForce(dashForce * Vector2.left, ForceMode2D.Impulse);
                }
                _animator.SetTrigger("dash");
            }
            
            
        
    }

    private void FixedUpdate()
    {
        if (groundDetector.isTouched)
        {
            
            if (_input.jumpInput)
            {
                _rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
                //_animator.SetBool("isJumping", true);
                _animator.SetTrigger("jump");
                
            }
            else
            {
                //_animator.SetBool("isJumping", false);
            }
            //_animator.SetBool("isFalling" , false);

        }
        else
        {
            //_animator.SetBool("isFalling" , true);
            _rb.gravityScale = gravityMod;
        }
    }

    
}
