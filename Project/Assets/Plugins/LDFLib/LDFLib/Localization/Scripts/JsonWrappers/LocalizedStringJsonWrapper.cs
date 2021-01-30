namespace LDF.Localization
{
    [System.Serializable]
    public class LocalizedStringJsonWrapper
    {
        public string LocalizationKey;
        public string localizedText = "";

        public LocalizedStringJsonWrapper(string localizationKey)
        {
            LocalizationKey = localizationKey;
        }
    }
}