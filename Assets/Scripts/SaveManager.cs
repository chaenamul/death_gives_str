using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager
{
    public static SaveManager instance = new SaveManager();
    private List<string> dataList = new List<string>();
    private SaveManager()
    {

    }
    public void pushData(string data) // Scene�� �ٲ�� �������� ȣ���� ��ߵ�
    {
        dataList.Add(data);
    }
    public string getLastData()
    {
        if (dataList.Count == 0)
        {
            return null;
        }
        return dataList[dataList.Count - 1];
    }
    public string popData()
    {
        string data = getLastData();
        if (data != null)
        {
            dataList.Remove(data);
        }
        return data;

    }
}
