# namespace Pieceの利用と駒の追加
## IPiecesManagerの利用法  
1. 前準備  
PieceManagerを適当なオブジェクトにアタッチしておく。  
Resourceフォルダ内に、以下の条件を満たす駒のprefabを配置しておく。
   - Piecesのアタッチし、自身をpublicなオブジェクトとして持つ  
   - AudioListnerを削除したカメラを子として持つ  
2. インスタンスの作成  
 GetComponentでPieceManagerがアタッチされたGameObjectからインスタンスを作成。  
 public GameObject PMobj;//とりあえずpuliceにしてある  
 Piece.PiecesManager PM = PMobj.GetComponent<Piece.PiecesManager>();  
3. メソッドの実行  
   - 駒の呼び出し()
  Pieces GetPieceById(int pieceId);
  PieceはMoveやRotateをメソッドとして持つ
   - 駒の作成
  作成した駒のIdを戻り値として返す
  int GeneratePiece(String pieceKind, int absoluteFaceId);
   - 駒のコスト参照
  float GetSummonCost(String pieceKind);
   - 駒の移動範囲参照
  Dictionary<int, List<int>> GetMoveRange(string pieceKind);

## 駒の追加
1. PieceInfoを継承したクラスを作成。(PieceInfoフォルダ参照)
2. PieceManager内のawakeでAllPieceInfoにインスタンスを追加。
3. プレハブ化した駒にPieceInfoをアタッチし、自身をpublicなオブジェクトして指定。
4. カメラを子要素として持たせ、AudioListerを削除する
