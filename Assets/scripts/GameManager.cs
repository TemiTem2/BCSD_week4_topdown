using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public Animator talkPanel;
    public Image char_picImg;
    public Animator char_picAnim;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;
    public Sprite prevchar_pic;
    public TypeEffect talk;

    

    private void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetBool("isShow",isAction);


        
    }
    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = 0;
        string talkData = "";
        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }
        
        if (talkData == null) 
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }
        
        if (isNpc)
        {
            talk.SetMsg(talkData.Split(':')[0]);
            //초상화 보이기
            char_picImg.sprite = talkManager.Getchar_pic(id,int.Parse(talkData.Split(':')[1]));
            char_picImg.color = new Color(1, 1, 1, 1);
            //초상화 애니메이션
            if (prevchar_pic != char_picImg.sprite)
            {
                char_picAnim.SetTrigger("doEffect");
                prevchar_pic = char_picImg.sprite;
            }
        }

        else { talk.SetMsg(talkData);
            char_picImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
}
