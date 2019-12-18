using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinWall : MonoBehaviour
{

    Score scoreObj;
    private bool open;

    // Start is called before the first frame update
    void Start()
    {
        open = false;
        scoreObj = GameObject.Find("Score").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        open = scoreObj.gameOver;
        if (open)
        {
            print("Open Door!");
            gameObject.GetComponent<Transform>().localPosition = new Vector3(100f, -18f, -13.31f);
        }
        else
        {
            gameObject.GetComponent<Transform>().localPosition = new Vector3(-13.52f, 6.6985f, -13.31f);
        }
    }
}
