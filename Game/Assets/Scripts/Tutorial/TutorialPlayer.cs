using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPlayer : MonoBehaviour
{
    [SerializeField] private TypeWriter _tutorialTextField;
    [SerializeField] private Text _continueTextField;
    [SerializeField] private float _tutorialDelay;
    [SerializeField] private TemperatureUIController _temperatureUiController;
    [SerializeField] private Image _exitArrow;
    [SerializeField] private Spawner _fireballSpawner;

    [Tooltip("Time in seconds before the tutorial starts")] public TutorialItem[] Sequence;

    public bool IsFinished { get; set; }

    public TutorialItem CurrentSequenceItem
    {
        get { return Sequence[_sequenceIndex]; }
    }

    public TutorialItem NextSequenceItem
    {
        get
        {
            _sequenceIndex++;
            return _sequenceIndex > Sequence.Length - 1 ? null : CurrentSequenceItem;
        }
    }

    private Player _player;
    private int _sequenceIndex;

    private void Awake()
    {
        _sequenceIndex = -1;
        _continueTextField.enabled = false;
        _exitArrow.enabled = false;
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _player.Movement.enabled = false;
        _player.Wand.enabled = false;

        _temperatureUiController.Hide(true);

        _tutorialTextField.AddEventListener(TypeWriter.Finished, OnTypeWriterFinished);

        Invoke("Next", _tutorialDelay);
    }

    private void OnDestroy()
    {
        _tutorialTextField.RemoveEventListener(TypeWriter.Finished, OnTypeWriterFinished);
    }

    private void Update()
    {
        if (IsFinished)
            return;

        if (Input.anyKeyDown)
        {
            if (_tutorialTextField.IsWriting)
                _tutorialTextField.Finish();
            else
                Next();            
        }
    }

    private void Next()
    {        
        if (_sequenceIndex + 1 > Sequence.Length - 1 && !IsFinished)
        {
            StartCoroutine(Finish());
            return;
        }

        _continueTextField.enabled = false;
        HandleTutorialItem(NextSequenceItem);
    }

    private IEnumerator Finish()
    {
        IsFinished = true;        

        _continueTextField.enabled = false;
        _tutorialTextField.GetComponent<Text>().enabled = false;
        
        _exitArrow.enabled = true;

        yield return new WaitForSeconds(0.5f);

        _player.Movement.enabled = true;
        _player.Wand.enabled = true;
    }

    private void HandleTutorialItem(TutorialItem tutorialItem)
    {
        _tutorialTextField.Type(PlayerPrefStringResolver.Instance.Resolve(tutorialItem.Text));

        switch (tutorialItem.Action)
        {
            case TutorialItemActionType.ShowTemperatureMeter:
                _temperatureUiController.Hide(false);
                break;

            case TutorialItemActionType.CameraShake:
                CameraManager.Instance.Shake(0.2f);
                break;

            case TutorialItemActionType.SpawnFireball:
                _fireballSpawner.Spawn();
                _fireballSpawner.Invoke("Spawn", 0.3f);
                _fireballSpawner.Invoke("Spawn", 0.6f);
                break;

            case TutorialItemActionType.Mayhem:
                NpcManager.Instance.UnleashTheChaos();
                break;

            case TutorialItemActionType.None:
            default:
                break;
        }
    }

    private void OnTypeWriterFinished(EventObject eventObject)
    {
        _continueTextField.enabled = true;
    }
}
