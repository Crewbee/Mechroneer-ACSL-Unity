//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;
//using UnityEngine;

//public class HealthUI : MonoBehaviour
//{
//    private GameObject Robot;
//    private HealthComponentOld RobotHealthComp;

//    //Most recent damage taken
//    private float CurrentHealthValue = 1.0f;
//    private float LastHealthValue = 1.0f;

//    private bool StartUp = false;
//    private float duration = 2.0f;

//    private float HealthStartTime;

//    //References to bars managed; currentbars are ones that display the current status while growingbar displays the growing effect
//    private Image currentHealthBar;
//    private Image growingHealthBar;


//    [Header("Unity Stuff")]
//    //References to actual bars
//    public Image HealthBar;
//    public Image HealthBarDamage;

//    // Start is called before the first frame update
//    void Start()
//    {
//        HealthStartTime = Time.time;
//        currentHealthBar = HealthBar;
//        growingHealthBar = HealthBarDamage;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //Very first update get our references so we get the instantiated game objects and not the prefabs
//        if (StartUp == false)
//        {
//            if (GetComponentInParent<HealthComponentOld>())
//            RobotHealthComp = GetComponentInParent<HealthComponentOld>();

//            StartUp = true;
//        }

//        //Update our time remaining for both values
//        float healthTime = (Time.time - HealthStartTime) / duration;

//        //Have our stats changed? if so return whether we're healing and change the bars we'll visibly change
//        switch (HasHealthChanged(currentHealthBar))
//        {
//            case -1:
//                currentHealthBar = HealthBar;
//                growingHealthBar = HealthBarDamage;
//                break;

//            case 1:
//                currentHealthBar = HealthBarDamage;
//                growingHealthBar = HealthBar;
//                break;
//        }

//        //Run different functions based on whether we last took or healed damage
//        UpdateHealthBar(healthTime);
//    }

//    //Returns -1 if damage was taken, 1 if damage was gained, and 0 if nothing has changed
//    int HasHealthChanged(Image image)
//    {
//        int result = 0;
//        if (LastHealthValue != image.fillAmount)
//        {
//            if (LastHealthValue > image.fillAmount)
//                result = -1;

//            if (LastHealthValue < image.fillAmount)
//                result = 1;

//            LastHealthValue = image.fillAmount;
//            HealthStartTime = Time.time;
//            return result;
//        }

//        return result;
//    }

//    void UpdateHealthBar(float time)
//    {
//        currentHealthBar.fillAmount = RobotHealthComp.ReturnHealthPercentage();
//        growingHealthBar.fillAmount = Mathf.SmoothStep(CurrentHealthValue, currentHealthBar.fillAmount, time);
//        CurrentHealthValue = growingHealthBar.fillAmount;
//    }

//}
