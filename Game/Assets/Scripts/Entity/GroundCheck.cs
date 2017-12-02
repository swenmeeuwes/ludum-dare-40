﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GroundCheck : MonoBehaviour {
    public bool IsGrounded { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IsGrounded = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        IsGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IsGrounded = false;
    }
}
