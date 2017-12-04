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
        _boss.AddEventListener(Boss.Died, OnBossDied, true);
    }

    private void OnTutorialFinished(EventObject eventObject)
    {
        Active = true;
        StartSequence();
    }

    private void OnBossDied(EventObject eventObject)
    {
        StopCoroutine("HandleSequence");
        Time.timeScale = 0.1f;

        Invoke("NextLevel", 0.1f); // 0.1 / 0.1 (timeScale) = 1
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

    private void NextLevel()
    {
        Time.timeScale = 1f;

        if (SceneLoader.Instance != null)
            SceneLoader.Instance.LoadNextAsync();
    }
}
