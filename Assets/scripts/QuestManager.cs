using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public int QuestId;
    public int QuestActionIndex;
    public GameObject[] questObject;


    Dictionary<int, QuestData> QuestList;
    void Awake()
    {
        QuestList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        QuestList.Add(10, new QuestData("대화하기",new int[] {1000,2000}));
        QuestList.Add(20, new QuestData("동전줍기", new int[] { 5000, 2000 }));
        QuestList.Add(30, new QuestData("모두 완료!", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id) 
    {
        return QuestId + QuestActionIndex;

    }


    public string CheckQuest(int Id)
    {
       

        if (Id == QuestList[QuestId].npcId[QuestActionIndex])
            QuestActionIndex++;
        //퀘스트 오브젝트 컨트롤
        ControlObject();

        if (QuestActionIndex == QuestList[QuestId].npcId.Length)
            NextQuest();

        return QuestList[QuestId].QuestName;
    }
    public string CheckQuest()
    {
        return QuestList[QuestId].QuestName;
    }

    void NextQuest()
    {
        QuestId += 10;
        QuestActionIndex = 0;
    }

    public void ControlObject() 
    {
        switch (QuestId)
        {
            case 10:
                if(QuestActionIndex==2)
                    questObject[0].SetActive(true);

                break;
            case 20:
                if (QuestActionIndex==0)
                    questObject[0].SetActive(true);
                if (QuestActionIndex == 1)
                    questObject[0].SetActive(false);
                break;
        }
    }
}
