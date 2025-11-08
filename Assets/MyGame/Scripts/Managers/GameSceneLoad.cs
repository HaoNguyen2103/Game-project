using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneLoad : MonoBehaviour
{
    public void OnClickExit()
    {
        Application.Quit();
    }
    public void OnClickButtonOne(string Level)
    {
        SceneManager.LoadScene(Level);
    }
    public void OnClickButtonTwo(string Level)
    {
        SceneManager.LoadScene(Level);
    }
    public void OnClickButtonThree(string Level)
    {
        SceneManager.LoadScene(Level);
    }
}
