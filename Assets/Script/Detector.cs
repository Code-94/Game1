using System;
using UnityEngine;

public class Detector : MonoBehaviour
{

    [SerializeField] private Vector2 boxSize;

    [SerializeField] private LayerMask layerMask;

    public bool isTouched;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isTouched = Physics2D.OverlapBox(transform.position, boxSize, 0, layerMask);
    }


    private void OnDrawGizmos()
    {
        if (!isTouched)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, boxSize);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, boxSize);
        }
    }
}
