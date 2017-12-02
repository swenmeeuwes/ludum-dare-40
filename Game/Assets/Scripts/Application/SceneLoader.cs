using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoSingleton<SceneLoader>
{
    public AsyncOperation CurrentAsyncOperation;

    // todo change to work with method name
    public void LoadNextAsync()
    {
        // TEST METHOD
        SceneManager.LoadScene(SceneLiterals.LoadScreen, LoadSceneMode.Additive);

        CurrentAsyncOperation = SceneManager.LoadSceneAsync("Level 1");
        CurrentAsyncOperation.allowSceneActivation = false;

        CurrentAsyncOperation.completed += completedAsyncOperation => completedAsyncOperation.allowSceneActivation = true;
    }
}
