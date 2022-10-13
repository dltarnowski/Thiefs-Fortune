using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    [Range(0, 1)]
    public float TimeOfDay;
    public float DayDuration = 30f;

    public AnimationCurve SunCurve;
    public AnimationCurve MoonCurve;
    public AnimationCurve SkyboxCurve;

    public Material DaySkybox;
    public Material NightSkybox;
  
    public Stars Stars;

    public Light Sun;
    public Light Moon;

    public float sunIntensity;
    public float moonIntensity;

    void Start()
    {
        Sun.intensity = sunIntensity;
        Moon.intensity = moonIntensity;
    }

    void Update()
    {
        TimeOfDay += Time.deltaTime / DayDuration;

        if (TimeOfDay >= 1)
            TimeOfDay -= 1;

        UpdateSkybox();
        UpdateLight();
        UpdateStars();
    }

    /// <summary>
    /// Update properties in editor mode
    /// </summary>
    void OnValidate()
    {
        UpdateSkybox();
        UpdateLight();
        UpdateStars();

        Stars.EnableStars();
        Stars.SetSize();
        Stars.SetLifetime();
        Stars.SetColor();
        Stars.SetAmount();
        Stars.SetColorOverLifetime();
    }

    /// <summary>
    /// Change skybox from day to night
    /// </summary>
    public void UpdateSkybox()
    {
        RenderSettings.skybox.Lerp(NightSkybox, DaySkybox, SkyboxCurve.Evaluate(TimeOfDay));
        RenderSettings.sun = SkyboxCurve.Evaluate(TimeOfDay) > 0.5f ? Sun : Moon;
        DynamicGI.UpdateEnvironment();
    }

    /// <summary>
    /// Update Sun and Moon position and intensity
    /// </summary>
    public void UpdateLight()
    {
        // Sun and Moon rotation
        Sun.transform.localRotation = Quaternion.Euler(TimeOfDay * 360f, 180, 0);
        Moon.transform.localRotation = Quaternion.Euler(TimeOfDay * 360f + 180f, 180, 0);

        // Sun and Moon light intensity
        Sun.intensity = sunIntensity * SunCurve.Evaluate(TimeOfDay);
        Moon.intensity = moonIntensity * MoonCurve.Evaluate(TimeOfDay);
    }

    /// <summary>
    /// Update stars transparency
    /// </summary>
    public void UpdateStars()
    {
        Stars.UpdateColor(new Color(1, 1, 1, 1 - SkyboxCurve.Evaluate(TimeOfDay)));
    }
}
