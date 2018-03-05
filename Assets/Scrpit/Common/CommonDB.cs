using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class CommonDB
{
    //拼图信息数据库名称
    public static readonly string PuzzleInfoDB_Name = "PuzzlesInfoDB.db";

    //拼图信息数据库 拼图基本信息表
    public static readonly string PuzzleInfoDB_PuzzlesBase_Table = "puzzles_base";
    //拼图信息数据库 拼图基本信息表
    public static readonly string PuzzleInfoDB_Details_Painting_Table = "details_painting";
    public static readonly string PuzzleInfoDB_Details_Movie_Table = "details_movie";
    public static readonly string PuzzleInfoDB_Details_Celebrity_Table = "details_celebrity";

    //拼图信息数据库 BGM信息表
    public static readonly string PuzzleInfoDB_BGMInfo_Table = "sound_bgm";

    //拼图信息数据库 UI基本信息表
    public static readonly string PuzzleInfoDB_UITextBase_Table = "ui_text_base";
    //拼图信息数据库  UI内容表
    public static readonly string PuzzleInfoDB_UITextContent_Table = "ui_text";


}

