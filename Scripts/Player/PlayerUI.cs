using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private TextMeshProUGUI deathText;
    [SerializeField] private TextMeshProUGUI winText;
    private bool pause = false;
    public GameObject pauseMenu;
    public GameObject playerUICanvas;

    // Start is called before the first frame update
    void Start()
    {
        playerUICanvas.SetActive(true);
        deathText.enabled = false;
        winText.enabled = false;
    }

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }

    public void PauseGame() 
    {
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pause = !pause;
        pauseMenu.SetActive(pause);
    }

    public void ContinueGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pause = !pause;
        pauseMenu.SetActive(pause);
    }

    public void Win()
    {
        winText.enabled = true;
    }

    public void Die()
    {
        deathText.enabled = true;
    }

}
