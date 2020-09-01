/*
  Author      田中木介
  LastUpdate  2020/05/16
  Since       2020/03/14
  Contents    AddBgm AddSeで追加する
  Usage : ①GameObject　SoundManagerからSoundManagerコンポーネントを取得する
          ②SoundManager.AddSE()or AddBGM()で音を追加する　/この時resourceNameにファイル名、nameで音を操作する時の名前
          ③PlayBGM()orPlaySE()等で音を再生
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Sound {

    [RequireComponent(typeof(AudioClip))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour,ISound
    {
        private BGMInfo bgmMG = new BGMInfo();
        private SEInfo seMG= new SEInfo();

        private AudioSource bgmSource;
        private AudioSource seSource;

        public float BGMVolume
        {
            get
            {
                return bgmSource.volume;
            }
            set
            {
                bgmSource.volume = value;
            }
        }


        public float SEVolume
        {
            get
            {
                return seSource.volume;
            }
            set
            {
                seSource.volume = value;
            }
        }

        void Start()
        {
            bgmSource = GetComponents<AudioSource>()[0];
            seSource=GetComponents<AudioSource>()[1];
            
            DontDestroyOnLoad(gameObject);

            bgmSource.loop = true;
            seSource.loop = false;

        }

        public void AddSE(string resourceName, string seName)
        {
            /*
             TODO:SEを追加　
             Argument:ファイルの名前　そのSEを扱う時の名前　
             Coments : このseNameを使ってSEを操作する
             Return:none
             */

            seMG.AddSE(resourceName, seName);
        }

        public void AddBGM(string resourceName, string bgmName)
        {
            /*
             TODO:BGMを追加　
             Argument:ファイルの名前　そのBGMを扱う時の名前
             Coments : このbgmNameを使ってSEを操作する
             Return:none
             */
            bgmMG.AddBGM(resourceName, bgmName);
        }


        public void PlayBGM(string bgmName)
        {
            /*
             TODO:BGMを流す
             Argument:bgmの名前
             Return:none
             */

            if (bgmSource == null) return;

            var bgm = bgmMG.BgmDictionary[bgmName];
            bgmSource.clip = bgm;
            bgmSource.Play();
        }

        public void StopBGM(string bgmName)
        {
            /*
             TODO:BGMを止める
             Argument:bgmの名前
             Return:none
             */
            if (bgmSource == null) return;
            bgmSource.Stop();
        }

        public void PlaySE(string seName)
        {
            /*
             TODO:SEを流す
             Argument:SEの名前
             Return:none
             */
            if (seSource == null) return;

            seSource.PlayOneShot(seMG.SeDictionary[seName]);
        }
    }

}


