using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScene : MonoBehaviour
{

    [SerializeField] Slider _loadingBar;
    [SerializeField] TextMeshProUGUI _loadingText;

    void Start()
    {
        _loadingBar.value = 0;
        _loadingBar.maxValue = 1;
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);

        asyncOperation.allowSceneActivation = false;

        _loadingBar.value = asyncOperation.progress / 0.9f;

        float fakeProgress = 0;

        while (!asyncOperation.isDone && fakeProgress < 0.99f)
        {
            fakeProgress = Mathf.Lerp(fakeProgress, asyncOperation.progress / 0.9f, 0.5f);

            _loadingBar.value = fakeProgress;
            _loadingText.text = "Loading..." + (int)(fakeProgress * 100) + "%";

            yield return new WaitForSeconds(0.25f);
        }

        asyncOperation.allowSceneActivation = true;

    }
}
