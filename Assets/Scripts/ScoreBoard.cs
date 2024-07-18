using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public int score;
    TMP_Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "Score:0";
    }

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        scoreText.text = $"Score:{score.ToString()}";
    }
}
