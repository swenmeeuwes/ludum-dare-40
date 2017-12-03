using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlockHolder : MonoBehaviour
{
    [SerializeField] private IceBlock _iceBlock;

    public void Freeze(float amount)
    {
        _iceBlock.gameObject.SetActive(true);
        _iceBlock.AddHealth(amount);
    }
}
