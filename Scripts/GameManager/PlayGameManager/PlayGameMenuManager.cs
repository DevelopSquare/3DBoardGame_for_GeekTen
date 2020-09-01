/*
  Author      田中木介
  LastUpdate  2020/04/13
  Since       2020/04/13
  Contents    PlayGamenのMenuの制御

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using GameManager.ListManager;
using System;

namespace GameManager.PlayGame
{
    public class PlayGameMenuManager : MonoBehaviour
    {
        private ListCtrl listCtrl;

        [SerializeField] GameObject[] PieceContainers = default;
        [SerializeField] GameObject ListMenu = default;


        // Start is called before the first frame update
        void Start()
        {
            listCtrl = GetComponent<ListCtrl>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }



        public void Menu2Pawn()
        {
            listCtrl.DisplayList();

            TurnMenu(false);

            PieceContainers[0].SetActive(true);
        }

        public void Menu2King()
        {
            listCtrl.DisplayList();

            TurnMenu(false);

            PieceContainers[1].SetActive(true);
        }

        public void Menu2Queen()
        {
            listCtrl.DisplayList();

            TurnMenu(false);

            PieceContainers[2].SetActive(true);

        }

        public void BackMenu()
        {
            TurnMenu(false);

            ListMenu.SetActive(true);
        }

        private void TurnMenu(bool isActive)
        {
            ListMenu.SetActive(isActive);
            foreach(GameObject panel in PieceContainers)
            {
                panel.SetActive(isActive);
            }
        }
    }

}
