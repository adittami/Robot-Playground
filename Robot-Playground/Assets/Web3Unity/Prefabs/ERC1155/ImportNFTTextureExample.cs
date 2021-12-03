using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ImportNFTTextureExample : MonoBehaviour
{
    public InputField ContractAddressInput;
    public InputField TokenIDInput;

    public bool CustomNFT = false;
    public bool CheckingBalance = false;
    public class Response {
        public string image;
    }

    async void Start()
    {
        if (!CustomNFT)
        {
            //from tutorial
            //contract: 0x3a8A85A6122C92581f590444449Ca9e66D8e8F35
            //tokenId: 5

            //from https://docs.opensea.io/docs/3-viewing-your-items-on-opensea
            //contract: 0x7dca125b1e805dC88814aeD7ccc810f677d3E1DB
            //tokenId: 12

            //from https://testnets.opensea.io/assets/0x88b48f654c30e99bc2e4a1559b4dcf1ad93fa656/9821907875876459826970335187971642635874725110019031877339021421615215804417
            //contract: 0x88B48F654c30e99bc2e4A1559b4Dcf1aD93FA656
            //tokenId: 9821907875876459826970335187971642635874725110019031877339021421615215804417

            string chain = "ethereum";
            string network = "mainnet";
            string contract = "0x7dca125b1e805dC88814aeD7ccc810f677d3E1DB";   
            string tokenId = "12";   

            // fetch uri from chain
            string uri = await ERC1155.URI(chain, network, contract, tokenId);
            print("uri: " + uri);

            // fetch json from uri
            UnityWebRequest webRequest = UnityWebRequest.Get(uri);
            await webRequest.SendWebRequest();
            Response data = JsonUtility.FromJson<Response>(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));

            // parse json to get image uri
            string imageUri = data.image;
            print("imageUri: " + imageUri);

            // fetch image and display in game
            UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(imageUri);
            await textureRequest.SendWebRequest();
            this.gameObject.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)textureRequest.downloadHandler).texture;
        }
    }

    public void NFTTexturePopulate()
    {
        GetCustomNFTtexture();
    }

    async void GetCustomNFTtexture()
    {
        
        string chain = "ethereum";
        string network = "rinkeby";
        string contract = ContractAddressInput.GetComponent<InputField>().text;
        string tokenId = TokenIDInput.GetComponent<InputField>().text;
        string account = PlayerPrefs.GetString("Account");

        // fetch uri from chain
        string uri = await ERC1155.URI(chain, network, contract, tokenId);
        print("uri: " + uri);

        // fetch json from uri
        UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        await webRequest.SendWebRequest();
        Response data = JsonUtility.FromJson<Response>(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));

        // parse json to get image uri
        string imageUri = data.image;
        print("imageUri: " + imageUri);

        // fetch image and display in game
        UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(imageUri);
        await textureRequest.SendWebRequest();

        BigInteger balanceOf = await ERC1155.BalanceOf(chain, network, contract, account, tokenId);
        print(balanceOf);

        if (CheckingBalance)
        {
            if (balanceOf > 0)
            {
                this.gameObject.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)textureRequest.downloadHandler).texture;
            }
        }
        else
            this.gameObject.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)textureRequest.downloadHandler).texture;
    }
}
