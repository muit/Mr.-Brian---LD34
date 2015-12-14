using UnityEngine;
using Crab.Controllers;

public class GameOver : SceneScript
{
    protected override void OnGameStart(PlayerController player)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        AudioListener.pause = false;
        Time.timeScale = 1;
        Console console = FindObjectOfType<Console>();
        console.Write("GAME OVER", 75);
        console.Write("Hope you liked this small LudumDare34 Compo game.", 75);
        console.Write("Your feedback is highly appreciated.", 100);
        console.Write("@muitxer", 100);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            FindObjectOfType<Menu>().Play(1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            FindObjectOfType<Menu>().Play(0);
        }
    }
}
