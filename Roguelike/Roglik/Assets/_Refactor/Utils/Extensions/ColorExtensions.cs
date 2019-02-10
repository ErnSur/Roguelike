using UnityEngine;

namespace LDF.Utils
{
    public static class ColorExtensions
    {
        public static Color GetRandomColor()
        {
            return new Color(Random.value,Random.value,Random.value);
        }
        
        public static Color GetRandomColor(int rndSeed)
        {
            Random.InitState(rndSeed);
            return GetRandomColor();
        }
    }
}