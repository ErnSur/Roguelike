using System;

namespace LDF.Utility
{
    //todo:
    // add and handle "useForChildren"
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class PropertyDrawerAttribute : Attribute
    {
        public Type TargetDrawerTargetType;

        public PropertyDrawerAttribute(Type drawerTargetType)
        {
            TargetDrawerTargetType = drawerTargetType;
        }
    }
}