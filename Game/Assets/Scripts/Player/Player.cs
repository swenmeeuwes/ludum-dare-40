using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour, ISpellCaster
{
    [SerializeField] private Material _material;
    [SerializeField] private Material _hitMaterial;

    public Transform Transform { get; set; }

    [HideInInspector] public PlayerWand Wand;
    [HideInInspector] public PlayerMovement Movement;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        Transform = transform;

        Movement = GetComponent<PlayerMovement>();
        Wand = GetComponentInChildren<PlayerWand>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Safeguard for when falling out of the map somehow
        if (transform.position.y < -100)
            SceneLoader.Instance.ReloadCurrentSceneAsync();
    }

    public void Hit()
    {
        _spriteRenderer.material = _hitMaterial;

        Invoke("SetMaterialToDefault", 2f);
    }

    private void SetMaterialToDefault()
    {
        _spriteRenderer.material = _material;
    }
}
