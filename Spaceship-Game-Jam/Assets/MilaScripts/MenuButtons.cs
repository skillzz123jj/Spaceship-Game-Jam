using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuButtons : MonoBehaviour
{
    bool pauseMenuisOn;
    [SerializeField] GameObject pauseMenu;
    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuisOn)
        {
            ContinueGame();

        }else if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenuisOn)
        {
          PauseGame();

        }
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        pauseMenuisOn = false;
        pauseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuisOn = true;
        pauseMenu.SetActive(true);
    }

    public void BackToMenu(int scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);

    }
}
