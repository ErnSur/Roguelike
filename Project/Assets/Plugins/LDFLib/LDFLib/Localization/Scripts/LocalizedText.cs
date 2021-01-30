using LDF.Localization;
using UnityEngine;

[CreateAssetMenu()]
public class LocalizedText : ScriptableObject
{
    public string Key => name;

    public string GetText()
    {
        return LocalizationSystem.GetLocalizedString(this);
    }

    public static implicit operator string(LocalizedText t) => t.GetText();
}