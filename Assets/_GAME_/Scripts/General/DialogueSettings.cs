[System.Serializable]
public struct DialogueSettings
{
    public float displayDuration;
    public float fadeDuration;
    public float typingSpeed;

    public static DialogueSettings Default => new DialogueSettings
    {
        displayDuration = 2f,
        fadeDuration = 0.5f,
        typingSpeed = 0.045f
    };
}