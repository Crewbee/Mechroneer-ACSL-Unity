using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using Photon.Pun;

public class ChangeScene : MonoBehaviour
{

    public Animator sceneTransition;
    public void ChangeToScene(int i)
    {
        //if (PhotonNetwork.IsConnected)
        {
            //if (PhotonNetwork.IsMasterClient)
            //{
                //sceneTransition.SetTrigger("Start");
                //StartCoroutine(PhotonLoadLevelAsync(i));
            //}
            //else
            //{
            //    StartCoroutine(LoadLevelAsync(i));
            //}
        }
        //else
        {
            StartCoroutine(LoadLevelAsync(i));
        }
    }

    //IEnumerator PhotonLoadLevelAsync(int buildIndex)
    //{
    //    LoadingScreen._instance.Display();
    //    PhotonNetwork.LoadLevel(buildIndex);

    //    while (PhotonNetwork.LevelLoadingProgress < 1)
    //    {
    //        //LoadingScreen.Instance.UpdateProgressDisplay(PhotonNetwork.LevelLoadingProgress);
    //        LoadingScreen._instance.loadingProgress = PhotonNetwork.LevelLoadingProgress;
    //        yield return new WaitForEndOfFrame();
    //    }
    //}

    IEnumerator LoadLevelAsync(int buildIndex)
    {
        LoadingScreen._instance.Display();
        AsyncOperation async = SceneManager.LoadSceneAsync(buildIndex);

        while (async.progress < 1)
        {
            //LoadingScreen.Instance.UpdateProgressDisplay(async.progress);
            LoadingScreen._instance.loadingProgress = async.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
