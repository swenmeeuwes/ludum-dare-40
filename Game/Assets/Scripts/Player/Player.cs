using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] public float _initialMaxEnergy;
    [Tooltip("Energy usage per second while moving")][SerializeField] private float _energyUsage;

    public float MaxEnergy { get; private set; }
    public float Energy { get; private set; }

    private PlayerMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();

        MaxEnergy = _initialMaxEnergy;
        Energy = _initialMaxEnergy;
    }

    private void LateUpdate()
    {
        if(_movement.IsMoving)
            AddEnergy(-_energyUsage * Time.deltaTime);
        else
            AddEnergy(_energyUsage * Time.deltaTime);
    }

    private void AddEnergy(float addition)
    {
        Energy = Mathf.Clamp(Energy + addition, 0, MaxEnergy);
    }
}
