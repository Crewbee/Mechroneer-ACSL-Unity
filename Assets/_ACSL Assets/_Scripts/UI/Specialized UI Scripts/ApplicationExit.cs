﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationExit : MonoBehaviour
{
    public void SafeShutdown()
    {
        Application.Quit();
    }
}
