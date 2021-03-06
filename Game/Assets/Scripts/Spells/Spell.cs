﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : ISpell
{
    public bool IsCasting { get; set; }

    protected float HeatEffect; // Range[-1, 1], effect on heat per cast
    protected float Cooldown; // Cooldown in seconds

    private float _secondsSinceLastCast = float.MinValue;

    public virtual bool Cast(ISpellCaster spelCaster)
    {
        if (Time.time - _secondsSinceLastCast >= Cooldown && TemperatureManager.Instance != null)
        {
            TemperatureManager.Instance.AddTemperature(HeatEffect);
            _secondsSinceLastCast = Time.time;

            return true;
        }

        return false;
    }

    public virtual void StopCasting(ISpellCaster spellCaster)
    {
        
    }
}
