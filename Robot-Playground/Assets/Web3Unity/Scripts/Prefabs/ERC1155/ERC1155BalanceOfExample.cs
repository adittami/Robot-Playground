using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ERC1155BalanceOfExample : MonoBehaviour
{
    public GameObject Sphere;
    async void Start()
    {
        string chain = "ethereum";
        string network = "rinkeby";
        string contract = "0x88B48F654c30e99bc2e4A1559b4Dcf1aD93FA656";
        string account = PlayerPrefs.GetString("Account");
        string tokenId = "9821907875876459826970335187971642635874725110019031877339021421615215804417";

        BigInteger balanceOf = await ERC1155.BalanceOf(chain, network, contract, account, tokenId);
        print(balanceOf);

        if (balanceOf > 0)
        {
            Sphere.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
    }
}
