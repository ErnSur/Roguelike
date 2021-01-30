namespace LDF.Localization
{
    [System.Serializable]
    public class LocalizedDictionaryJsonWrapper
    {
        public string Language;
        public LocalizedStringJsonWrapper[] KeyValuePairs;

        public LocalizedDictionaryJsonWrapper(LocalizedStringJsonWrapper[] keyValuePairs)
        {
            KeyValuePairs = keyValuePairs;
        }
    }
}