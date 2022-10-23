using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        movement.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        float fltHorizontalThrow = movement.ReadValue<Vector2>().x; 
        float fltVerticalThrow = movement.ReadValue<Vector2>().y;

        Debug.Log(fltHorizontalThrow); 
        Debug.Log(fltVerticalThrow); 
    }
}
