using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public bool useSystemTime = false;

    public Light sun;
    public Light moon;

    public float currentTimeOfDay;
    public float currentTimeOfYear;
    public float timeScale;

    private float sunInitialIntensity;
    private float sunInitialIndirect;
    public Gradient sunColorGradient;

    private float moonInitialIntensity;
    private float moonInitialIndirect;
    public Gradient moonColorGradient;

    private readonly int secondsInFullDay = 86400;
    private System.DateTime currentSystemTime;
    private float currentTimeInSeconds;

    private void Start()
    {
        if (sun.type != LightType.Directional || moon.type != LightType.Directional)
        {
            Debug.LogError("LightType != LightType.Directional!");
        }

        sunInitialIntensity = sun.intensity;
        sunInitialIndirect = sun.bounceIntensity;

        moonInitialIntensity = moon.intensity;
        moonInitialIndirect = moon.bounceIntensity;
    }

    private void Update()
    {
        // Assign current system time
        currentSystemTime = System.DateTime.Now;
        currentTimeInSeconds = (float)currentSystemTime.TimeOfDay.TotalSeconds;

        // Update planetoids
        UpdateSun();
        UpdateMoon();

        // Recalculate the time of day
        if (useSystemTime)
        {
            currentTimeOfDay = (currentTimeInSeconds / secondsInFullDay); // System Time
            currentTimeOfYear = (currentTimeInSeconds / (secondsInFullDay * 365)); // System Year
        }
        else
        {
            currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeScale; // Game Time
            currentTimeOfYear += (Time.deltaTime / (secondsInFullDay * 365)) * timeScale; // Game Year
        }

        // Loop time of day
        if (currentTimeOfDay >= 1f)
        {
            currentTimeOfDay = 0f;
        }
    }

    private void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, (currentTimeOfYear * 360f) - 90, 0);

        float intensityMultiplier = 1;
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            // Night
            intensityMultiplier = 0;
        }
        else if (currentTimeOfDay <= 0.25f)
        {
            // Sunrise
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        else if (currentTimeOfDay >= 0.73f)
        {
            // Sunset
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
        sun.bounceIntensity = sunInitialIndirect * intensityMultiplier;
        sun.color = sunColorGradient.Evaluate(currentTimeOfDay);
    }

    private void UpdateMoon()
    {
        moon.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 270, -(currentTimeOfYear * 360f) - 90, 0);

        float intensityMultiplier = 0;
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            // Day
            intensityMultiplier = 1;
        }
        else if (currentTimeOfDay <= 0.25f)
        {
            // Sunrise
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.23f) * (1 / 0.02f)));
        }
        else if (currentTimeOfDay >= 0.73f)
        {
            // Sunset
            intensityMultiplier = Mathf.Clamp01(((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        moon.intensity = moonInitialIntensity * intensityMultiplier;
        moon.bounceIntensity = moonInitialIndirect * intensityMultiplier;
        moon.color = moonColorGradient.Evaluate(currentTimeOfDay);
    }
}