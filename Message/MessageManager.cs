/*
  Author      森友雅 + kicu
  LastUpdate  2020/08/28
  Since       2020/08/28
  Contents    Messageを表示する際、親オブジェクトのCanvasにアタッチ　　
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using InputID;

namespace Message
{
    public class MessageManager : MonoBehaviour
    {
        //private InputEventFactory input;

        private void Start()
        {
            // input = GameObject.Find("InputManager").GetComponent<InputEventFactory>();
        }


        public GameObject DispDialog(string kind,string tmpMsg)
        {
            //input.rock = true;
            GameObject mObj = (GameObject)Resources.Load("Message/Prehabs/"+ kind +"Message");
            GameObject instance = (GameObject)Instantiate(mObj,this.transform.position,
                                               Quaternion.identity);
            instance.transform.SetParent(this.transform);
            instance.transform.localScale = new Vector3(1, 1, 1);
            Text dispMsg = instance.transform.Find("DialogBody").
                                         transform.Find("Text").GetComponent<Text>();
            dispMsg.text = tmpMsg;

            return instance;
        }

    }
}
