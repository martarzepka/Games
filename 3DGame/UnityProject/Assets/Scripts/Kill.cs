using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            animator.enabled = false;
            controller.enabled = false;
        }
    }
}
