using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerI : MonoBehaviour
{

    public float moveInput;

    public bool jumpInput;

    public bool slashInput;

    public bool dashInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Debug.Log("is running" + ctx.ReadValue<float>());
        moveInput = ctx.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        
            Debug.Log("is jumping");
            jumpInput = ctx.ReadValueAsButton();
        
        
        
        
    }
    
    public void OnSlash(InputAction.CallbackContext ctx)
    {
        Debug.Log("is slashing");
        slashInput = ctx.ReadValueAsButton();
    }
    
    public void OnDash(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            StartCoroutine(Once(0.5f));
        }

        
        
    }

    private IEnumerator Once(float waitSecond)
    {
        Debug.Log("is dashing");
        dashInput = true;
        yield return new WaitForSeconds(waitSecond);
        dashInput = false;
        
    }
}
