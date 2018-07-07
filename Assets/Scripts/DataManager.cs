using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Canvas使う為の準備！

//PlayLogをCSVファイルに貯めておく為のクラス！
public class DataManager : MonoBehaviour
{
	string[,] playLog;//string型の2次元配列playLogを定義(取得したcsvファイルを2次元配列に直してぶち込んでおく為の変数)
	[SerializeField] InputField inputField;//score入力する為のInputField
	int score;


	string csvPath = "CSV/PlayLog";//扱うCSVがある場所(Path)



    void Start()
    {
		Load();//CSVファイル"PlayLog"を変数playLogに格納！
    }



    //- * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - *




    //CSVファイルを読み込む関数
    public void Load()
    {
		playLog = csvManager.GetCsvData(csvPath);//Resourcesフォルダ内のplayLogを取得→2次元配列に変換して変数playLogの中に格納
    }

    //CSVファイルに書き込みを行う関数
	public void SaveCSV()
    {
		if(inputField.text != null)
		{
			score = int.Parse(inputField.text);
			AddRow(score);
            csvManager.WriteData(csvPath + ".csv", playLog);//配列playLogの内容をCSV『PlayLog』に適応！
            Debug.Log("PlayLog Saved!!");
		}
		else
		{
			Debug.Log("InputField is Null");
		}
    }

    //playLogの要素数追加→要は行数追加！
	public void AddRow(int score)
    {

        int rowCount = playLog.GetLength(0);//行数取得！
        int colCount = playLog.GetLength(1);//列数獲得！

		//既存のものより1行だけ多い2次元配列を作成！(新しいデータ入れる為)
        string[,] array = new string[rowCount + 1, colCount];

        //今までのデータを全部ぶち込む！
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                array[i, j] = playLog[i, j];
            }
        }

        //新しいデータを追加
        array[rowCount, 0] = rowCount.ToString();//id列に追加
		array[rowCount, 1] = score.ToString();//score列に追加(小数点以下第二位まで)
        
        playLog = array;//playLogにarrayを代入→playLog内容の更新！
    }

}


