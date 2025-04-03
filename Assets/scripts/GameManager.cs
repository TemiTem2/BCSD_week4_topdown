using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkPanel;
    public Image char_picImg;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;

    private void Awake()
    {
        talkPanel.SetActive(false);
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);


        
    }
    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null) 
        {
            isAction = false;
            talkIndex = 0;
            return;
        }
        
        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0];

            char_picImg.sprite = talkManager.Getchar_pic(id,int.Parse(talkData.Split(':')[1]));
            char_picImg.color = new Color(1, 1, 1, 1);
        }

        else { talkText.text = talkData;
            char_picImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
}
