using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBeamSpell : Spell
{
    private IceBeam _activeIceBeam = null;
    private float _secondsSinceLastCast = float.MinValue;

    public IceBeamSpell()
    {
        HeatEffect = -0.1f;
        Cooldown = 1f;
    }

    public override bool Cast(ISpellCaster spellCaster)
    {
        // Don't call base.Cast since we have channel spell which only goes on cooldown once stopped casting
        if (Time.time - _secondsSinceLastCast < Cooldown || TemperatureManager.Instance == null)
            return false;

        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);        

        if (_activeIceBeam == null)
        {
            var iceBeamPrefab = PrefabLocator.Instance.Locate(Prefab.IceBeam);
            var iceBeamObject = MonoBehaviour.Instantiate(iceBeamPrefab, spellCaster.Transform.position, Quaternion.identity);

            if (spellCaster.Transform.name == "Player")
                iceBeamObject.layer = (int)Layers.PlayerSpells;

            var mouseIceBeamDelta = (mouseWorldPosition - iceBeamObject.transform.position).normalized;
            var rotationAngle = Mathf.Atan(mouseIceBeamDelta.y / mouseIceBeamDelta.x) * Mathf.Rad2Deg;
            iceBeamObject.transform.localRotation = Quaternion.Euler(Vector3.forward * rotationAngle);

            _activeIceBeam = iceBeamObject.GetComponent<IceBeam>();
        }
        else
        {

            var iceBeamObject = _activeIceBeam.gameObject;

            var mouseIceBeamDelta = (mouseWorldPosition - iceBeamObject.transform.position).normalized;            
            var rotationAngle = Mathf.Atan(mouseIceBeamDelta.y / mouseIceBeamDelta.x) * Mathf.Rad2Deg;
            iceBeamObject.transform.localRotation = Quaternion.AngleAxis(mouseIceBeamDelta.x > 0 ? rotationAngle : rotationAngle + 180, Vector3.forward);
            iceBeamObject.transform.position = spellCaster.Transform.position;
        }

        TemperatureManager.Instance.AddTemperature(HeatEffect * Time.deltaTime);

        return true;
    }

    public override void StopCasting(ISpellCaster spellCaster)
    {
        if (_activeIceBeam == null)
            return;

        _secondsSinceLastCast = Time.time;
        _activeIceBeam.CleanUp();
        _activeIceBeam = null;
    }
}
