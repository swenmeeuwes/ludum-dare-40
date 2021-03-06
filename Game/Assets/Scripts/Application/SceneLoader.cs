﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoSingleton<SceneLoader>
{
    public AsyncOperation CurrentAsyncOperation;

    private void Awake()
    {
        DefineSingleton(this);
    }
    
    public void LoadNextAsync()
    {
        var activeScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(SceneLiterals.LoadScreen, LoadSceneMode.Additive);

        CurrentAsyncOperation = SceneManager.LoadSceneAsync(activeScene.buildIndex + 1);
        CurrentAsyncOperation.allowSceneActivation = false;

        CurrentAsyncOperation.completed += completedAsyncOperation => completedAsyncOperation.allowSceneActivation = true;
    }

    public void ReloadCurrentSceneAsync()
    {
        var activeScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(SceneLiterals.LoadScreen, LoadSceneMode.Additive);

        CurrentAsyncOperation = SceneManager.LoadSceneAsync(activeScene.buildIndex);
        CurrentAsyncOperation.allowSceneActivation = false;

        CurrentAsyncOperation.completed += completedAsyncOperation => completedAsyncOperation.allowSceneActivation = true;
    }
}
