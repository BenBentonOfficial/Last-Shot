using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneChange : MonoBehaviour
{
    public Animator anim;
    private void Start()
    {
        Time.timeScale = 0;
        StartCoroutine(LoadMenu());
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("TestLevel");
    }

    private IEnumerator LoadMenu()
    {
        anim.enabled = false;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        anim.enabled = true;
       
    }
}
