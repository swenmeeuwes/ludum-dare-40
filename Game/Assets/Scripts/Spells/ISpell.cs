﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpell
{
    bool Cast(ISpellCaster spellCaster);
}
