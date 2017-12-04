using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossSequence : MonoBehaviour
{
    [SerializeField] private TutorialPlayer _tutorialPlayer;
    [SerializeField] private Boss _boss;
    [SerializeField] private float _intervalInSeconds;

    public bool Active = false;

    private void Start()
    {
        _tutorialPlayer.AddEventListener(TutorialPlayer.Finished, OnTutorialFinished, true);
    }

    private void OnTutorialFinished(EventObject eventObject)
    {
        Active = true;
        StartSequence();
    }

    private void StartSequence()
    {
        StartCoroutine(HandleSequence());
    }

    private IEnumerator HandleSequence()
    {
        while (Active)
        {
            yield return new WaitForSeconds(_intervalInSeconds);

            var randomIndex = Random.Range(0, 2);
            _boss.CurrentPhase = (BossPhase) Enum.GetValues(typeof(BossPhase)).GetValue(randomIndex);

            _boss.BeginCasting();
        }
    }
}
