using UnityEngine;

namespace SimpleClicker.Extensions
{
    public class PP
    {
        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public static float GetFloat(string key, float @default)
        {
            return PlayerPrefs.GetFloat(key, @default);
        }

        public static void Reset(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}