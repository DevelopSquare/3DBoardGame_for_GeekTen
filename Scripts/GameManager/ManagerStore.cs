/*
  Author      田中木介,小路重虎
  LastUpdate  2020/04/13
  Since       2020/03/30
  Contents    ManagerStore
              シーン間での情報共有用staticクラス
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Field;
using Piece;

namespace GameManager
{
    public class ManagerStore : MonoBehaviour
    {
        public static PlayerBase humanPlayer;
        public static PlayerBase cp;
        public static FieldManager fieldManager;
        public static PiecesManager piecesManager;

        void Awake()
        {
           // Debug.Log("Awake");
            fieldManager = transform.Find("FieldManager").GetComponent<FieldManager>();
            piecesManager = transform.Find("PiecesManager").GetComponent<PiecesManager>();
            cp = transform.Find("CP").GetComponent<PlayerBase>();
            humanPlayer = transform.Find("HumanPlayer").GetComponent<PlayerBase>();

            cp.SetPlayerKind(PlayerKind.CP);
            humanPlayer.SetPlayerKind(PlayerKind.HumanPlayer);

            if (cp == null) Debug.Log("null");

        }


    }
}
