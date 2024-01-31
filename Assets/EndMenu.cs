using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public static bool winState;
    public GameObject winText;
    public GameObject loseText;
    private void Start()
    {
        if(winState == true)
        {
            winText.SetActive(true);
            loseText.SetActive(false);
        } else
        {
            winText.SetActive(false);
            loseText.SetActive(true);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
}
