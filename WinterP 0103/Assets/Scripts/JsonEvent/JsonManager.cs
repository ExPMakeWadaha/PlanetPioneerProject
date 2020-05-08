using System.Collections;
using UnityEngine;

public class JsonManager {

    public static string JsonReader(string path)
    {
        string jsonFilePath = path.Replace(".json", "");

        TextAsset loadedJsonfile = Resources.Load<TextAsset>(jsonFilePath);
        return loadedJsonfile.text;
    }

    public static T[] getEventListJson<T>(string path)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(JsonReader(path));
        return wrapper.Events;
    }

    [System.Serializable]
    class Wrapper<T>
    {
        public T[] Events;
    }

}
