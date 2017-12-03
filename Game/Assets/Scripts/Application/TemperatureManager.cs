using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureManager : MonoSingleton<TemperatureManager>
{
    [Tooltip("Scales from 0 to 1, where 0 is cold and 1 hot")][Range(0, 1)][SerializeField] private float _temperature;
    [SerializeField] private bool _ignoreTemperatureInThisScene = false;
    
    public float Temperature {
        get { return _temperature; }
    }

    private void Awake()
    {
        DefineSingleton(this);
    }

    private void Update()
    {
        if (_ignoreTemperatureInThisScene)
        {
            _temperature = 0.5f;
            return;
        }

        if (_temperature < 0.01f && GameManager.Instance.State == GameState.Playing)
            OnAbsoluteZero();

        if (_temperature > 0.99f && GameManager.Instance.State == GameState.Playing)
            OnBoilingPoint();
    }

    public void SetTemperature(float newTemperature, bool instant = false)
    {
        if (instant)
        {
            _temperature = newTemperature;
            return;
        }

        // todo: replace by coroutine?
        iTween.ValueTo(gameObject, iTween.Hash(
                "from", _temperature,
                "to", newTemperature,
                "time", 0.6f,
                "onupdatetarget", gameObject,
                "onupdate", "TemperatureTweenOnUpdateCallBack",
                "easetype", iTween.EaseType.easeInOutCirc
            )
        );
    }

    public void AddTemperature(float addition, bool instant = false)
    {
        var targetTemperature = Mathf.Clamp01(_temperature + addition);

        if (instant)
        {
            _temperature = targetTemperature;
            return;
        }

        // todo: replace by coroutine?
        iTween.ValueTo(gameObject, iTween.Hash(
                "from", _temperature,
                "to", targetTemperature,
                "time", 0.6f,
                "onupdatetarget", gameObject,
                "onupdate", "TemperatureTweenOnUpdateCallBack",
                "easetype", iTween.EaseType.easeInOutCirc
            )
        );
    }

    private void TemperatureTweenOnUpdateCallBack(float newValue)
    {
        _temperature = newValue;        
    }

    private void OnAbsoluteZero()
    {
        GameManager.Instance.State = GameState.GameOver;

        var iceBlockPrefab = PrefabLocator.Instance.Locate(Prefab.IceBlock);
        var player = FindObjectOfType<Player>();
        player.Wand.StopCastingAll();
        player.Movement.enabled = false;
        player.Wand.enabled = false;
        player.GetComponent<Rigidbody2D>().Sleep();

        var iceBlockObject = Instantiate(iceBlockPrefab, player.transform.position, Quaternion.identity);
        var iceBlock = iceBlockObject.GetComponent<IceBlock>();
        iceBlock.Holding = player.gameObject;
    }

    private void OnBoilingPoint()
    {
        GameManager.Instance.State = GameState.GameOver;

        var firePrefab = PrefabLocator.Instance.Locate(Prefab.Fire);
        var player = FindObjectOfType<Player>();
        player.Hit();
        player.Wand.StopCastingAll();
        player.Movement.enabled = false;
        player.Wand.enabled = false;
        player.GetComponent<Rigidbody2D>().Sleep();

        var fireObject = Instantiate(firePrefab, player.transform.position, Quaternion.identity);
    }
}
