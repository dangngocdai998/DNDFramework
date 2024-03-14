using System;
using System.Collections;
using System.Collections.Generic;
using DNDFramework.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : SingletonMonoBehaviour<LoadSceneManager>
{
    public const string sceneMenu = "Menu";
    public const string sceneSpl = "Spl";
    public const string sceneGamePlay = "GamePlay";




    #region LoadSceneAsync
    private IEnumerator WaitLoadAsyncScene(string name, Action completed, bool closeFade)
    {
        Debug.Log("Scene " + name + " is loading...");
        /* if (String.Compare(name, houseScene, false) == 0 && String.Compare(houseScene, SceneManager.GetActiveScene().name, false) == 0)
        {
            TransitionFX.Instance.FakeRunProgress();
            completed?.Invoke();

        }
        else
        { */
        // CanvasManager.ClearAllUI();
        PoolingManager.Instance.DisableAllObject();
        EventDispatcher.ClearAllListener();
        CustomInput.ResetInput();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
        while (!asyncLoad.isDone)
        {
            TransitionFX.Instance.ChangeValueProgress(asyncLoad.progress);

            yield return null;
        };
        completed?.Invoke();
        TransitionFX.Instance.ChangeValueProgress(1);
        // }

        /*  if (closeFade && TransitionFX.Exists())
         {
             yield return new WaitForSecondsRealtime(2f);
             TransitionFX.I.EndFade();
         } */

        Debug.Log("Scene " + name + " is loaded!!!");
    }
    #endregion

    public void LoadScene(string name, Action completed)
    {
        TransitionFX.Instance.StartFade(() => { StartCoroutine(WaitLoadAsyncScene(name, completed)); });
    }

    private IEnumerator WaitLoadAsyncScene(string name, Action completed)
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

        if (TransitionFX.Exists())
        {
            yield return new WaitForSecondsRealtime(0.5f);
            TransitionFX.Instance.EndFade();
        }

        Debug.Log("Scene " + name + " is loaded!!!");
    }

}
