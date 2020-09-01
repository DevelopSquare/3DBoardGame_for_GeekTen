/*
  Author      森友雅 + kicu
  LastUpdate  2020/08/29
  Since       2020/08/28
  Contents    ダイアログの処理を行う際のテンプレート
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Message;
using InputID;

public class CreateMessage : MonoBehaviour
{
    //scripts
    private MessageManager message_manager;
    private ChoiceMessage choice_message;    
    private CloseMessage close_message;
    private InputEventFactory input;


    void Start()
    {
        message_manager = GameObject.Find("MessageCanvas").GetComponent<MessageManager>();
        input = GameObject.Find("InputManager").GetComponent<InputEventFactory>();
    }



     /// <summary>
    /// ダイアログを表示するトリガーメソッド
    /// </summary>
    public void OnDialog()
    {
        input.rock = true;

        //表示するメッセージ
        string msg = "な";

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
        input.rock = false;

        if (result == "Yes")
        {

            /*Yes選択時の処理*/
            Debug.Log("Yes");

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