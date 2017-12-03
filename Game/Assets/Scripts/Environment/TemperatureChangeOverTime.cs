using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureChangeOverTime : MonoBehaviour
{
    public bool Enabled = true;

    [Range(-1, 1)][SerializeField] private float _temperatureChangePerSecond;
	
	private void Update () {
		if (Enabled && TemperatureManager.Instance != null)
            TemperatureManager.Instance.AddTemperature(_temperatureChangePerSecond * Time.deltaTime, true);
	}
}
