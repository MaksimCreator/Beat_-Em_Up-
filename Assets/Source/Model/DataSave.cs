using UnityEngine;

namespace Model
{
    public class DataSave
    {
        public void Save(Data data)
        {
            string json = JsonUtility.ToJson(data);
            System.IO.File.WriteAllText(Application.persistentDataPath + Constant.PathBeforceData, json);
        }

        public Data Load()
        {
            string path = Application.persistentDataPath + Constant.PathBeforceData;
            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path);
                return JsonUtility.FromJson<Data>(json);
            }
            return null;
        }
    }
}