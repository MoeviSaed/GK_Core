
using UnityEngine;

namespace MVVM.Data
{
    public class MVVMSaveData
    {
        protected static void SaveString(string link, string value) => PlayerPrefs.SetString(link, value);
        protected static string LoadString(string link, string defaultValue) => PlayerPrefs.GetString(link, defaultValue);

        protected static void SaveInt(string link, int value) => PlayerPrefs.SetInt(link, value);
        protected static int LoadInt(string link, int defaultValue) => PlayerPrefs.GetInt(link, defaultValue);

        protected static void SaveFloat(string link, float value) => PlayerPrefs.SetFloat(link, value);
        protected static float LoadFloat(string link, float defaultValue) => PlayerPrefs.GetFloat(link, defaultValue);

        protected static void SaveBool(string link, bool value) => PlayerPrefs.SetInt(link, value ? 1 : 0);
        protected static bool LoadBool(string link, bool defaultValue) => PlayerPrefs.GetInt(link, defaultValue ? 1 : 0) == 1;
    }
}
