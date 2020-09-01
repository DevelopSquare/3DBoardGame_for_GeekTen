/*
  Author      田中木介
  LastUpdate  2020/05/09
  Since       2020/05/09
  Contents    Settingの管理
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sound;
using MiniMap;
using System.Diagnostics;

namespace Menu
{
    public class SettingManager : MonoBehaviour
    {
        [SerializeField] private Transform Setting;

        private Slider bgmSlider;
        private Slider seSlider;
        private Slider cameraSlider;

        private SoundManager sound;
        private MiniMapCameraCtrl camera;

        private Text bgmNum;
        private Text seNum;
        private Text cameraNum;

        

        // Start is called before the first frame update
        void Awake()
        {
            var bgmBar = Setting.Find("BGMBar").gameObject;
            var seBar = Setting.Find("SEBar").gameObject;
            var cameraBar = Setting.Find("CameraBar").gameObject;
            bgmSlider = bgmBar.GetComponent<Slider>();
            seSlider = seBar.gameObject.GetComponent<Slider>();
            cameraSlider = cameraBar.gameObject.GetComponent<Slider>();
            bgmNum = bgmBar.transform.Find("value").gameObject.GetComponent<Text>();
            seNum = seBar.transform.Find("value").gameObject.GetComponent<Text>();
            cameraNum = cameraBar.transform.Find("value").gameObject.GetComponent<Text>();
            sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            camera = GameObject.Find("Main Camera").GetComponent<MiniMapCameraCtrl>();


            InitSetting();
            
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log(bgmSlider.normalizedValue);

            SettingBGM();
            SettingSE();
            SettingCamera();

            SaveSetting();

        }

        void OnDestroy()
        {
            //Debug.Log("Destroy Setting ");
        }

        private void InitSetting()
        {
            if(!PlayerPrefs.HasKey("BGM"))
            {
                PlayerPrefs.SetFloat("BGM", 0.5f);
            }

            if (!PlayerPrefs.HasKey("SE"))
            {
                PlayerPrefs.SetFloat("SE", 0.5f);
            }

            if (!PlayerPrefs.HasKey("Camera"))
            {
                PlayerPrefs.SetFloat("Camera", 0.5f);
            }

            float bgmV = PlayerPrefs.GetFloat("BGM");
            float seV = PlayerPrefs.GetFloat("SE");
            float cameraV = PlayerPrefs.GetFloat("Camera");

            bgmSlider.normalizedValue = bgmV;
            seSlider.normalizedValue = seV;//sound.SEVolume;
            if (camera != null) cameraSlider.normalizedValue = camera.scrollSensitivity;
            cameraSlider.normalizedValue = cameraV;


            //camera = GameObject.Find("Main Camera").GetComponent<MiniMapCameraCtrl>();
            //if (camera == null) return;
            //if (cameraV != null) camera.SetSensibility(cameraV * 2f);
        }

        private void SaveSetting()
        {   
            PlayerPrefs.SetFloat("BGM", sound.BGMVolume);
            PlayerPrefs.SetFloat("SE", sound.SEVolume);

            camera = GameObject.Find("Main Camera").GetComponent<MiniMapCameraCtrl>();
            if (camera == null) return;
            PlayerPrefs.SetFloat("Camera", camera.scrollSensitivity);
        }

        private void SettingBGM()
        {
            sound.BGMVolume = bgmSlider.normalizedValue;
            bgmNum.text = ((int)(bgmSlider.normalizedValue * 100f)).ToString();
        }

        private void SettingSE()
        {
            sound.SEVolume = seSlider.normalizedValue;
            seNum.text = ((int)(seSlider.normalizedValue * 100f)).ToString();
        }

        private void SettingCamera()
        {
            cameraNum.text = ((int)(cameraSlider.normalizedValue * 100f)).ToString();

            camera = GameObject.Find("Main Camera").GetComponent<MiniMapCameraCtrl>();
            if (camera == null) return;
            camera.SetSensibility(cameraSlider.normalizedValue*2f);
        }
    }

}

