using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NpcOldWizard : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Duck", false);
    }

    public void Duck()
    {
        _animator.SetBool("Duck", true);
    }

    public void Calm()
    {
        _animator.SetBool("Duck", false);
    }
}
