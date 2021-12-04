using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseAvatar : MonoBehaviour
{
    public Dropdown dropdown;
    public CustomImportNFT updatePlayerProfile;

    // Start is called before the first frame update
    void Start()
    {
        dropdown.GetComponent<Dropdown>();

        dropdown.options.Clear();

        List<string> items = new List<string>();

        for (int i = 1; i<55; i++)
        {
            items.Add(i.ToString());
        }

        foreach(string item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData { text = item });
        }

        dropdown.onValueChanged.AddListener(delegate { DropItemSelected(dropdown); });
    }

    void DropItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        updatePlayerProfile.tokenId = dropdown.options[index].text;
        updatePlayerProfile.ChangeAvatar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
