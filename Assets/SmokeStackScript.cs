using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeStackScript : MonoBehaviour
{
    public List<ParticleSystem> smokeStacks;

    // Start is called before the first frame update
    void Start()
    {
        //foreach (ParticleSystem stack in smokeStacks)
        //{
        //    ParticleSystem.MainModule main = stack.main;
        //    main.loop = false;
        //}
    }

    public void PlaySmokeStacks()
    {
        foreach (ParticleSystem stack in smokeStacks)
        {
            stack.Play();
            stack.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        }
    }
}
