using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager
{
    public static SaveManager instance = new SaveManager();
    private List<string> dataList = new List<string>();
    private string mainMenu = "MainMenu";

    private SaveManager()
    {

    }

    void pushData(string data) // Scene�� �ٲ�� �������� ȣ���� ��ߵ�
    {
        if (data == null) return;
        dataList.Add(data);
    }

    string getLastData()
    {
        if (dataList.Count == 0)
        {
            return null;
        }
        return dataList[dataList.Count - 1];
    }

    string popData()
    {
        string data = getLastData();
        if (data != null)
        {
            dataList.Remove(data);
        }
        return data;
    }

    public void BackToMainMenu() //���� �޴��� �� ���� �� �Լ��� ���ؼ���
    {
        GameManager.instance.playerController.gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void Initialize()
    {
        dataList.Clear();
    }

    public void MoveToNextScene(string curScene, string sceneToLoad)
    {
        if (GameManager.instance.playerController.isDmgBoosted)
        {
            GameManager.instance.playerController.isDmgBoosted = false;
            GameManager.instance.Dmg--;
        }
        CoroutineManager.instance.Coroutine(moveToNextScene(curScene, sceneToLoad));
    }

    private IEnumerator moveToNextScene(string curScene, string sceneToLoad)
    {
        pushData(curScene);
        SceneManager.LoadScene(sceneToLoad);
        yield return null;
        GameManager.instance.playerController.transform.position = GameObject.Find("StartPoint").transform.position;
    }

    public void MoveToPrevScene()
    {
        if (GameManager.instance.playerController.isDmgBoosted)
        {
            GameManager.instance.playerController.isDmgBoosted = false;
            GameManager.instance.Dmg--;
        }
        CoroutineManager.instance.Coroutine(moveToPrevScene());
    }

    private IEnumerator moveToPrevScene()
    {
        string toMove = popData();
        if (toMove == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(toMove);
        }
        yield return null;
        GameManager.instance.playerController.transform.position = GameObject.Find("StartPoint").transform.position;
    }
}