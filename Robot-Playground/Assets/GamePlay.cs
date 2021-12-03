using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GamePlay : MonoBehaviour
{

    public GameObject[] Levels,Environment,Player;
    public GameObject Enemy,E1,E2,P1,P2;
    public static GamePlay i;
    public static int Count = 0, WinCounter=0,FailCounter = 0;
    public GameObject LevelCompeltePannel, PausePannel, LevelFailPannel,TouchControlPannel, Round1Pannel, Round2Pannel, FinalRoundPannel, FightPannel,YouWin,YouLose,CountDownPannel,C1,C2,C3;
    UIManager UI;
    public AudioSource Source;
    public AudioClip CountDown,Win,Fail,Buttonsound;
    private void Awake()
    {
        i = this;
      
        AudioListener.volume=1;
        Time.timeScale = 1f;
      //  Enemy = GameObject.FindGameObjectWithTag("Enemy");
         UI = GameObject.FindObjectOfType<UIManager>();
        // UI.UI_fader.Fade(UIFader.FADE.FadeOut, 1f, 0);
        Debug.Log("test count " + Count);
        Levels[PlayerPrefs.GetInt("Level")].SetActive(true);
        Player[MainMenu.PlayerNumber].SetActive(true);
        if (Count==0)
        {
            Environment[0].SetActive(true);
            Round1Pannel.SetActive(true);
        }
        else if (Count==1)
        {
            Environment[1].SetActive(true);
            Round2Pannel.SetActive(true);
            if(WinCounter==1)
            {
                P1.SetActive(true);
            }if(FailCounter == 1)
            {
                E1.SetActive(true);
            }
        }
        else if(Count==2)
        {
            Environment[2].SetActive(true);
            FinalRoundPannel.SetActive(true);
            if (WinCounter == 1)
            {
                P1.SetActive(true);
            }
            if (FailCounter == 1)
            {
                E1.SetActive(true);
            }
        }
        //AdsScript.Instance.ShowBannerAtTop();
    }
    private void Start()
    {
        hide_banner();
        show_banner();
    }
    public void TouchPannelOff()
    {
        Debug.Log("int touch off");
        TouchControlPannel.SetActive(false);
    }
    public void LevelComplete()
    {
        if(PlayerPrefs.GetInt("LevelComplete")== PlayerPrefs.GetInt("Level"))
        {
            PlayerPrefs.SetInt("LevelComplete", PlayerPrefs.GetInt("Level") + 1);
        }
        PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins")+1000);
        UI.UI_fader.Fade(UIFader.FADE.FadeIn, .5f, .3f);
        //AdsScript.Instance.CompletedAds();
        UI.ShowMenu("LevelComplete");
        Debug.Log("level win ");
        show_intersial();
    }  
    public void LevelFail()
    {
        UI.UI_fader.Fade(UIFader.FADE.FadeIn, .5f, .3f);
        //AdsScript.Instance.FailedAds();
        UI.ShowMenu("GameOver");
        Debug.Log("level fail ");
        show_intersial();
    }
    public void WinUpdate()
    {
        WinCounter++;
       if(WinCounter==2)
        {
            P2.SetActive(true);
            WinCounter = 0;
            Count = 0;
          //  Source.PlayOneShot(Win);
            Invoke("LevelComplete", 0.1f);
        }
       else
        {
            Debug.Log("next round ");
            P1.SetActive(true);
            UI.DisableAllScreens();
            
            StartCoroutine(RestartLevel());
        }
    }
    IEnumerator RestartLevel()
    {
        
        yield return new WaitForSeconds(1f);

        //load level
        UI.UI_fader.Fade(UIFader.FADE.FadeIn, .5f, .3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void FailUpdate()
    {
        FailCounter++;
       if(FailCounter == 2)
        {
            FailCounter = 0;
            Count = 0;
            E2.SetActive(true);
          //  Source.PlayOneShot(Fail);
            Invoke("LevelFail", 0.1f);
        }
       else
        {
            Debug.Log("next round fail ");
            E1.SetActive(true);
            UI.DisableAllScreens();

            StartCoroutine(RestartLevel());
        }
    }
    public void OnAnimCompletRound1()
    {
        Round1Pannel.SetActive(false);
        StartCoroutine(FightSoundEffect());
    }
    public void OnAnimCompletRound2()
    {
        Round2Pannel.SetActive(false);
        StartCoroutine(FightSoundEffect());
    }public void OnAnimCompletFinalRound()
    {
        FinalRoundPannel.SetActive(false);
        StartCoroutine(FightSoundEffect());
    }
    IEnumerator FightSoundEffect()
    {
        float time = 0.8f;
        CountDownPannel.SetActive(true);
        C3.SetActive(true);
        Source.PlayOneShot(CountDown);
        yield return new WaitForSeconds(time);
        C3.SetActive(false);
        C2.SetActive(true);
        Source.PlayOneShot(CountDown);
        yield return new WaitForSeconds(time);
        C2.SetActive(false);
        C1.SetActive(true);
        Source.PlayOneShot(CountDown);
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
        yield return new WaitForSeconds(time);
        C1.SetActive(false);
        CountDownPannel.SetActive(false);
        FightPannel.SetActive(true);
    }
   public void OnTouchControlPannel()
    {
        FightPannel.SetActive(false);
        TouchControlPannel.SetActive(true);
        Enemy.GetComponent<EnemyAI>().enabled = true;
    }
    public void RestartGame()
    {
        Count = 0; WinCounter = 0; FailCounter = 0;
        Source.PlayOneShot(Buttonsound);
        SceneManager.LoadScene("Gameplay");
    }public void ResumeGame()
    {
        PausePannel.SetActive(false);
        Source.PlayOneShot(Buttonsound);
        Time.timeScale = 1f;
    }
    public void Home()
    {
        hide_banner();
        Count = 0; WinCounter = 0; FailCounter = 0;
        Source.PlayOneShot(Buttonsound);
        SceneManager.LoadScene("MainMenu");
    }
    public void Next()
    {
        hide_banner();
        Count = 0; WinCounter = 0; FailCounter = 0;
        Source.PlayOneShot(Buttonsound);
        SceneManager.LoadScene("MainMenu");
        MainMenu.Next = true;
    }
    public void PasueGame()
    {
        show_intersial();
        PausePannel.SetActive(true);
        Source.PlayOneShot(Buttonsound);
        //AdsScript.Instance.PausedAds();
        Time.timeScale = 0.01f;
    }
    public void show_banner()
    {

    }
    public void hide_banner()
    {

    }
    public void show_intersial()
    {

    }
}
