using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;

public class EnergyComponent : ResourceComponent//, IPunObservable
{
    public bool HasEnoughEnergy(float amount)
    {
        return currentValue >= amount;
    }

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    //    if (stream.IsWriting)
    //    //    {
    //    //        stream.SendNext(energy);
    //    //        stream.SendNext(m_MaxEnergy);
    //    //    }
    //    //    else if (stream.IsReading)
    //    //    {
    //    //        energy = (float)stream.ReceiveNext();
    //    //        m_MaxEnergy = (float)stream.ReceiveNext();
    //    //    }
    //}
}
