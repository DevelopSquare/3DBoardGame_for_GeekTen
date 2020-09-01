/*
  Author      森友雅
  LastUpdate  2020/08/28
  Since       2020/08/28
  Contents    N GameSetUp
              スタートボタン
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using InputID;
using Message;

namespace GameManager.GameSetUp
{
    public class StartButton : MonoBehaviour
    {
        //scripts
        private MessageManager message_manager;
        private ChoiceMessage choice_message;
        private CloseMessage close_message;
        private GameSetUp gamesetup;
        private SetObjectManager setobject;
        private InputEventFactory input;

        public GameObject Field;
        public GameObject ManagerStore;

        private void Start()
        {
            gamesetup = GetComponent<GameSetUp>();
            setobject = GetComponent<SetObjectManager>();
            message_manager = GameObject.Find("MessageCanvas").GetComponent<MessageManager>();
            input = GameObject.Find("InputManager").GetComponent<InputEventFactory>();
        }

        /// <summary>
        /// ダイアログを表示するトリガーメソッド
        /// </summary>
        public void OnDialog()
        {
            input.rock = true;

            int cost = gamesetup.RemainPoint();
            //表示するメッセージ
            string msg;
            if (cost > 0)
            {
                msg = "設置可能な駒があります  ゲームを開始しますか?";
            }
            else
            {
                msg = "ゲームを開始しますか?";
            }

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
                setobject.ColiiderOn();
                SceneManager.LoadScene("PlayGame");
                DontDestroyOnLoad(Field);
                DontDestroyOnLoad(ManagerStore);

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
}
