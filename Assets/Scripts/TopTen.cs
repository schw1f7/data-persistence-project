using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopTen : MonoBehaviour
{
    public TextMeshProUGUI topTenScoresText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DataManager.Instance.LoadSave();
        var top10Scores = DataManager.Instance.GetTop10Scores();
        for (var i = 1; i < top10Scores.Count + 1; i++)
        {
            var score = top10Scores[i - 1];
            topTenScoresText.text += $"#{i} {score.Name} : {score.Score}\n";
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }
}
