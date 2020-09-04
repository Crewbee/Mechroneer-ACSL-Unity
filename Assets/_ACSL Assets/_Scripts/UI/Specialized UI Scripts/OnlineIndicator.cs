using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class OnlineIndicator : MonoBehaviour
{
    [Header("Indicator Setitngs")]
    public Image IndicatorImage;
    public TextMeshProUGUI IndicatorText;
    public Color OnlineColor = Color.green;
    public Color OfflineColor = Color.red;
    public string OnlineText = "Online";
    public string OfflineText = "Offline";
    [Header("Update Settings")]
    public float UpdateFrequency = 10f;

    //Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateNetworkStatus());

        IndicatorImage.color = OfflineColor;
        IndicatorText.color = OfflineColor;
        IndicatorText.text = OfflineText;
    }

    IEnumerator UpdateNetworkStatus()
    {
        StartCoroutine(NetworkManager._instance.CheckInternetConnection((isOnline) =>
        {
            if (isOnline)
            {
                // Online
                IndicatorImage.color = OnlineColor;
                IndicatorText.color = OnlineColor;
                IndicatorText.text = OnlineText;
            }
            else
            {
                // Offline
                IndicatorImage.color = OfflineColor;
                IndicatorText.color = OfflineColor;
                IndicatorText.text = OfflineText;
            }
        }));

        yield return new WaitForSeconds(UpdateFrequency);
    }
}
