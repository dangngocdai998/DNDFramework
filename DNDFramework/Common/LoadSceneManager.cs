using System;
using System.Collections;
using System.Collections.Generic;
using DNDFramework.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : SingletonMonoBehaviour<LoadSceneManager>
{
    #region LoadSceneAsync
    // private IEnumerator WaitLoadAsyncScene(string name, Action completed, bool closeFade)
    // {
    //     Debug.Log("Scene " + name + " is loading...");
    //     /* if (String.Compare(name, houseScene, false) == 0 && String.Compare(houseScene, SceneManager.GetActiveScene().name, false) == 0)
    //     {
    //         TransitionFX.Instance.FakeRunProgress();
    //         completed?.Invoke();

    //     }
    //     else
    //     { */
    //     // CanvasManager.ClearAllUI();
    //     PoolingManager.Instance.DisableAllObject();
    //     EventDispatcher.ClearAllListener();
    //     CustomInput.ResetInput();

    //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
    //     while (!asyncLoad.isDone)
    //     {
    //         TransitionFX.Instance.ChangeValueProgress(asyncLoad.progress);

    //         yield return null;
    //     };
    //     completed?.Invoke();
    //     TransitionFX.Instance.ChangeValueProgress(1);
    //     // }

    //     /*  if (closeFade && TransitionFX.Exists())
    //      {
    //          yield return new WaitForSecondsRealtime(2f);
    //          TransitionFX.I.EndFade();
    //      } */

    //     Debug.Log("Scene " + name + " is loaded!!!");
    // }
    #endregion

    public void LoadScene(string name, Action completed, bool isEndTransition = true)
    {
        TransitionFX.Instance.StartFade(() => { StartCoroutine(WaitLoadAsyncScene(name, completed, isEndTransition)); });
    }

    private IEnumerator WaitLoadAsyncScene(string name, Action completed, bool isEndTransition)
    {
        Debug.Log("Scene " + name + " is loading...");
        CanvasManager.ClearAllUI();
        PoolingManager.Instance.DisableAllObject();
        EventDispatcher.ClearAllListener();
        CustomInput.ResetInput();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
        while (!asyncLoad.isDone)
        {
            // TransitionFX.Instance.ChangeValueProgress(asyncLoad.progress);

            yield return null;
        };
        completed?.Invoke();
        if (isEndTransition)
            if (TransitionFX.Exists())
            {
                yield return new WaitForSecondsRealtime(0.5f);
                TransitionFX.Instance.EndFade();
            }

        Debug.Log("Scene " + name + " is loaded!!!");
    }

}
