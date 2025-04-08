using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    string targetMsg;
    public int CharPerSeconds;
    Text msgText;
    AudioSource audioSource;
    int Index;
    public GameObject Endcursor;
    float interval;
    public bool isAnim;

    private void Awake()
    {
        msgText = GetComponent<Text>();
        Endcursor.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }
    public void SetMsg(string msg)
    {
        if (isAnim)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
            return;
        } 
        targetMsg = msg;
        EffectStart();
    }

    // Update is called once per frame
    void EffectStart()
    {
        msgText.text = "";
        Index = 0;
        interval = 1.0f/CharPerSeconds;
        Invoke(nameof(Effecting), interval);
        Endcursor.SetActive(false);

        isAnim =true;
    }

    void Effecting()
    {
        if (msgText.text == targetMsg) {
            EffectEnd();
            return;
        }
        
        msgText.text += targetMsg[Index];
        if (targetMsg[Index]!=' '|| targetMsg[Index]!='.')
            audioSource.Play();
        Index++;

       
        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        Endcursor.SetActive(true);
        isAnim = false;
    }
}
