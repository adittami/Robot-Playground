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
    public class Response {
        public string image;
    }

    async void Start()
    {
        /*
        string chain = "ethereum";
        string network = "rinkeby";
        string contract = "0x3a8A85A6122C92581f590444449Ca9e66D8e8F35";
        string tokenId = "5";

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
        */
    }

    public void NFTTexturePopulate()
    {
        GetNFTtexture();
    }

    async void GetNFTtexture()
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

        if (balanceOf > 0)
        {
            this.gameObject.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)textureRequest.downloadHandler).texture;
        }
    }
}
