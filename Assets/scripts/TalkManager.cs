using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> char_picData;

    public Sprite[] char_picArr;
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        char_picData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        talkData.Add(1000, new string[] {"Test Text:0","q3123:2"});
        talkData.Add(2000, new string[] { "Test Text:1", "q3123:0" });
        talkData.Add(100, new string[] { "»óÀÚ´Ù" });

        char_picData.Add(1000 + 0, char_picArr[0]);
        char_picData.Add(1000 + 1, char_picArr[1]);
        char_picData.Add(1000 + 2, char_picArr[2]);
        char_picData.Add(1000 + 3, char_picArr[3]);
        char_picData.Add(2000 + 0, char_picArr[4]);
        char_picData.Add(2000 + 1, char_picArr[5]);
        char_picData.Add(2000 + 2, char_picArr[6]);
        char_picData.Add(2000 + 3, char_picArr[7]);
    }

    public string GetTalk(int id,int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
    public Sprite Getchar_pic(int id, int char_picIndex)
    {
        return char_picData[id + char_picIndex];
    }
}
