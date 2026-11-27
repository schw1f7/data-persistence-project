using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;

    public TMP_InputField nameTextField;

    public Button startButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadData();
    }

    public void StartNew()
    {
        DataManager.Instance.SetName(nameTextField.text);
        SceneManager.LoadScene(1);
    }

    public void GoToTopTen()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        // TODO: Save current name

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// Load save data to populate best score and last used name.
    /// </summary>
    private void LoadData()
    {
        DataManager.Instance.LoadSave();

        // Show best score, if there is one
        var bestScore = DataManager.Instance.GetHighScore();

        var baseBestScoreText = "Best Score: ";
        bestScoreText.text = baseBestScoreText;
        if (bestScore != null)
        {
            bestScoreText.text += $"{bestScore.Name} : {bestScore.Score}";
        }

        var lastUsedName = DataManager.Instance.GetName();
        if (!string.IsNullOrEmpty(lastUsedName))
        {
            nameTextField.text = lastUsedName;
        }
    }
}
