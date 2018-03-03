using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData {

    //是否能拖拽物体
    public static bool IsDargMove = false;
    //游戏语言
    public static GameLanguageEnum GameLanguage=GameLanguageEnum.Chinese;

    //选择的拼图信息
    public static PuzzlesInfoBean SelectPuzzlesInfo;

    //拼图信息数据库名称
    public static readonly string PuzzleInfoDB_Name = "PuzzlesInfoDB.db";
    //拼图信息数据库 拼图基本信息表
    public static readonly string PuzzleInfoDB_PuzzlesBase_Table = "puzzles_base";

    //拼图信息数据库 拼图基本信息表
    public static readonly string PuzzleInfoDB_Details_Painting_Table = "details_painting";
    public static readonly string PuzzleInfoDB_Details_Movie_Table = "details_movie";
    public static readonly string PuzzleInfoDB_Details_Celebrity_Table = "details_celebrity";
}
