using UnityEngine;

public class Hit : MonoBehaviour
{
    
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Attack()
    {
        
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

        foreach (Collider2D enemy in hit)
        {
            Debug.Log("is hit");
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
        
    }
}
