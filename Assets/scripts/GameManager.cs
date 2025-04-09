using Unity.VisualScripting;
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
    public GameObject menuSet;
    public bool isAction;
    public int talkIndex;
    public Sprite prevchar_pic;
    public TypeEffect talk;
    public Text questText;
    public GameObject player;

    

    private void Start()
    {
        GameLoad();
        questText.text = questManager.CheckQuest();
    }


    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SubmenuActive();
        }
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetBool("isShow",isAction);


        
    }
    public void SubmenuActive()
    {
        if (menuSet.activeSelf)
            menuSet.SetActive(false);
        else
            menuSet.SetActive(true);
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
            questText.text = questManager.CheckQuest(id);
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

    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX",player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY",player.transform.position.y);
        PlayerPrefs.SetInt("QuestID", questManager.QuestId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.QuestActionIndex);//정보 저장
        PlayerPrefs.Save();
    }
    public void GameLoad()
    {
        if(!PlayerPrefs.HasKey("PlayerX"))
            { return; }

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int QuestId = PlayerPrefs.GetInt("QuestID");
        int QuestActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        player.transform.position = new Vector2(x, y);
        questManager.QuestId = QuestId;
        questManager.QuestActionIndex = QuestActionIndex;
        questManager.ControlObject();
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
