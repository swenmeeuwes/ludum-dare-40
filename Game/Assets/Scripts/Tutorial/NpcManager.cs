using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoSingleton<NpcManager>
{
    private readonly List<StressingNpc> _npcs = new List<StressingNpc>();

    private Player _player;

    private void Awake()
    {
        DefineSingleton(this);
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _player.Wand.AddEventListener(PlayerWand.SpellCast, OnPlayerSpellCast, true);
    }

    public void Register(StressingNpc npc)
    {
        _npcs.Add(npc);
    }

    public void UnleashTheChaos()
    {
        foreach (var stressingNpc in _npcs)
        {
            stressingNpc.IsStressing = true;
        }
    }

    public void CalmDownThePlebs()
    {
        foreach (var stressingNpc in _npcs)
        {
            stressingNpc.IsStressing = false;
        }
    }

    private void OnPlayerSpellCast(EventObject eventObject)
    {
        UnleashTheChaos();
    }
}
