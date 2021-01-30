using System;
using UnityEngine;
using System.Collections.Generic;
using System.Globalization;

namespace LDF.Localization
{
    public static class LocalizationSystem
    {
        private const string _playerPrefKey = "Current_Language";

        private static SystemLanguage _fallbackLanguage;
        private static Dictionary<string, string> _languageDictionary = new Dictionary<string, string>();

        public static bool IsInitialized { get; private set; }
        public static event Action LocalizationChangedEvent;
        public static SystemLanguage CurrentLanguage { get; private set; }

        static LocalizationSystem()
        {
            if(!IsInitialized)
            {
                Initialize(SystemLanguage.English);
            }
        }

        public static void Initialize(SystemLanguage fallbackLanguage)
        {
            if(IsInitialized)
            {
                return;
            }

            _fallbackLanguage = fallbackLanguage;
            InitCurrentLanguage();
            IsInitialized = true;
        }

        public static string GetLocalizedString(LocalizedText keyAsset)
        {
            return keyAsset != null ? GetLocalizedString(keyAsset.Key) : null;
        }

        public static string GetLocalizedString(string key)
        {
            if (!IsInitialized)
            {
                throw new Exception("Localization not initialized.");
            }

            if (_languageDictionary.ContainsKey(key))
            {
                return _languageDictionary[key];
            }

            return null;
        }

        public static string[] GetLocalizedStrings(params string[] keys)
        {
            string[] localizedStrings = new string[keys.Length];

            for (int i = 0; i < keys.Length; i++)
            {
                localizedStrings[i] = GetLocalizedString(keys[i]);
            }

            return localizedStrings;
        }

        public static CultureInfo GetCultureInfo()
        {
            switch(CurrentLanguage)
            {
                //todo: en-GB/en-US?
                case SystemLanguage.English:
                    {
                        return new CultureInfo("en-GB");
                    }
                case SystemLanguage.Polish:
                    {
                        return new CultureInfo("pl-PL");
                    }
                default:
                    {
                        throw new NotSupportedException("Currently other than english/polish languages are not supported");
                    }
            }
        }

        public static void SetCurrentLanguage(SystemLanguage value)
        {
            if (value == CurrentLanguage)
                return;

            CurrentLanguage = value;
            LoadLanguageDictionary(value);
            LocalizationChangedEvent?.Invoke();
            PlayerPrefs.SetString(_playerPrefKey, value.ToString());
        }

        private static void InitCurrentLanguage()
        {
            var language = GetLanguageFromPlayerPrefs();

            if (language == SystemLanguage.Unknown)
            {
                SetCurrentLanguage(Application.systemLanguage);
            }
            else
            {
                SetCurrentLanguage(language);
            }
        }

        private static SystemLanguage GetLanguageFromPlayerPrefs()
        {
            var savedLanguage = PlayerPrefs.GetString(_playerPrefKey);

            try
            {
                return (SystemLanguage)Enum.Parse(typeof(SystemLanguage), savedLanguage);
            }
            catch
            {
                return SystemLanguage.Unknown;
            }
        }

        private static void LoadLanguageDictionary(SystemLanguage language)
        {
#if DEBUG
            Debug.Log($"Loading localization language from resources: Localization/{language.ToString()}");
#endif
            TextAsset jsonObject = Resources.Load<TextAsset>($"Localization/{language.ToString()}");

            if (jsonObject == null)
            {
                jsonObject = Resources.Load<TextAsset>($"Localization/{_fallbackLanguage.ToString()}");

                if (jsonObject == null)
                {
                    throw new Exception($"No Localization Json File at \"Localization/{language.ToString()}\" or \"Localization/{_fallbackLanguage.ToString()}\"");
                }
            }

            var jsonDictionary = JsonUtility.FromJson<LocalizedDictionaryJsonWrapper>(jsonObject.text);

            CreateNewDictionaryFromJson(jsonDictionary);
        }

        private static void CreateNewDictionaryFromJson(LocalizedDictionaryJsonWrapper jsonDictionary)
        {
            _languageDictionary = new Dictionary<string, string>();

            foreach (var kvp in jsonDictionary.KeyValuePairs)
            {
                _languageDictionary.Add(kvp.LocalizationKey, kvp.localizedText);
            }
        }
    }
}