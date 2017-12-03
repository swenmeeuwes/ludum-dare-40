using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing,
    GameOver
}

public class GameManager : MonoSingleton<GameManager>
{
    private GameState _state = GameState.Playing;
    public GameState State {
        get { return _state; }
        set
        {
            if (value == GameState.GameOver)
                StartCoroutine(GameOver());

            _state = value;
        }
    }

    private void Awake()
    {
        DefineSingleton(this);
    }

    private IEnumerator GameOver()
    {
        // todo: do stuff?

        yield return new WaitForSeconds(2f);

        // Restart
        SceneLoader.Instance.ReloadCurrentSceneAsync();
    }
}
