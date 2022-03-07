using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiMenu : MonoBehaviour
{
    [SerializeField] public Button[] LevelButtons;
    [SerializeField] public Button ButtomMusic;
    [SerializeField] public GameObject MenuInicial, MenuFases, TelaDetalhes;
    [SerializeField] public int menu = 0, QuantidadeTelas, faseSelecionada = 0;
    [SerializeField] public GameObject[] TelasFases;
    [SerializeField] public Text detalhes, numFase;
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
    public void Quit()
    {
        Application.Quit();
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
        detalhes.text = levelManager.inst.nivel[n - 1].desc;
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
}
