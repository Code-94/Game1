using System;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float  moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashForce;
    [SerializeField] private Detector groundDetector;
    [SerializeField] private Transform rightHitDetector;
    [SerializeField] private Transform leftHitDetector;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float playerMaxHp;
    [SerializeField] private ParticleSystem flash;
    
    
    [SerializeField] private float gravityMod;

    private Rigidbody2D _rb;
    private PlayerI _input;
    private SpriteRenderer _sprite;
    private Animator _animator;
    private Enemy _enemy;
    
    public float playerHp;
    
    public float damage;

    

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerI>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        

    }

    // Update is called once per frame
    void Update()
    {
            Vector2 run = new Vector2(moveSpeed * _input.moveInput, Time.deltaTime);
            _rb.linearVelocityX = run.x;
            
            if (_rb.linearVelocityX > 0)
            {
                _animator.SetBool("isRunning", true);
                
                if (_input.jumpInput)
                {
                    _rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
                    _animator.SetBool("isJumping", true);
                }
                else
                {
                    _animator.SetBool("isJumping", false);
                }
                
                
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
                _animator.SetBool("isSlashing", true);
                flash.Play();
                //_animator.SetTrigger("slash");
                
            }else
            {
                 _animator.SetBool("isSlashing", false);
            }

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

            if (playerHp <= 0)
            {
                StartCoroutine(IsDying(1.0f));
            }
            
            
        
    }

    private void FixedUpdate()
    {
        if (groundDetector.isTouched)
        {
            
            if (_input.jumpInput)
            {
                _rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
                _animator.SetBool("isJumping", true);
                
                
            }
            else
            {
                _animator.SetBool("isJumping", false);
            }
            //_animator.SetBool("isFalling" , false);

        }
        else
        {
            //_animator.SetBool("isFalling" , true);
            _rb.gravityScale = gravityMod;
        }
    }

    public void Attack()
    {
        Collider2D[] Rhit = Physics2D.OverlapCircleAll(rightHitDetector.transform.position, radius, layerMask);

        foreach (Collider2D enemy in Rhit)
        {
            Debug.Log("is hit");
            enemy.GetComponent<Enemy>().health -= damage;

        }
        
        Collider2D[] Lhit = Physics2D.OverlapCircleAll(leftHitDetector.transform.position, radius, layerMask);

        foreach (Collider2D enemy in Lhit)
        {
            Debug.Log("is hit");
            enemy.GetComponent<Enemy>().health -= damage;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(rightHitDetector.transform.position, radius);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(leftHitDetector.transform.position, radius);
    }
    
    private IEnumerator IsDying(float waitSecond)
    {
        
        _animator.SetTrigger("isDead");
        yield return new WaitForSeconds(waitSecond);
        gameObject.SetActive(false);
    }
}
