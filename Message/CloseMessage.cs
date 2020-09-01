/*
  Author      森友雅 + kicu
  LastUpdate  2020/08/28
  Since       2020/08/28
  Contents    「Asseets/Message/Prehabs」のCloseMessageのprehabオブジェクトにアタッチ　　
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Message
{
    public class CloseMessage: MonoBehaviour
    {

        public enum DialogResult
        {
            Close,
        }

        public Action<DialogResult> FixDialog { get; set; }

        
        public void OnClose()
        {
            this.FixDialog?.Invoke(DialogResult.Close);
            Destroy(this.gameObject);
        }

    }
}
