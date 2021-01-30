using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace LDF.Utility
{
    [CustomPropertyDrawer(typeof(TagSelectorAttribute))]
    public class TagSelectorPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.SetNiceBackingFieldName();

            if (property.propertyType == SerializedPropertyType.String)
            {
                EditorGUI.BeginProperty(position, label, property);

                var att = attribute as TagSelectorAttribute;

                if (att.UseDefaultTagFieldDrawer)
                {
                    property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
                }
                else
                {
                    //generate the taglist + custom tags
                    var tagList = new List<string>();
                    tagList.Add("<NoTag>");
                    tagList.AddRange(InternalEditorUtility.tags);
                    var propertyString = property.stringValue;
                    var index = -1;
                    if (propertyString == "")
                        index = 0; //first index is the special <notag> entry
                    else
                    {
                        for (var i = 1; i < tagList.Count; i++)
                        {
                            if (tagList[i] == propertyString)
                            {
                                index = i;
                                break;
                            }
                        }
                    }

                    //Draw the popup box with the current selected index
                    index = EditorGUI.Popup(position, label.text, index, tagList.ToArray());

                    //Adjust the actual string value of the property based on the selection
                    if (index == 0)
                        property.stringValue = "";
                    else if (index >= 1)
                        property.stringValue = tagList[index];
                    else
                        property.stringValue = "";
                }

                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}