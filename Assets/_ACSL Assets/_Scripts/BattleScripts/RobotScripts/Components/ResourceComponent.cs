using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[DefaultExecutionOrder(-10)]
public class ResourceComponent : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    private float startingCurrentValue;
    [SerializeField]
    private float startingMaxValue;

    public float regenerationAmount;
    public float regenerationStartDelay;
    //How fast you 
    public float regenerationRate;
    public bool stopRegenOnResourceUsed;

    public float currentValue { get; protected set; }
    public float maxValue { get; protected set; }
    public float percent { get => currentValue / maxValue; }

    private bool m_hasInit;

    public delegate void OnResourceValueChanged(float amount);
    public event OnResourceValueChanged onResourceUsed;

    public delegate void OnResourceValueReachLimit();
    public event OnResourceValueReachLimit onResourceReachesZero;
    public event OnResourceValueReachLimit onResourceReachesMax;

    private float m_accumulatedTime;

    // Start is called before the first frame update
    void Awake()
    {
        m_accumulatedTime = 0;
        if (m_hasInit)
            return;
        currentValue = startingCurrentValue;
        maxValue = startingMaxValue;
    }

    public void Init(float maxValue, float currentValue = -1)
    {
        if (currentValue < 0)
        {
            this.currentValue = this.maxValue = maxValue;
        }
        else
        {
            this.currentValue = currentValue;
            this.maxValue = maxValue;
        }
    }

    public void UseResource(float amount)
    {
        currentValue -= amount;
        onResourceUsed?.Invoke(amount);

        if (stopRegenOnResourceUsed && m_accumulatedTime > 0 && amount > 0)
            m_accumulatedTime = 0;
        if (currentValue < 0)
        {
            currentValue = 0;
            onResourceReachesZero?.Invoke();
        }
        else if (currentValue > maxValue)
        {
            currentValue = maxValue;
            onResourceReachesMax?.Invoke();
        }
    }

    private void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            if (!photonView.IsMine)
                return;
        }
        Regenerate();
    }

    private void Regenerate()
    {
        if (currentValue < maxValue && regenerationAmount != 0)
        {

            while (m_accumulatedTime >= regenerationStartDelay + regenerationRate && currentValue < maxValue)
            {
                m_accumulatedTime -= regenerationRate;
                UseResource(-regenerationAmount);
            }

            m_accumulatedTime += Time.deltaTime;
        }
        else
        {
            m_accumulatedTime = 0;
        }
    }

    public void AdjustMaxValue(float adjustedValue, Adjust_Value adjustCurrentValue)
    {
        switch (adjustCurrentValue)
        {
        case Adjust_Value.None:
            maxValue = adjustedValue;
            if (currentValue > maxValue)
                currentValue = maxValue;
            break;
        case Adjust_Value.Additive:
            float amountToAdd = adjustedValue - maxValue;
            maxValue = adjustedValue;
            currentValue += amountToAdd;
            break;
        case Adjust_Value.Percentage:
            float percent = this.percent;
            maxValue = adjustedValue;
            currentValue = maxValue * percent;
            break;
        default:
            break;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentValue);
            stream.SendNext(maxValue);
        }
        else if (stream.IsReading)
        {
            currentValue = (float)stream.ReceiveNext();
            maxValue = (float)stream.ReceiveNext();
        }
    }

    public enum Adjust_Value
    {
        None,
        Additive,
        Percentage
    }
}
