using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;

    public TextField nameTextField;

    public UnityEngine.UI.Button startButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() { }

    public void StartNew()
    {

        DataManager.Instance.SaveName(nameTextField.text);
        SceneManager.LoadScene(1);
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
}
