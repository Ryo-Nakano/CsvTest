using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;

public class test : MonoBehaviour {


    String[,] csvData;
	List<string[]> lists = new List<string[]>();//stringの配列を格納する為のListを用意(要は2次元配列)


	// Use this for initialization
	void Start () {
		//2次元配列に値格納
		lists.Add(new string[]{"1","poge"});
		lists.Add(new string[] { "2", "pogehoge" });
		lists.Add(new string[] { "3", "magemage" });
		lists.Add(new string[] { "4", "gebogebo" });
		lists.Add(new string[] { "5", "miomio" });


		print("===================================================");
		for (int i = 0; i < lists.Count; i++)
		{
			print(i.ToString() + " : " + lists[i]);
		}
		print("===================================================");

        //testCsv = csvManager.ReadCsvFile("testCsv");//変数testCsvの中に取って来たcsvファイルが入っているはず
        //Debug.Log(testCsv);//中にcsv入っている事は確認

        //logSave(6, "hogehogepoge");
        csvManager.WriteData("testCsv.csv", lists);//Listsの内容をtestCsvに上書き！ 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
