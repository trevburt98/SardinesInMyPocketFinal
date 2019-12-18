using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseGameBehaviour : MonoBehaviour
{
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
         Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            print("p key was pressed");
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenu.SetActive(true);
        }
    }
    public void resume() {
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenu.SetActive(false);
    }


}
