using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiGame : MonoBehaviour
{
    [SerializeField] public Button ButtomMusic;
    [SerializeField] public GameObject MenuLosing;
    [SerializeField] public bool pause = false;
    [SerializeField] public GameObject MenuWinLose, gamemenu;
    [SerializeField] public Button NextLevel, Menubtn;
    [SerializeField] public Text Score, time, nexLvl, txtwin, txtDica;

    

    private void Start()
    {
        changeTextBtnSound();
    }
    public void mainMenu()
    {
        pauseGame();
        Menubtn.interactable = !pause;
        gamemenu.SetActive(pause);
        if (pause == false)
        {
            StartCoroutine(Wait());
        }
        else
        {
            GameManager.inst.podeJogar = false;
        }
    }

    public void continueGame()
    {
        Menubtn.interactable = true;
        MenuLosing.SetActive(false);
        StartCoroutine("Wait");
        MonetizationManager.inst.aparecendoAnuncio = true;
        MonetizationManager.inst.iaiaiai();
    }

    void pauseGame()
    {
        if (pause)
        {
            pause = !pause;
            Time.timeScale = 1;
        }
        else
        {
            pause = !pause;
            Time.timeScale = 0;
        }
    }
    public void AddScore()
    {
        GameManager.inst.AddScore();
        Score.text = "Score: " + GameManager.inst.Score;
    }
    public void Restart()
    {
        if (GameManager.inst.win)
        {
            levelManager.inst.nivelAtual--;
            GameManager.inst.lvll = levelManager.inst.nivel[levelManager.inst.nivelAtual - 1];
        }

        time.text = "Time: 0s";
        GameManager.inst.Time = 0f;
        GameManager.inst.Score = 0;

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
    public void nextLevel()
    {
        MonetizationManager.inst.aparecendoAnuncio = true;
        MonetizationManager.inst.iaiaiai();
        GameManager.inst.podeJogar = false;
        GameManager.inst.lvll = levelManager.inst.nivel[levelManager.inst.nivelAtual - 1];
        time.text = "Time; 0s";
        Score.text = "Score: 0";
        GameManager.inst.Time = 0f;
        GameManager.inst.Score = 0;
        SceneManager.LoadScene("game");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.inst.podeJogar = true;
    }
    public void LoadMainMenu()
    {
        GameManager.inst.Time = 0f;
        GameManager.inst.Score = 0;
        SceneManager.LoadScene("Main");
    }

    public void MuteUnmute()
    {
        SoundManager.inst.mute();
        changeTextBtnSound();
    }

    void Update()
    {
        if (time != null)
        {
            time.text = "Time: " + (int)GameManager.inst.Time + " s";
        }
    }

    private void changeTextBtnSound()
    {
        if (SoundManager.inst.isMuted())
        {
            ButtomMusic.GetComponentInChildren<Text>().text = "Sound: off";
        }
        else
        {
            ButtomMusic.GetComponentInChildren<Text>().text = "Sound: on";
        }
    }
    internal void morte()
    {
        if (!levelManager.inst.isLastLevel())
        {
            Menubtn.interactable = false;
            levelManager.inst.ChangeNewLevel();
            NextLevel.interactable = true;
            txtDica.text = levelManager.inst.nivel[levelManager.inst.nivelAtual - 1].desc;
            nexLvl.text = "Next Level: " + levelManager.inst.nivelAtual;
        }
        else
        {
            Menubtn.interactable = false;
            nexLvl.text = "The End :)";
            txtDica.text = "";
            NextLevel.interactable = false;
        }
        txtwin.text = "Win!!";
        MenuWinLose.SetActive(true);
        GameManager.inst.win = true;
    }
    internal void losed()
    {
        Menubtn.interactable = false;
        NextLevel.interactable = false;
        txtwin.text = "Lose!";
        nexLvl.text = "Current level: " + levelManager.inst.nivelAtual;
        txtDica.text = levelManager.inst.nivel[levelManager.inst.nivelAtual - 1].desc;
        MenuWinLose.SetActive(true);
        GameManager.inst.win = false;
    }
    internal void losingBottom()
    {
        Time.timeScale = 0;
        Menubtn.interactable = false;
        MenuLosing.SetActive(true);
    }
}
