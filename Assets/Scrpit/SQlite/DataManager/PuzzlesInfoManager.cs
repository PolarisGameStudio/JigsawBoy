using System.Collections.Generic;

public class PuzzlesInfoManager
{

    /// <summary>
    /// 更新所有拼图状态为有效
    /// </summary>
    public static void UpdateAllPuzzlesToValid()
    {
        SQliteHandle.UpdateTableData
          (
          CommonDB.PuzzleInfoDB_Name,
          CommonDB.PuzzleInfoDB_PuzzlesBase_Table,
          new string[] { "valid" }, new string[] { "1" }
          );
    }

    /// <summary>
    /// 查询指定拼图信息
    /// <param name="id"></param>
    /// <returns></returns>
    public static List<PuzzlesInfoBean> LoadBasePuzzlesDataById(long id)
    {
        List<PuzzlesInfoBean> listData = new List<PuzzlesInfoBean>();
        listData = SQliteHandle.LoadTableData<PuzzlesInfoBean>
          (
          CommonDB.PuzzleInfoDB_Name,
          CommonDB.PuzzleInfoDB_PuzzlesBase_Table,
          new string[] { " id " },
          new string[] { " = " },
          new string[] { id + "" }
          );
        return listData;
    }


    /// <summary>
    /// 获取所有拼图数据
    /// </summary>
    /// <returns></returns>
    public static List<PuzzlesInfoBean> LoadAllPuzzlesData()
    {
        List<PuzzlesInfoBean> listData = new List<PuzzlesInfoBean>();
        listData = SQliteHandle.LoadTableData<PuzzlesInfoBean>
    (
    CommonDB.PuzzleInfoDB_Name,
    CommonDB.PuzzleInfoDB_PuzzlesBase_Table
    );
        return listData;
    }

    /// <summary>
    /// 获取拼图图片数据
    /// </summary>
    /// <param name="language"></param>
    /// <param name="resourcesType"></param>
    /// <returns></returns>
    public static List<PuzzlesInfoBean> LoadAllPuzzlesDataByType(JigsawResourcesEnum resourcesType)
    {
        List<PuzzlesInfoBean> listData = new List<PuzzlesInfoBean>();
        GameLanguageEnum language = CommonConfigure.GameLanguage;

        string detailsTableName = "";
        if (resourcesType.Equals(JigsawResourcesEnum.Painting))
            detailsTableName = CommonDB.PuzzleInfoDB_Details_Painting_Table;
        else if (resourcesType.Equals(JigsawResourcesEnum.Movie))
            detailsTableName = CommonDB.PuzzleInfoDB_Details_Movie_Table;
        else if (resourcesType.Equals(JigsawResourcesEnum.Celebrity))
            detailsTableName = CommonDB.PuzzleInfoDB_Details_Celebrity_Table;
        else if (resourcesType.Equals(JigsawResourcesEnum.Other))
            detailsTableName = CommonDB.PuzzleInfoDB_Details_Other_Table;
        else if (resourcesType.Equals(JigsawResourcesEnum.Animal))
            detailsTableName = CommonDB.PuzzleInfoDB_Details_Animal_Table;
        else if (resourcesType.Equals(JigsawResourcesEnum.Scenery))
            detailsTableName = CommonDB.PuzzleInfoDB_Details_Scenery_Table;
        else if (resourcesType.Equals(JigsawResourcesEnum.Food))
            detailsTableName = CommonDB.PuzzleInfoDB_Details_Food_Table;
        else if (resourcesType.Equals(JigsawResourcesEnum.StarrySky))
            detailsTableName = CommonDB.PuzzleInfoDB_Details_StarrySky_Table;
        else
            return null;

        if (language.Equals(GameLanguageEnum.Chinese))
            detailsTableName += "_cn";
        else if (language.Equals(GameLanguageEnum.English))
            detailsTableName += "_en";

        listData = SQliteHandle.LoadTableData<PuzzlesInfoBean>
            (
            CommonDB.PuzzleInfoDB_Name,
            CommonDB.PuzzleInfoDB_PuzzlesBase_Table,
            new string[] { detailsTableName },
            "id",
            new string[] { "puzzles_id" },
            new string[] { "data_type" },
            new string[] { "=" },
            new string[] { (int)resourcesType + "" }
            );

        return listData;
    }
}