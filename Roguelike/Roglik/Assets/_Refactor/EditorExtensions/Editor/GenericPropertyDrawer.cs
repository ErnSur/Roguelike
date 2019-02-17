using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using PopupWindow = UnityEngine.UIElements.PopupWindow;

[CustomPropertyDrawer(typeof(UIElementsDrawerType),true)]
public class GenericPropertyDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();
        container.style.backgroundColor = UnityEngine.Random.ColorHSV();
 

        var popup = new UnityEngine.UIElements.ScrollView(ScrollViewMode.Vertical);
        container.Add(popup);
        popup.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        popup.style.flexGrow = 1;
        popup.style.flexShrink = 0;
        popup.style.flexBasis = 1;
        popup.Add(new PropertyField(property.FindPropertyRelative("amount")));
        popup.Add(new PropertyField(property.FindPropertyRelative("unit")));
        popup.Add(new PropertyField(property.FindPropertyRelative("name"), "CustomLabel: Name"));

        void BindItem(VisualElement v,int i)
        {
            var list = property.serializedObject.GetProperties().ToList();
            var newV = v as PropertyField;
            newV.BindProperty(list[i]);
        }
 
        return container;
    }
}

[CustomEditor(typeof(DrawerMono), true, isFallback = true)]
public class DrawerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
    }

    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();
        
        var propertiesBox = new VisualElement();
        root.Add(propertiesBox);
        
        foreach (var prop in serializedObject.GetProperties())
        {
            propertiesBox.Add(new PropertyField(prop));
        }
        
        propertiesBox[0].SetEnabled(false);
        
        return root;
    }
}

public class SerializedPropertyEnumerable : IEnumerable<SerializedProperty>
{
    private readonly SerializedProperty _prop;
    private const string _basePropName = "Base";
    public SerializedPropertyEnumerable(SerializedProperty prop)
    {
        _prop = prop;
    }

    public IEnumerator<SerializedProperty> GetEnumerator()
    {
        while (_prop.NextVisible(_prop.name == _basePropName))
        {
            yield return _prop;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public static class SerializedObjectExtensions
{
    public static IEnumerable<SerializedProperty> GetProperties(this SerializedObject o)
    {
        return new SerializedPropertyEnumerable(o.GetIterator());
    }
}