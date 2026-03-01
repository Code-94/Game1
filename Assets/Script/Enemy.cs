using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float radiusOne;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float radiusTwo;
    
    [SerializeField] private float walkSpeed;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform eHitZone;
    [SerializeField] private float targetRadius;
    
    private PlayerMove _player;
    private Animator _animator;
    public float health;
    private bool isOnSight;
    private bool isHitable;
    private float sliceTime = 1f;
    private float nextSlice = 0f;
    public float damage;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        _player = GetComponent<PlayerMove>();
        _animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

        float move = walkSpeed * Time.deltaTime;
        isOnSight = Physics2D.OverlapCircle(transform.position, radiusOne, layerMask);
        isHitable = Physics2D.OverlapCircle(transform.position, radiusTwo, layerMask);

        if (isOnSight)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, move);
            _animator.SetBool("isRunning", true);

        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
        

        if (isHitable)
        {
            if (Time.time > nextSlice)
            {
                nextSlice = Time.time + sliceTime;
                _animator.SetTrigger("slice");
            }
        }
        if (health <= 0)
        {
            StartCoroutine(IsDying(0.5f));

        }
    }
    
    public void EnemyAttack()
    {
        Collider2D[] EHit = Physics2D.OverlapCircleAll(eHitZone.transform.position, targetRadius, layerMask);
        
        foreach (Collider2D joueur in EHit)
        {
            Debug.Log(" player is hit");
            joueur.GetComponent<PlayerMove>().playerHp -= damage;

        }
    }

    private void OnDrawGizmos()
    {
        if (!isOnSight)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radiusOne);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radiusOne);
        }

        if (!isHitable)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, radiusTwo);
        }
        else
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radiusTwo);
        }

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(eHitZone.transform.position,targetRadius);
    }

    

    private IEnumerator IsDying(float waitSecond)
    {
        
        _animator.SetTrigger("isDying");
        yield return new WaitForSeconds(waitSecond);
        gameObject.SetActive(false);
    }
    
    

    
}
