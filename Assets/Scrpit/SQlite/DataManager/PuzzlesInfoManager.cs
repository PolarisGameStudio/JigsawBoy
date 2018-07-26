using System.Collections.Generic;

public class PuzzlesInfoManager
{
    /// <summary>
    /// 获取拼图图片数据
    /// </summary>
    /// <param name="language"></param>
    /// <param name="resourcesType"></param>
    /// <returns></returns>
    public static List<PuzzlesInfoBean> LoadAllPuzzlesDataByType(JigsawResourcesEnum resourcesType)
    {
        List<PuzzlesInfoBean> listData = new List<PuzzlesInfoBean>();
        GameLanguageEnum language =CommonConfigure.GameLanguage;

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