using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("MainScene_1");
    }

    public void OnMainButtonClick()
    {
        SceneManager.LoadScene("Title");
    }


    public void OnExitButtonClick()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
