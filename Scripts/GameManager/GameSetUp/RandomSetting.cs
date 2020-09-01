/*
  Author      森友雅
  LastUpdate  2020/06/01
  Since       2020/05/32
  Contents    N GameSetUp
            　お任せで駒を設置する際に利用
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager.GameSetUp
{
    public class RandomSetting : MonoBehaviour
    {
        private SetObjectManager setobject;
        private GameSetUp gamesetup;
        GetInfoManager getinfo = new GetInfoManager();
        private int[] settableSquare = { 21, 22, 23, 24, 25, 45, 46, 47, 48, 49 };
        private int pawn;
        private int queen;
        private int point;

        void Start()
        {
            setobject = GetComponent<SetObjectManager>();
            gamesetup = GetComponent<GameSetUp>();
            pawn = (int)ManagerStore.piecesManager.GetSummonCost(PieceKind.Pawn);
            queen = (int)ManagerStore.piecesManager.GetSummonCost(PieceKind.Queen);
        }


        public void OnClick()
        {
            setobject.RemoveAllPieces();
            point = 100;
            for(int i=0;i<100;i++)
            {
                int squareValue = settableSquare[Random.Range(0, 10)];

                if (ManagerStore.fieldManager.IsPieceOnFace(squareValue) == -1)
                {
                    int pieceValue = Random.Range(1, 5);

                    if (pieceValue == 1 && point-queen >= 0)
                    {
                        setobject.SetPiece(squareValue, -5);
                        point -= getinfo.PiecePoint(-5);
                    }
                    else if(point-pawn >= 0)
                    {
                        setobject.SetPiece(squareValue, -2);
                        point -= getinfo.PiecePoint(-2);
                    }

                    if(point == 0)
                    {
                        i = 100;
                        gamesetup.WritePoint(point);
                        
                    }
                }
            }

            gamesetup.WritePoint(point);
        }

    }
}