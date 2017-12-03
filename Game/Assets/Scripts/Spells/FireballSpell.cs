using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpell : Spell {
    public FireballSpell()
    {
        HeatEffect = 0.05f;
        Cooldown = 0.5f;
    }

    public override bool Cast(ISpellCaster spellCaster)
    {
        var didCast = base.Cast(spellCaster);
        if (!didCast) return false;        

        // todo: Shoot dat fireball        
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var fireballPrefab = PrefabLocator.Instance.Locate(Prefab.Fireball);
        var fireballObject = MonoBehaviour.Instantiate(fireballPrefab, spellCaster.Transform.position + mouseWorldPosition.normalized, Quaternion.identity);
        if (spellCaster.Transform.tag == "Player")
            fireballObject.layer = (int)Layers.HotPlayerSpells;

        var direction = (mouseWorldPosition - spellCaster.Transform.position).normalized;
        direction.z = 0;

        var fireballRigidbody2D = fireballObject.GetComponent<Rigidbody2D>();
        fireballRigidbody2D.velocity = direction * 50f;        

        return true;
    }
}
