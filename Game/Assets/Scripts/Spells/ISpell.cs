using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpell
{
    bool IsCasting { get; set; }

    bool Cast(ISpellCaster spellCaster);
    void StopCasting(ISpellCaster spellCaster);
}
