/*
  Author      森友雅u
  LastUpdate  2020/08/28
  Since       2020/08/28
  Contents    タイトルボタンの処理
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Message;

public class TitleButton : MonoBehaviour
{
    //scripts
    private MessageManager message_manager;
    private ChoiceMessage choice_message;
    private CloseMessage close_message;

    void Start()
    {
        message_manager = GameObject.Find("MessageCanvas").GetComponent<MessageManager>();
    }



    /// <summary>
    /// ダイアログを表示するトリガーメソッド
    /// </summary>
    public void OnDialog()
    {
        //表示するメッセージ
        string msg = "タイトルへ戻りますか？";

        //ダイアログの種類 (Choice or Close)
        string kind = "Choice";

        GameObject instance = message_manager.DispDialog(kind, msg);

        if (kind == "Choice")
        {
            choice_message = instance.transform.GetComponent<ChoiceMessage>();
            choice_message.FixDialog = result => Action(result.ToString());
        }
        else if (kind == "Close")
        {
            close_message = instance.transform.GetComponent<CloseMessage>();
            close_message.FixDialog = result => Action(result.ToString());
        }

    }


    //それぞれの選択時に応じた処理
    public void Action(string result)
    {

        if (result == "Yes")
        {
            /*Yes選択時の処理*/
            Destroy(GameObject.Find("Field"));
            Destroy(GameObject.Find("ManagerStore"));
            Destroy(GameObject.Find("SoundManager"));
            SceneManager.LoadScene("Title");

        }
        else if (result == "No")
        {
            /*No選択時の処理*/
            Debug.Log("No");

        }
        else if (result == "Close")
        {
            /*Close選択時の処理*/
            Debug.Log("Close");

        }
    }
}