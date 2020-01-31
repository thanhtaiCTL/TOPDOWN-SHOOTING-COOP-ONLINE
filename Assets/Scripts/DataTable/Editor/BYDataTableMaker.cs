#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.IO;

public class BYDataTableMaker : MonoBehaviour
{
    [MenuItem("Assets/DataTable/Make Data by CSV")]
    public static void CreateAsset()
    {
        foreach(Object obj in Selection.objects)
        {
            TextAsset csvFile = (TextAsset)obj;
            string tableName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(csvFile));
            ScriptableObject scripTableObject = ScriptableObject.CreateInstance(tableName);

            if (scripTableObject == null)
                return;

            AssetDatabase.CreateAsset(scripTableObject, "Assets/Resources/DataTable/" + tableName + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            BYDataTableOrigin byOrigin = (BYDataTableOrigin)scripTableObject;
            byOrigin.ImportData(csvFile.text);
            EditorUtility.SetDirty(byOrigin);
        }
    }

    [MenuItem("Assets/DataTable/Create CSV File from Scriptable Object", false, 1)]
    private static void CreateCSVFile()
    {
        foreach(UnityEngine.Object obj in Selection.objects)
        {
            BYDataTableOrigin dataFile = (BYDataTableOrigin)obj;
            string tableName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(dataFile));
            string data = dataFile.GetCSVData();
            string filePath = "Assets/Data/DataTable/" + tableName + ".csv";
            FileUtil.DeleteFileOrDirectory(filePath);

            //Create new file .txt
            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter sWriter = new StreamWriter(fs);
            //write and close file
            sWriter.Write(data);
            sWriter.Flush();
            fs.Close();
            AssetDatabase.Refresh();
        }
    }
}

#endif
