using TMPro;
using UnityEngine;

//<summary> For now, this script just handles keeping track of the Score. - Zander :3 </summary>
public class GameManager : MonoBehaviour
{
    public int currentScore = 0;
    public static int highScore = 0;

    public TextMeshProUGUI currentScoreDisplay; //Calling these "Displays" instead of text so its not currentScoreText.text
    public TextMeshProUGUI highScoreDisplay;
    public TextMeshProUGUI gameOverScoreDisplay;

    private void Start()
    {
        highScoreDisplay.text = $"Best: {highScore}";
    }

    private void UpdateScore()
    {
        currentScoreDisplay.text = $"Score: {currentScore}";
        highScoreDisplay.text = $"Best: {highScore}";
        gameOverScoreDisplay.text = $"Score: {currentScore}";
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        if (currentScore > highScore) highScore = currentScore;

        UpdateScore();
    }
}