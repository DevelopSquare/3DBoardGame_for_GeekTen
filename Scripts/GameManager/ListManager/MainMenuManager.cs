using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    //4つのPanelを格納する変数
    //インスペクターウィンドウからゲームオブジェクトを設定する
    [SerializeField] GameObject MainPanel = default;
    [SerializeField] GameObject PawnPanel = default;
    [SerializeField] GameObject QueenPanel = default;
    [SerializeField] GameObject KnightPanel = default;
    [SerializeField] GameObject BishopPanel = default;


    // Start is called before the first frame update
    void Start()
    {
        //BackToMenuメソッドを呼び出す
        BackToMenu();
    }



    public void SelectPawn()
    {
        PawnPanel.SetActive(true);
        MainPanel.SetActive(false);
    }


    
    public void SelectQueen()
    {
        QueenPanel.SetActive(true);
        MainPanel.SetActive(false);
    }

    public void SelectKnight()
    {
        KnightPanel.SetActive(true);
        MainPanel.SetActive(false);
    }

    public void SelectBishop()
    {
        BishopPanel.SetActive(true);
        MainPanel.SetActive(false);
    }



    public void BackToMenu()
    {
        MainPanel.SetActive(true);
        QueenPanel.SetActive(false);
        KnightPanel.SetActive(false);
        PawnPanel.SetActive(false);
        BishopPanel.SetActive(false);
    }
}
