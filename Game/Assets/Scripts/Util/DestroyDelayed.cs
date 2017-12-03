using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelayed : MonoBehaviour
{
    [SerializeField] private float _secondsTillDestroy;

    private void Awake()
    {
        Invoke("Die", _secondsTillDestroy);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
