using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffGameObj : MonoBehaviour
{
    public GameObject touch;
    private void OnDisable()
    {
        Invoke("Off", 0.01f);
        
    }
    public void Off()
    {
        touch.SetActive(false);
    }
}
