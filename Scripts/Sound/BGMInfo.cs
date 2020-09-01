/*
  Author      田中木介
  LastUpdate  2020/03/16
  Since       2020/03/14
  Contents    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


namespace Sound {
    //[RequireComponent(typeof(AudioSource))]
    public class BGMInfo 
    {
        public Dictionary<string, AudioClip> BgmDict = new Dictionary<string, AudioClip>();

        // Start is called before the first frame update


        public Dictionary<string, AudioClip> BgmDictionary
        {
            get { return BgmDict; }
        }

        public void AddBGM(string resourceName , string bgmName)
        {
            //AudioClip clip;
            BgmDict[bgmName]= Resources.Load("Sound/BGM/"+resourceName) as AudioClip;
        }

        }
        

            
    }







    

