using System;

namespace LDF.Utility
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class CustomAddonDrawerAttribute : Attribute
    {
        public Type TargetDrawerTargetType;

        public CustomAddonDrawerAttribute(Type drawerTargetType)
        {
            TargetDrawerTargetType = drawerTargetType;
        }
    }
}