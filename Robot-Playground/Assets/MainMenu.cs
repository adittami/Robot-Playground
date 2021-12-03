using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MainMenuPannel,LevelSelPannel,LoadingPannel,NextButton,NotEnoughMoney,ShopPannel,CoinPannel;
    public GameObject[] Button,PlayersSel,PlayerObj,BuyButton;
    public Text Coins1, Coin2;
    public static int PlayerNumber=0;
    public static bool Next,SoundGame=true,AdCheck=true;
    public AudioSource audio1, audio2;
    public AudioClip Bt;
    public static int Count=0;
    public static MainMenu ins;
    public GameObject[] remove_ads;
    public GameObject[] unlock_levels;
    public GameObject[] unlock_robots;
    public GameObject[] unlock_everything;
    void Start()
    {
        if (!PlayerPrefs.HasKey("ADSUNLOCK"))
        {
            PlayerPrefs.SetInt("ADSUNLOCK", 0);
        }
        if (!PlayerPrefs.HasKey("everything"))
        {
            PlayerPrefs.SetInt("everything", 0);
        }
        if (PlayerPrefs.GetInt("ADSUNLOCK") == 1 && PlayerPrefs.GetInt("unlock_robots") == 5
            && PlayerPrefs.GetInt("Level") == 15 && PlayerPrefs.GetInt("LevelComplete") == 15)
        {
            PlayerPrefs.SetInt("everything", 1);
        }

        
        Time.timeScale = 1;
        ins = this;
        Count++;
        if (PlayerPrefs.GetInt("everything") == 1)
        {
            open_panels();
        }
        else
        {
            if (Count == 1)
            {
                ShopPannel.SetActive(true);
            }
            if (Count == 2)
            {
                Count = 0;
            }
        }
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("TotalCoins", 1000);

        //PlayerPrefs.SetInt("Buy" + 0, 1);
        if (PlayerPrefs.GetInt("everything") == 1)
        {
            foreach (var item in unlock_everything)
            {
                if (item != null)
                {
                    item.SetActive(false);
                }
            }
        }
        
        if (PlayerPrefs.GetInt("ADSUNLOCK")==1)
        {
            foreach (var item in remove_ads)
            {
                if (item != null)
                {
                    item.SetActive(false);
                }
            }
        }
        if (PlayerPrefs.GetInt("unlock_robots") == 5)
        {
            foreach (var item in unlock_robots)
            {
                if (item != null)
                {
                    item.SetActive(false);
                }
            }
        }
        if (PlayerPrefs.GetInt("Level") == 15)
        {
            foreach (var item in unlock_levels)
            {
                if (item != null)
                {
                    item.SetActive(false);
                }
            }
        }
        Coins1.text = "" + PlayerPrefs.GetInt("TotalCoins");
        Coin2.text = "" + PlayerPrefs.GetInt("TotalCoins");
        for (int i = 1; i < Button.Length; i++)
        {
            if (i <= PlayerPrefs.GetInt("LevelComplete"))
            {
                Button[i].GetComponent<Button>().enabled = true;
                Button[i].transform.GetChild(2).gameObject.SetActive(false);
                Button[i].transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.white;
            }
        }
        if(Next)
        {
            Next = false;
            MainMenuPannel.SetActive(false);
            LevelSelPannel.SetActive(true);
        }
        //AdsScript.Instance.ShowBannerAtTop();
        show_banner();
        if (PlayerPrefs.GetInt("LevelComplete") == 15)
        {
            UnlockAll();
        }
    }
    public void open_panels()
    {
        if (PlayerPrefs.GetInt("everything") == 1)
        {
            ShopPannel.SetActive(false);
            foreach (var item in unlock_everything)
            {
                if (item != null)
                {
                    item.SetActive(false);
                }
            }
            foreach (var item in remove_ads)
            {
                if (item != null)
                {
                    item.SetActive(false);
                }
            }
            foreach (var item in unlock_robots)
            {
                if (item != null)
                {
                    item.SetActive(false);
                }
            }
            foreach (var item in unlock_levels)
            {
                if (item != null)
                {
                    item.SetActive(false);
                }
            }
            UnlockAll();
        }
    }
    public void OpenChrPannel()
    {
        PlayersSel[PlayerNumber].SetActive(true);
        PlayerObj[PlayerNumber].SetActive(true);
        show_intersial();
    } public void CloseChrPannel()
    {
        PlayersSel[PlayerNumber].SetActive(false);
        PlayerObj[PlayerNumber].SetActive(false);
    }
    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.fg.real.robot.ring.fighting");
    }   public void PrivacyPolice()
    {
        Application.OpenURL("https://sites.google.com/view/fighting-gamerz/privacy-policy");
    }
    public void PlayAudio()
    {
        audio1.PlayOneShot(Bt);
    }
    public void MoreApps()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Fighting+Gamerz");
    } 
    public void UnlockAll()
    {
        PlayerPrefs.SetInt("LevelComplete", 15);
        for (int i = 1; i < Button.Length; i++)
        {
            if (i <= PlayerPrefs.GetInt("LevelComplete"))
            {
                Button[i].GetComponent<Button>().enabled = true;
                Button[i].transform.GetChild(2).gameObject.SetActive(false);
                Button[i].transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.white;
            }
        }
    }
    public void NextChar()
    {
        if(PlayerNumber!=5)
        {
            NotEnoughMoney.SetActive(false);
            PlayersSel[PlayerNumber].SetActive(false);
            PlayerObj[PlayerNumber].SetActive(false);
            BuyButton[PlayerNumber].SetActive(false);
            PlayerNumber++;
            PlayersSel[PlayerNumber].SetActive(true);
            PlayerObj[PlayerNumber].SetActive(true);
            if(PlayerPrefs.GetInt("Buy"+ PlayerNumber)==1)
            {
                BuyButton[PlayerNumber].SetActive(false);
            }
            else
            {
                BuyButton[PlayerNumber].SetActive(true);
                //NextButton.SetActive(false);
            }
        }
    }public void PrevChar()
    {
        if (PlayerNumber != 0)
        {
            NotEnoughMoney.SetActive(false);
            PlayersSel[PlayerNumber].SetActive(false);
            PlayerObj[PlayerNumber].SetActive(false);
            BuyButton[PlayerNumber].SetActive(false);
            PlayerNumber--;
            PlayersSel[PlayerNumber].SetActive(true);
            PlayerObj[PlayerNumber].SetActive(true);
            if (PlayerPrefs.GetInt("Buy" + PlayerNumber) == 1)
            {
                BuyButton[PlayerNumber].SetActive(false);
            }
            else
            {
                BuyButton[PlayerNumber].SetActive(true);
                //NextButton.SetActive(false);
            }
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void SelectLevel(int a)
    {
        PlayerPrefs.SetInt("Level", a);    
        LoadingPannel.SetActive(true);
        Invoke("PlayGame", 1.5f);
        //AdsScript.Instance.admobInterstitial();
        //SceneManager.LoadScene("Gameplay");
        show_intersial();
    }
    public void AddCoin(int a)
    {
        PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") + a);
        Coins1.text = "" + PlayerPrefs.GetInt("TotalCoins");
        Coin2.text = "" + PlayerPrefs.GetInt("TotalCoins");
    }
    public void BuyChar(int a)
    {
        if(PlayerPrefs.GetInt("TotalCoins")>=a)
        {
            PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - a);
            PlayerPrefs.SetInt("Buy" + PlayerNumber, 1);
            Coins1.text = "" + PlayerPrefs.GetInt("TotalCoins");
            Coin2.text = "" + PlayerPrefs.GetInt("TotalCoins");
            BuyButton[PlayerNumber].SetActive(false);
        }
        else
        {
            NotEnoughMoney.SetActive(false);
            NotEnoughMoney.SetActive(true);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void InvNotEnoughMoney()
    {
        Invoke("NotEnoughMoneyPanne", 1f);
    }public void NotEnoughMoneyPanne()
    {
        NotEnoughMoney.SetActive(false);
    }
    public void SoundOff()
    {
        SoundGame = false;
        audio2.enabled = false;
        //AudioListener.volume = 0;
    }public void SoundOn()
    {
        SoundGame = true;
        audio2.enabled = true;
        //AudioListener.volume = 1;
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
