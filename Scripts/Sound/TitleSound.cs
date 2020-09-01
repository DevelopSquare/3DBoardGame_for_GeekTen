/*
  Author      田中木介
  LastUpdate  2020/05/28
  Since       2020/05/26
  Contents    TitleのSoundを管理
  
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound;

namespace GameManager.TitleManager
{
    public class TitleSound : MonoBehaviour
    {
        private SoundManager sound;

        private bool isPlayBgm=false;

        // Start is called before the first frame update
        void Start()
        {
            sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        }

        // Update is called once per frame
        void Update()
        {
            if (!isPlayBgm)
            {
                sound.PlayBGM("Title");
                isPlayBgm = true;

            }

        }

        void OnDisable()
        {
            Debug.Log("Destroyed");
            sound.StopBGM("Title");

        }

        public void ClickSound()
        {
            sound.PlaySE("titleBotton");
        }
    }
}


