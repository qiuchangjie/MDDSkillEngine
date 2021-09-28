//====================Copyright statement:AppsTools===================//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ShortRangeSkillsVFXEffectBundle1Demo : MonoBehaviour
{
    string ss = "Skill Boom 10_Green&Skill Boom 10_Purple&Skill Boom 10_Yellow&Skill Boom 11_Green&Skill Boom 11_Purple&Skill Boom 11_Yellow&Skill Boom 12_Green&Skill Boom 12_Purple&Skill Boom 12_Yellow&Skill Boom 13_Green&Skill Boom 13_Purple&Skill Boom 13_Yellow&Skill Boom 14_Green&Skill Boom 14_Purple&Skill Boom 14_Yellow&Skill Boom 15_Green&Skill Boom 15_Purple&Skill Boom 15_Yellow&Skill Boom 16&Skill Boom 1_Green&Skill Boom 1_Purple&Skill Boom 1_Yellow&Skill Boom 2_Blue&Skill Boom 2_Green&Skill Boom 2_Yellow&Skill Boom 3_Blue&Skill Boom 3_Green&Skill Boom 3_Yellow&Skill Boom 4_Blue&Skill Boom 4_Green&Skill Boom 4_Yellow&Skill Boom 5_Green&Skill Boom 5_Purple&Skill Boom 5_Yellow&Skill Boom 6_Green&Skill Boom 6_Purple&Skill Boom 6_Yellow&Skill Boom 7_Green&Skill Boom 7_Purple&Skill Boom 7_Yellow&Skill Boom 8&Skill Boom 9_Green&Skill Boom 9_Purple&Skill Boom 9_Yellow&Skill Cast 10&Skill Cast 11_Green&Skill Cast 11_Purple&Skill Cast 11_Yellow&Skill Cast 12_Green&Skill Cast 12_Purple&Skill Cast 12_Yellow&Skill Cast 13_Green&Skill Cast 13_Purple&Skill Cast 13_Yellow&Skill Cast 14_Green&Skill Cast 14_Purple&Skill Cast 14_Yellow&Skill Cast 15_Green&Skill Cast 15_Purple&Skill Cast 15_Yellow&Skill Cast 16_Green&Skill Cast 16_Purple&Skill Cast 16_Yellow&Skill Cast 17_Green&Skill Cast 17_Purple&Skill Cast 17_Yellow&Skill Cast 18_Green&Skill Cast 18_Purple&Skill Cast 18_Yellow&Skill Cast 19_Green&Skill Cast 19_Purple&Skill Cast 19_Yellow&Skill Cast 1_Green&Skill Cast 1_Purple&Skill Cast 1_Yellow&Skill Cast 20_Green&Skill Cast 20_Purple&Skill Cast 20_Yellow&Skill Cast 21_Green&Skill Cast 21_Purple&Skill Cast 21_Yellow&Skill Cast 22_Green&Skill Cast 22_Purple&Skill Cast 22_Yellow&Skill Cast 23_Green&Skill Cast 23_Purple&Skill Cast 23_Yellow&Skill Cast 24_Green&Skill Cast 24_Purple&Skill Cast 24_Yellow&Skill Cast 25_Green&Skill Cast 25_Purple&Skill Cast 25_Yellow&Skill Cast 26_Green&Skill Cast 26_Purple&Skill Cast 26_Yellow&Skill Cast 27_Green&Skill Cast 27_Purple&Skill Cast 27_Yellow&Skill Cast 28_Green&Skill Cast 28_Purple&Skill Cast 28_Yellow&Skill Cast 29_Green&Skill Cast 29_Purple&Skill Cast 29_Yellow&Skill Cast 2_Green&Skill Cast 2_Purple&Skill Cast 2_Yellow&Skill Cast 30_Green&Skill Cast 30_Purple&Skill Cast 30_Yellow&Skill Cast 31_Green&Skill Cast 31_Purple&Skill Cast 31_Yellow&Skill Cast 32_Green&Skill Cast 32_Purple&Skill Cast 32_Yellow&Skill Cast 33_Green&Skill Cast 33_Purple&Skill Cast 33_Yellow&Skill Cast 34_Green&Skill Cast 34_Purple&Skill Cast 34_Yellow&Skill Cast 35_Green&Skill Cast 35_Purple&Skill Cast 35_Yellow&Skill Cast 36_Green&Skill Cast 36_Purple&Skill Cast 36_Yellow&Skill Cast 37_Green&Skill Cast 37_Purple&Skill Cast 37_Yellow&Skill Cast 38_Green&Skill Cast 38_Purple&Skill Cast 38_Yellow&Skill Cast 39_Green&Skill Cast 39_Purple&Skill Cast 39_Yellow&Skill Cast 3_Green&Skill Cast 3_Purple&Skill Cast 3_Yellow&Skill Cast 40_Green&Skill Cast 40_Purple&Skill Cast 40_Yellow&Skill Cast 41_Green&Skill Cast 41_Purple&Skill Cast 41_Yellow&Skill Cast 42_Green&Skill Cast 42_Purple&Skill Cast 42_Yellow&Skill Cast 43_Green&Skill Cast 43_Purple&Skill Cast 43_Yellow&Skill Cast 44_Green&Skill Cast 44_Purple&Skill Cast 44_Yellow&Skill Cast 45_Green&Skill Cast 45_Purple&Skill Cast 45_Yellow&Skill Cast 46_Green&Skill Cast 46_Purple&Skill Cast 46_Yellow&Skill Cast 47_Green&Skill Cast 47_Purple&Skill Cast 47_Yellow&Skill Cast 48_Green&Skill Cast 48_Purple&Skill Cast 48_Yellow&Skill Cast 49_Green&Skill Cast 49_Purple&Skill Cast 49_Yellow&Skill Cast 4_Green&Skill Cast 4_Purple&Skill Cast 4_Yellow&Skill Cast 50_Green&Skill Cast 50_Purple&Skill Cast 50_Yellow&Skill Cast 51_Green&Skill Cast 51_Purple&Skill Cast 51_Yellow&Skill Cast 5_Green&Skill Cast 5_Purple&Skill Cast 5_Yellow&Skill Cast 6_Green&Skill Cast 6_Purple&Skill Cast 6_Yellow&Skill Cast 7_Green&Skill Cast 7_Purple&Skill Cast 7_Yellow&Skill Cast 8_Green&Skill Cast 8_Purple&Skill Cast 8_Yellow&Skill Cast 9_Green&Skill Cast 9_Purple&Skill Cast 9_Yellow&Skill Hit 1&Skill Hit 10&Skill Hit 11&Skill Hit 12&Skill Hit 13&Skill Hit 14&Skill Hit 15&Skill Hit 16&Skill Hit 2&Skill Hit 3&Skill Hit 4&Skill Hit 5";
    private bool r = false;
    string[] allArray = null;

    public int i = 0;
    public UnityEngine.UI.Text tex;
    public Transform ts;
    private GameObject currObj;
    public Transform hideParent;

    public void Awake()
    {
        /*
        string st2322r = "";
        var allFiles = Directory.GetFiles(Application.dataPath + "/Short-Range Skills VFX Effect Bundle 1/Prefabs", "*.prefab", SearchOption.AllDirectories);
        for (int i = 0; i < allFiles.Length; i++)
        {
            var str = Application.dataPath + "/Short-Range Skills VFX Effect Bundle 1/Prefabs/";
            allFiles[i] = allFiles[i].Replace(@"\", "/").Replace(str.Replace(@"\", "/"), "").Replace(".prefab", "");
            st2322r += allFiles[i] + "&";

        }
        Debug.LogError(st2322r);
        return;*/


        allArray = ss.Split('&');
        currObj = GameObject.Instantiate(hideParent.transform.Find(allArray[i]).gameObject);
        currObj.transform.SetParent(ts);
        //currObj.transform.localPosition = Vector3.zero;
        tex.text = "Name: "+ i +" 【" + allArray[i] + "】";
    }

    public void Update()
    {
        if (ts != null && r)
        {
            ts.transform.Rotate(Vector3.up * Time.deltaTime * 90f);
        }
    }

    public void R()
    {
        r = true;
    }

    public void NotR()
    {
        r = false;
    }

    public void RePlay() 
    {
        if (currObj != null)
        {
            currObj.SetActive(false);
            currObj.SetActive(true);
        }
    }

    public void CopyName() 
    {
        var s = allArray[i].ToLower().Replace(".prefab", "");
        s = s.Substring(s.IndexOf("/")+1);
        UnityEngine.GUIUtility.systemCopyBuffer = s;
    }

    public void OnLeftBtClick() 
    {
        i--;
        if (i <= 0)
        {
            i = allArray.Length - 1;
        }
        if (currObj != null)
        {
            GameObject.DestroyImmediate(currObj);
        }
        currObj = GameObject.Instantiate(hideParent.transform.Find(allArray[i]).gameObject);
        currObj.transform.SetParent(ts);
        //currObj.transform.localPosition = Vector3.zero;
        tex.text = "Name: " + i + " 【" + allArray[i] + "】";
    }

    public void OnRightBtClick()
    {
        i++;
        if (i >= allArray.Length)
        {
            i = 0;
        }
        if (currObj != null)
        {
            GameObject.DestroyImmediate(currObj);
        }
        currObj = GameObject.Instantiate(hideParent.transform.Find(allArray[i]).gameObject);
        currObj.transform.SetParent(ts);
        //currObj.transform.localPosition = Vector3.zero;
        tex.text = "Name: " + i + " 【" + allArray[i] + "】";
    }
}