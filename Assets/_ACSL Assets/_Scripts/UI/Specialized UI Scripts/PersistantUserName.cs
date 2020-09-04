using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PersistantUserName : MonoBehaviour
{
    public TextMeshProUGUI text;
    private void OnEnable()
    {
        text.text = PhotonNetwork.LocalPlayer.NickName;    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
