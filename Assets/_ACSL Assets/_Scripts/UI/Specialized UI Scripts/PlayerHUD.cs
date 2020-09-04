//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;
//using UnityEngine;
//using TMPro;

////TODO: Remake this

//public class PlayerHUD : MonoBehaviour
//{
//    //public BattleSceneManager BattleSceneManager;
//    public MechroneerPlayer mechroneerPlayer;

//    //Most recent damage taken
//    private float CurrentHealthValue = 1.0f;
//    private float LastHealthValue = 1.0f;

//    private float CurrentEnergyValue = 1.0f;
//    private float LastEnergyValue = 1.0f;

//    //private bool StartUp = false;
//    private float duration = 2.0f;

//    private float HealthStartTime;
//    private float EnergyStartTime;

//    //References to bars managed; currentbars are ones that display the current status while growingbar displays the growing effect
//    private Image currentHealthBar;
//    private Image currentEnergyBar;

//    private Image growingHealthBar;
//    private Image growingEnergyBar;


//    [Header("Unity Stuff")]
//    //References to actual bars
//    public Image HealthBar;
//    public Image HealthBarDamage;
//    public Image EnergyBar;
//    public Image EnergyBarDamage;
//    public TextMeshProUGUI HealthText;
//    public TextMeshProUGUI EnergyText;
//    public TextMeshProUGUI AimingText;
//    public Image AimingPanel;
//    public Image AimTarget1;
//    public Image AimTarget2;


//    public GameObject LeftButton;
//    public GameObject RightButton;
//    public GameObject BodyButton;
//    public GameObject LegButton;

//    public GameObject RobotStack;

//    public TextMeshProUGUI LeftArmEnergyText;
//    public TextMeshProUGUI RightArmEnergyText;
//    public TextMeshProUGUI BodyEnergyText;
//    public TextMeshProUGUI LegEnergyText;

//    public Robot currentRobot;

//    [Header("Left Arm")]
//    public Image LeftArmSkillIcon;
//    public Image LeftArmSkillRing;
//    public TextMeshProUGUI LeftArmPercent;
//    [Header("Right Arm")]
//    public Image RightArmSkillIcon;
//    public Image RightArmSkillRing;
//    public TextMeshProUGUI RightArmPercent;
//    [Header("Body")]
//    public Image BodySkillIcon;
//    public Image BodySkillRing;
//    public TextMeshProUGUI BodyPercent;
//    [Header("Leg")]
//    public Image LegSkillIcon;
//    public Image LegSkillRing;
//    public TextMeshProUGUI LegPercent;

//    private RobotLeg leg;
//    private RobotBody body;
//    private RobotArm leftArm;
//    private RobotArm rightArm;

//    private bool hudInstantiated = false;

//    // Start is called before the first frame update
//    void Start()
//    {
//        HealthStartTime = Time.time;
//        EnergyStartTime = Time.time;

//        currentHealthBar = HealthBar;
//        currentEnergyBar = EnergyBar;
//        growingHealthBar = HealthBarDamage;
//        growingEnergyBar = EnergyBarDamage;
//    }


//    // Update is called once per frame
//    void Update()
//    {
//        if (mechroneerPlayer)
//        {
//            if (mechroneerPlayer.m_abilityToFire != RobotPartType.Head)
//            {
//                AimingText.alpha = 1.0f;
//                AimingPanel.color = new Color(0.1294118f, 0.1294118f, 0.1294118f, 0.5f);
//                AimTarget1.color = Color.white;
//                AimTarget2.color = Color.white;
//            }
//            else
//            {
//                AimingText.alpha = 0.0f;
//                AimingPanel.color = Color.clear;
//                AimTarget1.color = Color.clear;
//                AimTarget2.color = Color.clear;
//            }

//            if (mechroneerPlayer.currentRobot)
//            {

//                //Update our time remaining for both values
//                float healthTime = (Time.time - HealthStartTime) / duration;
//                float energyTime = (Time.time - EnergyStartTime) / duration;

//                //Have our stats changed? if so return whether we're healing and change the bars we'll visibly change
//                switch (HasHealthChanged(currentHealthBar))
//                {
//                case -1:
//                    currentHealthBar = HealthBar;
//                    growingHealthBar = HealthBarDamage;
//                    break;

//                case 1:
//                    currentHealthBar = HealthBarDamage;
//                    growingHealthBar = HealthBar;
//                    break;
//                }

//                if (currentEnergyBar != null)
//                {
//                    switch (HasEnergyChanged(currentEnergyBar))
//                    {
//                    case -1:
//                        currentEnergyBar = EnergyBar;
//                        growingEnergyBar = EnergyBarDamage;
//                        break;

//                    case 1:
//                        currentEnergyBar = EnergyBarDamage;
//                        growingEnergyBar = EnergyBar;
//                        break;
//                    }
//                }

//                //Run different functions based on whether we last took or healed damage
//                UpdateHealthBar(healthTime);

//                if (currentEnergyBar != null)
//                    UpdateEnergyBar(energyTime);
//            }
//        }
//    }

//    public void PlayerLegAbility()
//    {
//        if (mechroneerPlayer)
//        {
//            if (mechroneerPlayer.currentRobot)
//                mechroneerPlayer.SetAbilityToFire(RobotPartType.Leg);
//        }
//    }

//    public void PlayerBodyAbility()
//    {
//        if (mechroneerPlayer)
//        {
//            if (mechroneerPlayer.currentRobot)
//                mechroneerPlayer.SetAbilityToFire(RobotPartType.Body);
//        }
//    }

//    public void LeftArmAbility()
//    {
//        if (mechroneerPlayer)
//        {
//            if (mechroneerPlayer.currentRobot)
//                mechroneerPlayer.SetAbilityToFire(RobotPartType.LeftArm);
//        }
//    }

//    public void RightArmAbility()
//    {
//        if (mechroneerPlayer)
//        {
//            if (mechroneerPlayer.currentRobot)
//                mechroneerPlayer.SetAbilityToFire(RobotPartType.RightArm);
//        }
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

//    int HasEnergyChanged(Image image)
//    {
//        int result = 0;
//        if (LastEnergyValue != image.fillAmount)
//        {
//            if (LastEnergyValue > image.fillAmount)
//                result = -1;

//            if (LastEnergyValue < image.fillAmount)
//                result = 1;

//            LastEnergyValue = image.fillAmount;
//            EnergyStartTime = Time.time;
//            return result;
//        }

//        return result;
//    }

//    void UpdateHealthBar(float time)
//    {
//        HealthComponentOld PlayerHealthComp = mechroneerPlayer.currentRobot.GetComponent<RobotOld>().healthComponent;
//        currentHealthBar.fillAmount = PlayerHealthComp.ReturnHealthPercentage();
//        growingHealthBar.fillAmount = Mathf.SmoothStep(CurrentHealthValue, currentHealthBar.fillAmount, time);
//        if (HealthText != null)
//        {
//            HealthText.text = PlayerHealthComp.health.ToString();
//        }
//        CurrentHealthValue = growingHealthBar.fillAmount;
//    }

//    void UpdateEnergyBar(float time)
//    {
//        EnergyComponent PlayerEnergyComp = mechroneerPlayer.currentRobot.GetComponent<RobotOld>().energyComponent;
//        currentEnergyBar.fillAmount = PlayerEnergyComp.ReturnEnergyPercentage();
//        growingEnergyBar.fillAmount = Mathf.SmoothStep(CurrentEnergyValue, currentEnergyBar.fillAmount, time);
//        if (EnergyText != null)
//        {
//            EnergyText.text = PlayerEnergyComp.energy.ToString();
//        }
//        CurrentEnergyValue = growingEnergyBar.fillAmount;
//    }

//    void ChangeImageTransparency(GameObject obj, float transparency)
//    {
//        Image[] img = obj.GetComponentsInChildren<Image>();
//        foreach (Image image in img)
//        {
//            Color col = image.color;
//            col.a = transparency;

//            image.color = col;
//        }
//    }
//}
