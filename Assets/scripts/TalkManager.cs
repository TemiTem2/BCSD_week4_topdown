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
        //talk data
        talkData.Add(1000, new string[] {"Test Text:0","q3123:2"});
        talkData.Add(2000, new string[] { "Test Text:1", "q3123:0" });
        talkData.Add(3000, new string[] { "상자다" });


        //quest talk
        talkData.Add(10+1000, new string[] { "어서와:0", "하하:2" });
        talkData.Add(11+2000, new string[] { "반가워 Text:1", "책상에서 동전좀 주워줘:0" });


        talkData.Add(20 + 1000, new string[] { "그냥 나 주라:0", "하하:2" });
        talkData.Add(20 + 2000, new string[] { "빨리 가져와잇:1" });
        talkData.Add(20 + 5000, new string[] { "동전을 줏었다"});
        talkData.Add(21 + 2000, new string[] { "가져다줘서고마워!:1", "보상은 없단다:0" });


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
        if (!talkData.ContainsKey(id))
        {//쿼스트 진행 순서가 없을때 맨 처음 대사를 가져온다
            if (!talkData.ContainsKey(id-id%10))
                return GetTalk(id - id%100, talkIndex);//기본 대사출력
            else
                return GetTalk(id-id%10, talkIndex);//퀘스트 처음 대사 출력
        }
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
