/*
  Author      田中木介・森友雅
  LastUpdate  2020/08/15
  Since       2020/05/09
  Contents    Messageの生成とか
*/
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InputID;

namespace Menu
{
    public class MessageManager : MonoBehaviour
    {
        private Transform mAnchor;

        private string prefabPath = "MessageTemp/";

        private InputEventFactory input;
        public GameObject titleDialog;

        private void Start()
        {
            input = GameObject.Find("InputManager").GetComponent<InputEventFactory>();
            mAnchor = transform.Find("MessageAnchor");
        }

        // Update is called once per frame
        void Update()
        {
        
        }


        public GameObject CreateChoiceMessage()
        {
            var mObj = (GameObject)Resources.Load(prefabPath + "ChoiceMessage");

            var obj = SetMessage(mObj,Vector2.zero);

            return obj;
        }

        public GameObject CreateTextMessage()
        {
            var mObj = (GameObject)Resources.Load(prefabPath + "TextMessage");

            var obj = SetMessage(mObj, Vector2.zero);

            return obj;
        }

        

        private GameObject SetMessage(GameObject mObj, Vector2 messagePosition)
        {
            var obj = Instantiate(mObj, Vector3.zero, Quaternion.identity);

            obj.transform.parent = mAnchor;

            obj.transform.localPosition = Vector3.zero;

            obj.GetComponent<RectTransform>().anchoredPosition = messagePosition;

            obj.transform.localScale = Vector2.one;
            obj.transform.rotation = new Quaternion(0, 0, 0, 0);


            return obj;
        }


        /// <summary>
        ///Titleボタン関係
        /// </summary>
        /// 
        public void OntitleButtonClick()
        {
            input.rock = true;
            titleDialog.SetActive(true);
        }

        // Yesクリック
        public void OntitleButtonYes()
        {
            Destroy(GameObject.Find("Field"));
            Destroy(GameObject.Find("ManagerStore"));
            Destroy(GameObject.Find("SoundManager"));
            input.rock = false;
            titleDialog.SetActive(false);
            SceneManager.LoadScene("Title");
        }

        // Noクリック
        public void OntitleButtonNo()
        {
            input.rock = false;
            titleDialog.SetActive(false);
        }

    }

}

