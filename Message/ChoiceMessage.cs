/*
  Author      森友雅 + kicu
  LastUpdate  2020/08/28
  Since       2020/08/28
  Contents    「Asseets/Message/Prehabs」のChoiceMessageのprehabオブジェクトにアタッチ　　
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Message
{
    public class ChoiceMessage: MonoBehaviour
    {

        public enum DialogResult
        {
            Yes,
            No,
        }

        public Action<DialogResult> FixDialog { get; set; }


        public void OnYes()
        {
            this.FixDialog?.Invoke(DialogResult.Yes);
            Destroy(this.gameObject);
        }


        public void OnNo()
        {
            this.FixDialog?.Invoke(DialogResult.No);
            Destroy(this.gameObject);
        }

    }
}
