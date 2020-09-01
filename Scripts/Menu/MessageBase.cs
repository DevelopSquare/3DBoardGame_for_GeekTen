/*
  Author      田中木介
  LastUpdate  2020/05/09
  Since       2020/05/09
  Contents    Messageのabstract class
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public enum ClickKind
    {
        /*
        TODO:クリックの種類
        */
        free,
        yes,
        no,
        close
    }

    public abstract class MessageBase : MonoBehaviour
    {
        protected string message = "Message";
        protected Text text = default;

        private ClickKind clickCondition = ClickKind.free;

        public void SetText(string mess)
        {
            /*
             TODO:Change text of the message
             Argument:message text
             Return:none
             */
            message = mess;
            text.text = message;
        }

        public Text ChatchText()
        {
            /*
             TODO:テクストをゲットできる
             Argument:none
             Return: Text
             */
            return text;
        }

        

        public void OnFree()
        {
            /*
             TODO:押されてい無いとき
             Argument:nnone
             Return:none
             */
            clickCondition = ClickKind.free;
        }

        public void Delete()
        {
            /*
             TODO:Mesageの消去
             Argument:none
             Return:none
             */
            Destroy(this.gameObject);
        }

        protected void Init()
        {
            text = this.transform.Find("Text").gameObject.GetComponent<Text>();
            Debug.Log(text);
        }
    }
}


