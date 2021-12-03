using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScript : MonoBehaviour
{
    public GameObject Controller;
    private void OnEnable()
    {
        Debug.Log("in con check");
        Invoke("off", 0.5f);
    }
    public void off()
    {
        Debug.Log("in con check as");
        Controller.SetActive(false);
    }
}
