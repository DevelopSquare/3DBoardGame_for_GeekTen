using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Player;
using Piece;
using InputID;
using GameManager;
using System;

namespace GameManager
{
    namespace ListManager {

        public class ListCtrl : MonoBehaviour,IListManager
        {
            [Header("駒をListから操作するときボタンを入れる")]
            /*駒をListから操作するときボタンを入れる*/
            [SerializeField] GameObject[] buttons = default;

            [Header("Pawnのボタンを格納するパネル")]
            /*Pawnのボタンを格納するパネル*/
            [SerializeField] GameObject PawnPanel = default;

            [Header("Kingのボタンを格納するパネル")]
            /*Kingのボタンを格納するパネル*/
            [SerializeField] GameObject KingPanel = default;

            [Header("Queenのボタンを格納するパネル")]
            /*Queenのボタンを格納するパネル*/
            [SerializeField] GameObject QueenPanel = default;

            //public PiecesManager pieceManager;

            private GameObject MiniMapCameraObj=default;
            private UnityEngine.Camera MiniMapCamera = default;

            private List<Pieces> EnabledPiecesId = new List<Pieces>();
            private List<Pieces> EnabledPiecesIdPast = new List<Pieces>();

            private PlayerBase player;
            private InputEvent input= new TouchEvent();
            private List<PieceBotton> pieceBottons=new List<PieceBotton>();
           //private PieceBotton[] pieceBottonsPast;
            
            

            // Start is called before the first frame update
            void Start()
            {
                //init();

                MiniMapCamera = UnityEngine.Camera.main; //MiniMapCameraObj.GetComponent<UnityEngine.Camera>();
            }

            // Update is called once per frame
            void Update()
            {
                

                EnabledPiecesId = ManagerStore.humanPlayer.GetMyPieces();// new List<int>(l);

                
                //Debug.Log("Length of enabled piece  " + EnabledPiecesId.Count);
                //Debug.Log("Length of enabled piece past  " + EnabledPiecesIdPast.Count);


                //Debug.Log(String.Join(",",pieceBottons));
            }

            public List<Pieces> EnumPieces()
            {
                
                return EnabledPiecesId;
            }

            public int GetSelectedPieces()
            {
                /*ListManager Default*/

                //var selectedPieces = new List<Pieces>();
                //for(int i = 0; i<pieceBottons.Count(); i++)
                //{



                //    if (pieceBottons[i].PushedObject() >= 0)
                //    {

                //        selectedPieces.Add(pieceBottons[i].GetPiece());
                //        if (EnabledPiecesId.IndexOf(selectedPieces[selectedPieces.Count - 1])>=0)
                //        {
                //            //Debug.Log("Puehed !!!   " + selectedPieces[selectedPieces.Count - 1]);
                //            return selectedPieces[selectedPieces.Count-1].GetPieceId();
                //           // break;
                //        }

                //    }


                //}
                //return -1;
                if (!Input.GetMouseButtonUp(0)) return -1;
                Ray ray = MiniMapCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                Physics.queriesHitBackfaces = true;

                if (Physics.Raycast(ray, out hit))
                {

                    var pieces =hit.transform.gameObject.GetComponent<Pieces>();
                    //Debug.Log("GetSelected Piecs : "+ hit.transform.gameObject);

                    if (pieces == null) return -1;


                    int pieceId = pieces.GetPieceId();
                    if (!ManagerStore.humanPlayer.HasPiece(pieceId)) return -1;
                    
                    return pieceId;
                                    }
                else
                {
                    return -1;
                }
                    
                

            }

            public void DisplayList()
            {
//                Debug.Log("Display  : " +String.Join(",",EnabledPiecesId));


                var pieceList = new List<int>();// new List<PieceButton>();
                for (int i = 0; i < EnabledPiecesId.Count(); i++)
                {
                    if (EnabledPiecesIdPast.IndexOf(EnabledPiecesId[i]) < 0 && EnabledPiecesId[i]!=null)//EnabledPiecesIdPast.IndexOf(EnabledPiecesId[i]) < 0ManagerStore.humanPlayer.HasPiece(EnabledPiecesId[i].GetPieceId())
                    {
                        //Debug.Log(EnabledPiecesId.IndexOf(EnabledPiecesId[i]));
                        //pieceList[i] = EnabledPiecesId[EnabledPiecesIdPast.Count() + i];

                        AddBotton(EnabledPiecesId[i]);
                    }

                }

                for (int i = 0; i < EnabledPiecesIdPast.Count(); i++)
                {
                    if (EnabledPiecesId.IndexOf(EnabledPiecesIdPast[i]) < 0 && EnabledPiecesIdPast[i]!=null)
                    {
                        //Debug.Log("Deleted Piece Button!");
                        DeleteButton(EnabledPiecesIdPast[i]);
                    }
                }

                //var addButton =;


            }

           

            private void init()
            {
                var objs = GameObject.FindGameObjectsWithTag("PieceBotton");
                for (int i = 0; i < objs.Length; i++)
                {
                    pieceBottons.Add( objs[i].GetComponent<PieceBotton>());
                }
            }

            private void AddBotton(Pieces pieces)
            {
                var piece = pieces;//ManagerStore.piecesManager.GetPieceById(pieceId);
                var pieceKind = piece.GetKind();

                GameObject panel = default;

                GameObject button;

                //Debug.Log(pieceKind);
                switch (pieceKind)
                {
                    case PieceKind.Pawn:
                        button= Instantiate(buttons[0]);
                        panel = PawnPanel;
                        break;
                    case PieceKind.King:
                        button = Instantiate(buttons[1]);
                        panel = KingPanel;
                        break;
                    case PieceKind.Queen:
                        button = Instantiate(buttons[2]);
                        panel = QueenPanel;
                        break;
                    default:
                        Debug.LogWarning("No some kind of pieces.");
                        return ;
                        //break;
                }

                var piecebutton = button.GetComponent<PieceBotton>();
                piecebutton.PieceId = pieces.GetPieceId();
                piecebutton.Piece = piece;

                pieceBottons.Add(piecebutton);

                button.transform.SetParent(panel.transform, false);

                var l = new List<int>();
                l.Add(piecebutton.PieceId );

                


                //ManagerStore.player.AddMyPieces(pieceKind,l);
                
                EnabledPiecesIdPast.Add(piece);
                


            }

            private void DeleteButton(Pieces pieces)
            {
                for(int i=0; i < pieceBottons.Count(); i++)
                {
                    int crrPieceId = pieceBottons[i].PieceId;
                    //Debug.Log(pieces);
                    
                    if (crrPieceId== pieces.GetPieceId())
                    {
                        
                        pieceBottons[i].Delete();
                        ManagerStore.humanPlayer.DeleteMyPieces(pieceBottons[i].PieceId);

                        EnabledPiecesIdPast.Remove(pieceBottons[i].GetPiece());
                        pieceBottons.Remove(pieceBottons[i]);

                        //Debug.Log(EnabledPiecesId);
                        //Debug.Log("Deleted Piece Button!" + pieceId);

                        return;
                    }
                }
            }

        }

    }

}

