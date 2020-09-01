/*
  Author      森拓哉
  LastUpdate  2020/04/06
  Since       2020/03/23
  Contents     SetInfo()でCPMoveクラスのdecisionインスタンスを生成
  戦略関数でCPMoveクラスのpreDecisionインスタンスに値を代入
  finalSet()でdecisionインスタンスに値を代入し、BestMove()で送るインスタンスを最終決定
  BestMove()でターン内の行動を返す
  SetCPPieces()でPawn10体を召喚する
*/

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Piece; // 20/04/06追加
using Player;
using Field;
using System.Linq; //list.Count() 
using GameManager;
using System.Runtime.Serialization;
using Piece.Extend;
namespace CP
{
    class CPBrain : MonoBehaviour, ICP
    {

        public CPMove decision = new CPMove();
        public CPMove preDecision = new CPMove();
        public int num = 0;// for debug(num = id -1)
        int check = 1;
        List<List<int>> kingOrderList = new List<List<int>>();
        List<List<int>> queenOrderList = new List<List<int>>();
        List<List<int>> pawnOrderList = new List<List<int>>();

        private CPAgent agent;

        private Pieces p_movedpieces;
        private Pieces cp_movedpieces;


        void Start()
        {
            Debug.Log("CPBrain Started");
            agent = GetComponent<CPAgent>();
            SetCPPieces();
            var human_player_pieces = ManagerStore.humanPlayer.GetMyPieces();
            var cp_player_pieces = ManagerStore.cp.GetMyPieces();
            p_movedpieces = human_player_pieces[0];//human_player_pieces[UnityEngine.Random.Range(0,human_player_pieces.Count-1)];
            cp_movedpieces = cp_player_pieces[0];//cp_player_pieces[UnityEngine.Random.Range(0,cp_player_pieces.Count-1)];
        }

        // set value to desicision(CPMove)
        private void FinalSet(int moveFaceId, int movePieceId,int isRotate)
        {
            decision.SetMoveFaceId(moveFaceId);
            decision.SetMovePieceId(movePieceId);
            decision.SetRotateDirection(isRotate);
            //Debug.Log("Final Setted : " + moveFaceId+" : "+movePieceId+" : "+isRotate) ;
        }

        // return CP's movement
        public CPMove BestMove()
        {
            //Debug.Log("decision : "+decision.GetMovePieceId());
            return decision;
        }

        public void SetInfo(PlayerBase cp, FieldManager field)
        {
            //TestStrategy(cp, field);

            //maybe can't use
            //  makeOrder(cp, field); // it has some bugs.
            //choiceOrder();


            /*　check all pieces
            foreach (Pieces check in cpList)
            {
                checkPieceProperty(field, check);
            }

            */

            //int num1 = 0; //moving
            //int num2 = 2; //purpose
            //int result = 0;
            //List<Pieces> cpList = cp.GetMyPieces();
            //foreach(Pieces piece in cpList)
            //{
            //    num1++;
            //    if(num1 == num2)
            //    {
            //        Debug.Log("Rotate!!");
            //        rotatePiece(field, piece, 1);
            //        if (result!=0)
            //        {
            //            num2++;
            //        }
            //    }
            //}
            /*
            foreach (Pieces piece in cpList)
            {
                result = 0;
                result = circleChainCheck(piece.GetFaceId());
                Debug.Log("aaaaaaa");
                Debug.Log(piece.GetPieceId());
                Debug.Log(result);
                Debug.Log("bbbbbbbb");
                
                if (result != 0)
                {
                    if (piece.GetKind().ToString().Equals("King"))
                    {
                        Debug.Log("KING");
                    }
                    debugPiecesInfo(piece);
                    Debug.Log("uruboros");
                    Debug.Log(result);
                    Debug.Log("-------");
                }
                
            }
            */

            /*--- Set Observation ---*/
            var cp_player = ManagerStore.cp;
            var player = ManagerStore.humanPlayer;
            var fieldManager = ManagerStore.fieldManager;



            var info = new List<float>();

            /*Set Info as [持ち駒数, 動いた駒のId,動いた駒の向いているマスのIｄ,敵の持ち駒,敵の動いた駒のId,敵の駒の向いているマス]*/
            //テストようにランダムに動かす
            if (true || player == null || cp_player == null || p_movedpieces==null || cp_movedpieces == null)
            {

                var cp__ = cp_player.GetMyPieces();
                var piece__ = cp__[UnityEngine.Random.Range(0, cp__.Count())];
                var piece_vFace = ManagerStore.piecesManager.GetMoveRange( piece__.GetKind())[piece__.GetShape()];
                var face__ = UnityEngine.Random.Range(0,piece_vFace.Count());

                var absFaceId_ = GameManager.ManagerStore.fieldManager.ConvertRelative2AbsId(piece__.GetFaceId(), piece_vFace[face__], piece__.GetForwardDirection());

                //Queenの二週目の挙動を無理やり矯正(kisuke)
                if (piece__.GetKind() == PieceKind.Queen && face__ == (piece__.GetShape()))
                {
                    var fFaceId_ = piece__.GetForwardFaceId();
                    var ffFace_ = ManagerStore.fieldManager.GetFace2Face(fFaceId_, piece__.GetFaceId());
                    absFaceId_ = ffFace_.GetComponent<Field.SurfaceInfo>().FaceId;

                }

                FinalSet(absFaceId_, piece__.GetPieceId(), 0);
                return;
            }
            info.Add(player.GetMyPieces().Count);
            info.Add(p_movedpieces.GetFaceId());
            info.Add(p_movedpieces.GetForwardFaceId());
            info.Add(cp_player.GetMyPieces().Count);
            info.Add(cp_movedpieces.GetFaceId());
            info.Add(cp_movedpieces.GetForwardFaceId());
            agent.SetObserVation(info);

            /*--- Get Action ---*/

            var act = agent.GetActionVector();

            int controlPieceId = (int)(Mathf.Clamp(act[0] * 11f, 0, player.GetMyPieces().Count - 1));
            int isRotate = (int)Mathf.Clamp(act[1], 0, 2);
            int movableFaceId = (int)(Mathf.Clamp(act[2] * 10f, 0, 9));

            var piece = cp_player.GetMyPieces()[controlPieceId];
            var pieceId = piece.GetPieceId();
            var absFaceId = fieldManager.ConvertRelative2AbsId(piece.GetFaceId(), movableFaceId, piece.GetForwardDirection());

            if (isRotate == 1)
            {
                isRotate = 1;
            }
            else if(isRotate == 2)
            {
                isRotate = -1;
            }
            Debug.Log("安全確認、ヨシ！");
            // Debug.Log(faceSaferyCheck(1,preDecision.GetMoveFaceId(), cp, field)); 
            p_movedpieces = piece;
            FinalSet(absFaceId, pieceId, isRotate);
        }

        //とりあえずPawnをずらっと
        public void SetCPPieces()
        {
            int summonCount = 1;
            //絶対idのリストを返す
            List<int> SetFaces = GameManager.ManagerStore.fieldManager.GetPieceCPSettableFace();
            //Debug.Log(String.Join(",", SetFaces));
            // Debug.Log("以下FaceId");
            foreach (int setFaceId in SetFaces)
            {
                //king 設置　04/08現在kingを生成するとカメラの挙動がおかしくなるのでやめときます
                if (summonCount == 1)
                {
                    GameManager.ManagerStore.cp.AddMyPieces(PieceKind.King, setFaceId, 50);
                    summonCount++;
                }
                else
                {
                    GameManager.ManagerStore.cp.AddMyPieces(PieceKind.Pawn, setFaceId, ManagerStore.fieldManager.GetFace2Face(setFaceId,50).GetComponent<SurfaceInfo>().FaceId);
                }
            }
        }

        public void SetAgentCPPieces(int i)
        {
            int summonCount = 1;
            //絶対idのリストを返す
            List<int> SetFaces = GameManager.ManagerStore.fieldManager.GetAdjacentSurface(i, Vector3.forward);
            Debug.Log(String.Join(",", SetFaces));
            // Debug.Log("以下FaceId");
            foreach (int setFaceId in SetFaces)
            {
                //king 設置　04/08現在kingを生成するとカメラの挙動がおかしくなるのでやめときます
                if (summonCount == 1)
                {
                    GameManager.ManagerStore.cp.AddMyPieces(PieceKind.King, setFaceId, 0);
                    summonCount++;
                }
                else
                {
                    GameManager.ManagerStore.cp.AddMyPieces(PieceKind.Pawn, setFaceId, 0);
                }
            }
        }

        // return front faceId 
        private int frontFaceIdCheck(FieldManager field, Pieces piece)
        {
            //何角形のますにいるかをint型で返す
            int faceShape =  piece.GetShape();
            int frontFaceId = -1;
            //辞書型配列で受け取る
            Dictionary<int, List<int>> MovableRange = GameManager.ManagerStore.piecesManager.GetMoveRange(piece.GetKind());
            //辞書型配列で受け取った稼働範囲からforeachでそれぞれ検証
            List<int> movableFaces = MovableRange[faceShape];
            foreach (int movableFace in movableFaces)
            {
                //候補の面の相対idを絶対idに変換
                frontFaceId = field.ConvertRelative2AbsId(piece.GetFaceId(), movableFace,  piece.GetForwardDirection());
                break;
            }
            return frontFaceId;
        }

        // return circle faceId 
        private List<int> circleFaceIdCheck(FieldManager field, Pieces piece)
        {
            //何角形のますにいるかをint型で返す
            int faceShape = piece.GetShape();
            List<int> circleFaceIdCheck = new List<int>();
            //辞書型配列で受け取る
            Dictionary<int, List<int>> MovableRange = GameManager.ManagerStore.piecesManager.GetMoveRange(piece.GetKind());
            //辞書型配列で受け取った稼働範囲からforeachでそれぞれ検証
            List<int> movableFaces = MovableRange[faceShape];
            foreach (int movableFace in movableFaces)
            {
                //候補の面の相対idを絶対idに変換
                circleFaceIdCheck.Add(field.ConvertRelative2AbsId(piece.GetFaceId(), movableFace, piece.GetForwardDirection()));
                
            }
            return circleFaceIdCheck;
        }

        // rotate selected piece
        private　void rotatePiece(FieldManager field,Pieces piece,int degree)
        {
            piece.RotatePiece(degree);
            preDecision.SetMovePieceId(piece.GetPieceId());
            preDecision.SetMoveFaceId(piece.GetFaceId());
        }

        // return Pieces from faceId
        // faceId needs the piece
        private Pieces faceIdtoPieces(int faceId)
        {
            int a = GameManager.ManagerStore.fieldManager.IsPieceOnFace(faceId);
            if(a!=-1)
            {
               if( GameManager.ManagerStore.cp.HasPiece(a))
               {
                   return  GameManager.ManagerStore.cp.GetPieceById(a);
                }
                else{
                    return GameManager.ManagerStore.humanPlayer.GetPieceById(a);
                }
            }
            Debug.Log("Error");
            return null;
        }

        // return whoseTroop
        private bool MyTroop(Pieces piece)
        {
            if (GameManager.ManagerStore.cp.HasPiece(piece.GetPieceId()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /* for reward
         *  private void String2String(FieldManager field,Pieces piece,int string2string)
         *  private void confirmFace(FieldManager field, int faceId, PlayerBase cp, PlayerBase player)
         */



        //investigate the targetPiece whetever it look to sameface from faceId
        //return value
        private int lookingToSameFace(int purposeFaceId,int targetFaceId)
        {
            Pieces piece = faceIdtoPieces(targetFaceId);
            int unit = 0;
            int value = 1;

            if(MyTroop(piece))
            {
                unit = 1;
            }
            else 
            {
                unit = -1;
            }
            if(piece.GetKind().ToString().Equals("King"))
            {
                return 0;
            }else if(frontFaceIdCheck(GameManager.ManagerStore.fieldManager, piece) == purposeFaceId)
            {
                Debug.Log("purposePieceId");
                Debug.Log(faceIdtoPieces(purposeFaceId).GetPieceId());
                Debug.Log("guardPiece");
                Debug.Log(piece.GetPieceId());
                return value*unit;
            }
            else
            {
                return 0;
            }
        }

        // analyse circleEnvironment and return value ;
        int debugcheck = 0;
        private int circleChainCheck(int faceId)
        {
//           Debug.Log(faceIdtoPieces(faceId).GetPieceId());
           int value=0;
           List<int> environment = GameManager.ManagerStore.fieldManager.GetAdjacentSurface(faceId, Vector3.forward);
           foreach(int face in environment)
           {
                int pieceId = GameManager.ManagerStore.fieldManager.IsPieceOnFace(face);
                if (pieceId != -1)
                {
                    value += lookingToSameFace(faceId, face);
                }
           }
           if(debugcheck != value)
            {
                Debug.Log("value");
                Debug.Log(value);
                Debug.Log("----");
            }
            
            return value;
        }


        //Debug
        private void debugPiecesInfo(Pieces piece)
        {
            /*
            Debug.Log("my Kind is");
            Debug.Log(piece.GetKind());
            Debug.Log("my pieceId is");
            Debug.Log(piece.GetPieceId());
            Debug.Log("onTheFace");
            Debug.Log(piece.GetFaceId());
            */
            if (piece.GetKind().ToString().Equals("Pawn"))
            {
                int frontFaceId = frontFaceIdCheck(GameManager.ManagerStore.fieldManager, piece);
                /*
                Debug.Log("frontface");
                Debug.Log(frontFaceId);
                */
            }
            if (piece.GetKind().ToString().Equals("King"))
            {
                List<int> CircleList = circleFaceIdCheck(GameManager.ManagerStore.fieldManager, piece);
                foreach (int frontFaceId in CircleList)
                {
                    Debug.Log(frontFaceId);
                }
            }
            Debug.Log(" ------ ");
        }
        // for Debug?
        private void debugarmyDefense(PlayerBase cp)
        {
            Debug.Log("snake");
            int armyDefenseValue = 0;
            List<Pieces> a = cp.GetMyPieces();
            foreach(Pieces piece in a)
            {
               armyDefenseValue += circleChainCheck(piece.GetFaceId());
            }
            Debug.Log(armyDefenseValue);
            Debug.Log("SNAKE EATER");
        }

        
        

        //kingフラフラストラテギー
        private void TestStrategy(PlayerBase cp, FieldManager field)
        {
            int count = 0;
            //pieceリストから先頭を取得
            List<Pieces> CpList = cp.GetMyPieces();

            foreach (Pieces candidate in CpList)
            {
                //何角形のますにいるかをint型で返す
                int faceShape = candidate.GetShape();

                //辞書型配列で受け取る
                Dictionary<int, List<int>> MovableRange = GameManager.ManagerStore.piecesManager.GetMoveRange(candidate.GetKind());

                //辞書型配列で受け取った稼働範囲からforeachでそれぞれ検証
                List<int> movableFaces = MovableRange[faceShape];

                foreach (int movableFace in movableFaces)
                {
                    //候補の面の相対idを絶対idに変換
                    int candidateFace = field.ConvertRelative2AbsId(candidate.GetFaceId(), movableFace, candidate.GetForwardDirection());
                    check = field.IsPieceOnFace(candidateFace);
                    if (check == -1)
                    {
                 //       Debug.Log("敵艦見ゆ?");
                        preDecision.SetMovePieceId(candidate.GetPieceId());//移動する駒のid
                        preDecision.SetMoveFaceId(candidateFace);//移動先の絶対id
                        count++;
                        break;
                    }
                    if(candidate.GetKind().ToString().Equals("Pawn"))
                    {
                        break;
                    }
                }
                if(count!=0)
                {
                    break;
                }
            }
        }

        
    }
}




/* maybe can't use
 // 進軍先と評価値をまとめたリストを作成
        private void makeOrder(PlayerBase cp, FieldManager field)
        {
            kingOrderList.Clear();
            queenOrderList.Clear();
            pawnOrderList.Clear();

            List<Pieces> CpList = cp.GetMyPieces();
            Debug.Log("pieceId");
            foreach (Pieces candidate in CpList)
            {
                //Debug.Log(candidate.GetPieceId());
                //Debug.Log(candidate.GetFaceId());
            }
            List<Pieces> PlayerList = GameManager.ManagerStore.humanPlayer.GetMyPieces(); ;
            foreach (Pieces candidate in CpList)
            {
                //何角形のますにいるかをint型で返す
                int faceShape = candidate.GetShape();
                //辞書型配列で受け取る
                Dictionary<int, List<int>> MovableRange = GameManager.ManagerStore.piecesManager.GetMoveRange(candidate.GetKind());
                //辞書型配列で受け取った稼働範囲からforeachでそれぞれ検証
                List<int> movableFaces = MovableRange[faceShape];
                foreach (int movableFace in movableFaces)
                {
                    int value = 0;
                    //候補の面の相対idを絶対idに変換
                    int candidateFace = field.ConvertRelative2AbsId(candidate.GetFaceId(), movableFace, candidate.GetForwardDirection());
                    check = field.IsPieceOnFace(candidateFace);
                    if (check != -1)
                    {
                        if (cp.HasPiece(check))
                        {
                            value = -1;
                        }
                        else
                        {
                            value = 1;
                            //以下駒の情報によって評価値を変化
                            foreach (Pieces humanPiece in PlayerList)
                            {
                                if (check == humanPiece.GetPieceId() && humanPiece.GetKind().ToString().Equals("King"))
                                {
                                    value = 5;
                                }
                                if (check == humanPiece.GetPieceId() && humanPiece.GetKind().ToString().Equals("Queen"))
                                {
                                    value = 3;
                                }
                            }
                        }
                    }
                    else if (check == -1)
                    {
                        value = 0;
                    }

                    List<int> order = new List<int>() { value, candidate.GetPieceId(), candidateFace };
                    if (candidate.GetKind().ToString().Equals("King"))
                    {
                        kingOrderList.Add(order);
                    }
                    else if (candidate.GetKind().ToString().Equals("Queen"))
                    {
                        queenOrderList.Add(order);
                    }
                    else if (candidate.GetKind().ToString().Equals("Pawn"))
                    {
                        pawnOrderList.Add(order);
                        break;
                    }
                }
            }
        }

    private void choiceOrder()
        {
            int value = -1;
            for(int i = 0;i<kingOrderList.Count();i++)
            {
                if(i == 0)
                {
                    value = kingOrderList[i][0];
                    preDecision.SetMovePieceId(kingOrderList[i][1]);
                    preDecision.SetMoveFaceId(kingOrderList[i][2]);
                }
                 if(value < kingOrderList[i][0])
                 {
                    value = kingOrderList[i][0];
                    preDecision.SetMovePieceId(kingOrderList[i][1]);
                    preDecision.SetMoveFaceId(kingOrderList[i][2]);
                 }
            }

            for (int i = 0; i < queenOrderList.Count(); i++)
            {
                if (value < queenOrderList[i][0])
                {
                    value = queenOrderList[i][0];
                    preDecision.SetMovePieceId(queenOrderList[i][1]);
                    preDecision.SetMoveFaceId(queenOrderList[i][2]);
                }
            }

            for (int i = 0; i < pawnOrderList.Count(); i++)
            {
                if (value < pawnOrderList[i][0])
                {
                    value = pawnOrderList[i][0];
                    preDecision.SetMovePieceId(pawnOrderList[i][1]);
                    preDecision.SetMoveFaceId(pawnOrderList[i][2]);
                }
                
                if(pawnOrderList[i][0] !=  -1)
                {
                    //Debug.Log("value");
                    //Debug.Log(pawnOrderList[i][0]);
                    //Debug.Log("pieceId");
                    //Debug.Log(pawnOrderList[i][1]);
                }
                
            }
        }

    private (int, int) faceSaferyCheck(int checkCount, int candidateFaceId, PlayerBase cp, FieldManager field)
        {
            int count = 1; int candidateFace; int enemyCount = 0;
            int risk = 0;
            int chain = 1;
            List<Pieces> PlayerList = GameManager.ManagerStore.humanPlayer.GetMyPieces(); ;
            if (candidateFaceId < 30)
            {
                candidateFace = 4;
            }
            else if (candidateFaceId < 50)
            {
                candidateFace = 6;

            }
            else
            {
                candidateFace = 10;
            }
            List<int> safetyCheckList = field.GetAdjacentSurface(candidateFaceId, Vector3.forward);
            for (int j = 0; j < candidateFace; j++)
            {
                int currentCheckId = safetyCheckList[j];
                int unknownpieceId = field.IsPieceOnFace(currentCheckId);
                if (unknownpieceId != -1 && cp.HasPiece(unknownpieceId) == false)
                {
                    enemyCount++;
                    foreach (Pieces humanPieces in PlayerList)
                    {
                        if (unknownpieceId == humanPieces.GetPieceId())
                        {
                            //何角形のますにいるかをint型で返す
                            int faceShape = humanPieces.GetShape();
                            //辞書型配列で受け取る
                            Dictionary<int, List<int>> MovableRange = GameManager.ManagerStore.piecesManager.GetMoveRange(humanPieces.GetKind());

                            //辞書型配列で受け取った稼働範囲からforeachでそれぞれ検証
                            List<int> movableFaces = MovableRange[faceShape];

                            foreach (int movableFace in movableFaces)
                            {
                                //候補の面の相対idを絶対idに変換
                                int check = field.ConvertRelative2AbsId(humanPieces.GetFaceId(), movableFace, humanPieces.GetForwardDirection());
                                if (check == candidateFaceId)
                                {
                                    risk++;
                                }
                            }
                        }
                    }

                    if (count < checkCount)
                    {
                        chain++;
                        faceSaferyCheck(checkCount - 1, candidateFaceId, cp, field);
                    }
                }
            }
            Debug.Log("risk");
            Debug.Log(risk);
            return (risk, chain);
        }
        */

