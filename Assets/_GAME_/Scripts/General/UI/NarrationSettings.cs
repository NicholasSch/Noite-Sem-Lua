using System;

[Serializable]
public struct NarrationSettings
{
    public float displayDuration;
    public float fadeDuration;
    public float typingSpeed;

    public static NarrationSettings Default => new NarrationSettings
    {
        displayDuration = 2f,
        fadeDuration = 0.5f,
        typingSpeed = 0.045f
    };
}