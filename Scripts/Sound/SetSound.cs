/*
  Author      田中木介
  LastUpdate  2020/05/28
  Since       2020/05/26
  Contents    AddBgm AddSeで追加する
  
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class SetSound : MonoBehaviour
    {
        private SoundManager manager;

        // Start is called before the first frame update
        void Awake()
        {
            manager = GetComponent<SoundManager>();

            SetBGM();
            SetSE();

        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void SetBGM()
        {
            manager.AddBGM("New_Morning","Title");
            manager.AddBGM("Chemical_House", "GameSetUp");
            manager.AddBGM("Quick_pipes", "PlayGame");
        }

        private void SetSE()
        {
            manager.AddSE("botton01","titleBotton");
            manager.AddSE("botton02","normalBotton");

            manager.AddSE("magic-worp1", "EffectMove");
            manager.AddSE("explotion01", "EffectBeated");
            manager.AddSE("recollection1", "EffectCreate");
            manager.AddSE("boon1", "EffectDestroy");
        }
    }
}


