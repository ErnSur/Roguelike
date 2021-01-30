using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine.Experimental.XR;

namespace LDF.Utility
{
    public static class PropertyDrawerFactory
    {
        //<TargetType,PropertyDrawerType>
        private static readonly Dictionary<Type, Type> _extendedDrawers;
        //<TargetType,AddonDrawerType>
        private static readonly Dictionary<Type, Type> _addonDrawers;

        private const string FIELDINFO_FIELD_NAME = "m_FieldInfo";
        private const string ATTRIBUTE_FIELD_NAME = "m_Attribute";

        static PropertyDrawerFactory()
        {
            _addonDrawers = GetAddonDictionary();
            _extendedDrawers = GetDrawerDictionary();
        }

        public static PropertyDrawer GetPropertyDrawer(FieldInfo field)
        {
            if (TryGetExtendedDrawerAttributeTarget(field, out var attributeType))
            {
                return CreatePropertyDrawer(field, attributeType, true);
            }

            if (_extendedDrawers.ContainsKey(field.FieldType))
            {
                return CreatePropertyDrawer(field, field.FieldType, false);
            }

            return null;
        }

        private static bool TryGetExtendedDrawerAttributeTarget(FieldInfo field, out Type attributeType)
        {
            attributeType = field.GetCustomAttributes()
                .Select(attribute => attribute.GetType())
                .FirstOrDefault(_extendedDrawers.ContainsKey);
            return attributeType != null;
        }

        public static List<AddonDrawer> GetAddons(FieldInfo fieldInfo)
        {
            return fieldInfo.GetCustomAttributes()
                .Select(att => att.GetType())
                .Where(attType => _addonDrawers.ContainsKey(attType))
                .Select(CreateAddonDrawer)
                .ToList();

            AddonDrawer CreateAddonDrawer(Type attType)
            {
                var addon = Activator.CreateInstance(_addonDrawers[attType]) as AddonDrawer;
                addon?.Initialize(fieldInfo.GetCustomAttribute(attType));

                return addon;
            }
        }

        private static PropertyDrawer CreatePropertyDrawer(FieldInfo field, Type targetType, bool isAttributeDrawer)
        {
            var drawer = Activator.CreateInstance(_extendedDrawers[targetType]) as PropertyDrawer;

            typeof(PropertyDrawer)
                .GetField(FIELDINFO_FIELD_NAME, BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(drawer, field);

            if (isAttributeDrawer)
            {
                typeof(PropertyDrawer)
                    .GetField(ATTRIBUTE_FIELD_NAME, BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(drawer, field.GetCustomAttribute(targetType));
            }

            return drawer;
        }

        private static Dictionary<Type, Type> GetDrawerDictionary()
        {
            return GetExistingExtendedPropertyDrawers()
                .ToDictionary(GetDrawerTargetType, drawer => drawer);

            Type GetDrawerTargetType(Type drawer)
            {
                return drawer
                    .GetCustomAttribute<PropertyDrawerAttribute>()
                    .TargetDrawerTargetType;
            }
        }

        private static Dictionary<Type, Type> GetAddonDictionary()
        {
            return GetExistingAddonDrawers()
                .ToDictionary(GetDrawerTargetType, drawer => drawer);

            Type GetDrawerTargetType(Type drawer)
            {
                return drawer
                    .GetCustomAttribute<CustomAddonDrawerAttribute>()
                    .TargetDrawerTargetType;
            }
        }

        private static Type[] GetExistingExtendedPropertyDrawers()
        {
            return (
                from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                where !domainAssembly.IsDynamic
                from assemblyType in domainAssembly.GetExportedTypes()
                where assemblyType.IsSubclassOf(typeof(PropertyDrawer))
                where assemblyType.IsDefined(typeof(PropertyDrawerAttribute))
                where !assemblyType.IsAbstract
                select assemblyType
            ).ToArray();
        }

        private static Type[] GetExistingAddonDrawers()
        {
            return (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                    where !domainAssembly.IsDynamic
                    from assemblyType in domainAssembly.GetExportedTypes()
                    where assemblyType.IsSubclassOf(typeof(AddonDrawer))
                    where assemblyType.IsDefined(typeof(CustomAddonDrawerAttribute))
                    where !assemblyType.IsGenericType && !assemblyType.IsAbstract
                    select assemblyType
                ).ToArray();
        }
    }
}