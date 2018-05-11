using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Text;


public class csvManager
{
    /// <summary>
    /// Resourcesフォルダ内のcsvファイルを読み込み、string型2次元配列で返す。
    /// example) CsvManager.ReadTextFile("Commands/BasicCommands.csv")
    /// </summary>
    public static string ReadTextFile(string dataPath)
    {
        string data;
        string textData = OpenTextFile(GetPath() + dataPath);
        if (textData != "ERROR")
        {
            data = textData;
        }
        else
        {
            data = "";
        }

        if (string.IsNullOrEmpty(data))
        {
            return null;
        }
        return data;
    }

    public static string[,] ReadCsvFile(string dataPath)
    {
        string data;
        string textData = OpenTextFile(GetPath() + dataPath);
        if (textData != "ERROR")
        {
            data = textData;
        }
        else
        {
            TextAsset textAsset = (TextAsset)Resources.Load(dataPath.Split('.')[0]);
            data = textAsset.ToString();
        }

        if (data == null)
        {
            return null;//データが空だった時処理を離脱
        }
        string[] rows = data.Replace("\r\n", "\n").Split('\n');//改行で分けて配列rowにぶち込む
        int dataRows = rows.Length;//csvの行数を変数dataRowsに格納
		int dataCols = rows[0].Split(","[0]).Length;//csvの列数を変数dataRowsに格納
        string[,] csvData = new string[dataRows, dataCols];//新しく配列を宣言
        for (int i = 0; i < csvData.GetLength(0); i++)//行数分だけforを回す
        {
            for (int j = 0; j < csvData.GetLength(1) - 1; j++)//列数分だけforを回す
            {
                string[] value = rows[i].Split(","[0]);//コンマ区切りで丁寧にsplitして、配列valueにぶち込んでおく
                csvData[i, j] = value[j];//1列1列丁寧にぶち込んで行く
            }
        }
        return csvData;//2次元配列csvDataを返す
    }


    /// <summary>
    /// Resourcesフォルダ内に、ファイルを書き込む。
    /// 例) csvManager.WriteData("data.csv", dataArray);
    /// </summary>
    public static void WriteData(string dataPath, string[,] newData)
    {
        string stringData = "";
        for (int i = 0; i < newData.GetLength(0); i++)
        {
            for (int j = 0; j < newData.GetLength(1); j++)
            {
                if (j < newData.GetLength(1) - 1)
                {
                    stringData += newData[i, j] + ",";
                }
                else if (j == newData.GetLength(1) - 1 && i < newData.GetLength(0) - 1)
                {
                    stringData += newData[i, j] + "\n";
                }
                else
                {
                    stringData += newData[i, j];
                }
            }
        }
        WriteData(dataPath, stringData);
    }

    public static void WriteData(string dataPath, List<string[]> newData)
    {
        int maxLength = 1;
        for (int i = 0; i < newData.Count; i++)
        {
            if (maxLength < newData[i].Length)
            {
                maxLength = newData[i].Length;
            }
        }
        string stringData = "";
        for (int i = 0; i < newData.Count; i++)
        {
            for (int j = 0; j < maxLength; j++)
            {
                if (j < maxLength - 1)
                {
                    stringData += newData[i][j] + ",";
                }
                else if (j == maxLength - 1 && i < newData.Count - 1)
                {
                    stringData += newData[i][j] + "\n";
                }
                else
                {
                    stringData += newData[i][j];
                }
            }
        }
        WriteData(dataPath, stringData);
    }

    //この関数を使って上書き(override)してるみたい！
    public static void WriteData(string dataPath, string data)
    {
        string[] directries = dataPath.Split('/');
        for (int i = 0; i < directries.Length - 1; i++)
        {
            string tmpPath = "";
            for (int j = 0; j < i + 1; j++)
            {
                tmpPath += directries[j] + "/";
            }
            tmpPath = tmpPath.Remove(tmpPath.Length - 1, 1);
            if (!Directory.Exists(GetPath() + tmpPath))
            {
                Directory.CreateDirectory(GetPath() + tmpPath);
            }
        }
        string existingString = OpenTextFile(GetPath() + dataPath);
        FileStream fs;
        if (existingString == "ERROR")
        {
            fs = new FileStream(GetPath() + dataPath, FileMode.CreateNew);
			Debug.Log("CreateNew");
        }
        else
        {
            fs = new FileStream(GetPath() + dataPath, FileMode.Create);
			Debug.Log("Create");
        }
        StreamWriter sw = new StreamWriter(fs);
        sw.Write(data);
        sw.Flush();
        sw.Close();
    }


    public static string GetPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/Resources/";
#elif UNITY_ANDROID
        return Application.persistentDataPath + "/";
#elif UNITY_IPHONE
        return Application.persistentDataPath + "/";
#else
        return Application.dataPath + "";
#endif
    }

    public static string OpenTextFile(string _filePath)
    {
        FileInfo fi = new FileInfo(_filePath);
        string returnSt = "";
        if (fi.Exists)
        {
            StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8);
            returnSt = sr.ReadToEnd();
            sr.Close();
        }
        else
        {
            returnSt = "ERROR";
        }
        return returnSt;
    }
}