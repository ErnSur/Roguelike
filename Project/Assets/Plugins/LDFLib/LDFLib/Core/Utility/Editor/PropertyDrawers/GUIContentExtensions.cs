using UnityEngine;

namespace LDF.Utility
{
    public static class GUIContentExtensions
    {
        public static void SetNiceBackingFieldName(this GUIContent content)
        {
            content.text = GetDisplayNameOfPropertyBackingField(content.text);
        }

        public static string GetDisplayNameOfPropertyBackingField(string backingFieldName)
        {
            const int _indexOfLessThanSign = 1;

            if (backingFieldName.StartsWith("<", System.StringComparison.CurrentCulture))
            {
                backingFieldName = backingFieldName.Substring(_indexOfLessThanSign, backingFieldName.IndexOf('>') - _indexOfLessThanSign);
            }
            
            return backingFieldName;
        }
    }
}