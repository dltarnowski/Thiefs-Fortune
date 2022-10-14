using System;
using UnityEngine;

[Serializable]
public class Stars
{
    public ParticleSystem particles;
    public bool isEnabled;
    public ParticleSystem.MinMaxCurve size;
    public ParticleSystem.MinMaxCurve lifetime;
    public Color color;
    public int starsAmount;
    public ParticleSystem.MinMaxGradient colorOverLifetime;

    /// <summary>
    /// Turn stars on or off
    /// </summary>
    public void EnableStars()
    {
        if (isEnabled)
            particles.Play();
        else
            particles.Stop();
    }

    /// <summary>
    /// Update stars color during play
    /// </summary>
    public void UpdateColor(Color color)
    {
        var mainModule = particles.main;
        mainModule.startColor = color;
    }

    /// <summary>
    /// Set stars initial size
    /// </summary>
    public void SetSize()
    {
        var mainModule = particles.main;
        mainModule.startSize = size;
    }

    /// <summary>
    /// Set stars initial lifetime
    /// </summary>
    public void SetLifetime()
    {
        var mainModule = particles.main;
        mainModule.startLifetime = lifetime;
    }

    /// <summary>
    /// Set stars initial color
    /// </summary>
    public void SetColor()
    {
        var mainModule = particles.main;
        mainModule.startColor = color;
    }

    /// <summary>
    /// Set stars initial amount
    /// </summary>
    public void SetAmount()
    {
        var mainModule = particles.main;
        mainModule.maxParticles = starsAmount;
    }

    /// <summary>
    /// Set stars initial color over lifetime
    /// </summary>
    public void SetColorOverLifetime()
    {
        var mainModule = particles.colorOverLifetime;
        mainModule.color = colorOverLifetime;
    }
}
