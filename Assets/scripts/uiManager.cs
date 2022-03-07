using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class uiManager : MonoBehaviour
{
    [SerializeField] public Button[] LevelButtons;
    [SerializeField] public Button ButtomMusic;
    [SerializeField] public GameObject MenuInicial, MenuFases,TelaDetalhes, MenuLosing;
    [SerializeField] public int menu = 0, QuantidadeTelas,faseSelecionada = 0;
    [SerializeField] public bool pause = false;
    [SerializeField] public GameObject[] TelasFases;


    [SerializeField] public GameObject MenuWinLose, gamemenu;
    [SerializeField] public Button NextLevel, Menubtn;

    //=================================================================================
    internal void losingBottom()
    {
        Time.timeScale = 0;
        Menubtn.interactable = false;
        MenuLosing.SetActive(true);
    }


    public void continueGame()
    {
        MonetizationManager.inst.aparecendoAnuncio = false;
        MonetizationManager.inst.iaiaiai();
        StartCoroutine("Wait");
        Menubtn.interactable = true;
        MenuLosing.SetActive(false);

    }


    public void Resume()
    {
            Time.timeScale = 1;
            StartCoroutine("Wait");
            Menubtn.interactable = true;
            MenuLosing.SetActive(false);
    }


    internal void losed()
    {
        Menubtn.interactable = false;
        NextLevel.interactable = false;
        txtwin.text = "Lose!";
        nexLvl.text = "Current level: "+levelManager.inst.nivelAtual;
        txtDica.text = levelManager.inst.nivel[levelManager.inst.nivelAtual - 1].desc;
        MenuWinLose.SetActive(true);
        GameManager.inst.win = false;
    }

    public void Restart()
    {
        if (GameManager.inst.win)
        {
            levelManager.inst.nivelAtual--;
            GameManager.inst.lvll = levelManager.inst.nivel[levelManager.inst.nivelAtual - 1];
        }

        time.text = "Time; 0s";
        GameManager.inst.Time = 0f;
        GameManager.inst.Score = 0;
        
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);

    }

    public void mainMenu()
    {
        pauseGame();
        Menubtn.interactable = !pause;
        gamemenu.SetActive(pause);
        if(pause == false)
        {
            StartCoroutine("Wait");
        }
        else
        {
            GameManager.inst.podeJogar = false;
        }
        
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

    internal void morte() {
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

    [SerializeField] public Text detalhes, numFase;



    [SerializeField] public Text Score, time,nexLvl,txtwin, txtDica;
    [SerializeField] GameObject startbtn;

    internal void oncloseAdsScene() {
        GameManager.inst.lvll = levelManager.inst.nivel[levelManager.inst.nivelAtual - 1];
        GameManager.inst.lvll = levelManager.inst.nivel[levelManager.inst.nivelAtual - 1];
        Restart();
    }

    public void AddScore()
    {
        GameManager.inst.AddScore();
        Score.text = "Score: " + GameManager.inst.Score;
    }
    public void Startgame()
    {
        startbtn?.SetActive(false);
        StartCoroutine("Wait");
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

        //Restart();

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.inst.podeJogar = true;
    }

    public void OpenMenuFases()
    {
        int n = levelManager.inst.nivelMaisAltoTerminado;
        foreach (GameObject tF in TelasFases)
        {
            LevelButtons = tF.GetComponentsInChildren<Button>();
            foreach (Button btn in LevelButtons)
            {
                if (n >= 0)
                {
                    btn.interactable = true;
                }
                else
                {
                    btn.interactable = false;
                }
                n--;
            }
        }

        TelasFases[Mathf.Abs(menu)].SetActive(true);

        MenuInicial.SetActive(false);
        MenuFases.SetActive(true);
    }
    private void Start()
    {
        changeTextBtnSound();
    }
    public void ShowDetalhes(int n)
    {
        faseSelecionada = n;
        if (n < 10)
        {
            numFase.text = "0" + n;
        }
        else
        {
            numFase.text = n.ToString();
        }
        detalhes.text = levelManager.inst.nivel[n-1].desc;
        TelaDetalhes.SetActive(true);
    }

    public void returnMenuFases()
    {
        faseSelecionada = 0;
        TelaDetalhes.SetActive(false);
    }
    public void returnMainMenu()
    {
        MenuFases.SetActive(false);
        MenuInicial.SetActive(true);
    }

    public void LoadMainMenu()
    {
        GameManager.inst.Time = 0f;
        GameManager.inst.Score = 0;
        SceneManager.LoadScene("Main");
    }
    public void NextTF()
    {

        foreach (GameObject TF in TelasFases)
        {
            TF.SetActive(false);
        }
        menu++;
        menu = menu % QuantidadeTelas;
        TelasFases[Mathf.Abs(menu)].SetActive(true);
    }

    public void prevTF()
    {
        foreach (GameObject TF in TelasFases)
        {
            TF.SetActive(false);
        }
        menu--;
        menu = menu % QuantidadeTelas;
        TelasFases[Mathf.Abs(menu)].SetActive(true);
    }

    public void LoadNivel()
    {
        levelManager.inst.nivelAtual = faseSelecionada;
        GameManager.inst.lvll = levelManager.inst.nivel[faseSelecionada - 1];
        SceneManager.LoadScene("game");
    }

    public void Quit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        if (time != null)
        {
            time.text ="Time: " + (int)GameManager.inst.Time +" s";
        }
    }

    public void MuteUnmute()
    {
        SoundManager.inst.mute();
        changeTextBtnSound();
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
}
