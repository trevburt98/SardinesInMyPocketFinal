using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public int score = 0;
    public bool gameOver = false;
    public int winAmount = 15;

    [SerializeField]
    private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        print(score);
        scoreText.text = score.ToString();
        if(score >= winAmount)
        {
            gameOver = true;
        }
    }
}
