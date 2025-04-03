using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;

    private void Awake()
    {
        talkPanel.SetActive(false);
    }

    public void Action(GameObject scanObj)
    {
        if (isAction)
        {
            isAction = false;
        }
        else {
            isAction = true;
            
            scanObject = scanObj;
            talkText.text = "�̰���" + scanObject.name + "�Դϴ�";
        }

        talkPanel.SetActive(isAction);

    }
}
