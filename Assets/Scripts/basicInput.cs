using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class basicInput : MonoBehaviour
{

    private Rigidbody rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed){
            Debug.Log("Jump " + context.phase);
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }
}
