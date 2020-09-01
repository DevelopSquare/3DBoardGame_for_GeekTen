/*
  Author      田中木介
  LastUpdate  2020/05/09
  Since       2020/05/09
  Contents    ChoiceMessageの生成とか
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{

    public class ChoiceMessage : MessageBase
    {
        private ClickKind clickCondition = ClickKind.free;


        // Start is called before the first frame update
        void Awake()
        {
            Init();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnYes()
        {
            clickCondition = ClickKind.yes;
        }

        public void OnNo()
        {
            clickCondition = ClickKind.no;
        }

        public ClickKind GetClick()
        {
            /*
             TODO:クリックの状態を取得する
             Argument:none
             Return:ClickKind
             */
            return clickCondition;
        }
    }
}

