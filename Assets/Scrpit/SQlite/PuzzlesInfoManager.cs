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
        GameLanguageEnum language = CommonData.GameLanguage;

        string detailsTableName = "";
        if (resourcesType.Equals(JigsawResourcesEnum.Painting))
            detailsTableName = CommonData.PuzzleInfoDB_Details_Painting_Table;
        else if (resourcesType.Equals(JigsawResourcesEnum.Movie))
            detailsTableName = CommonData.PuzzleInfoDB_Details_Movie_Table;
        else if (resourcesType.Equals(JigsawResourcesEnum.Celebrity))
            detailsTableName = CommonData.PuzzleInfoDB_Details_Celebrity_Table;
        else
            return null;



        if (language.Equals(GameLanguageEnum.Chinese))
            detailsTableName += "_cn";
        else if (language.Equals(GameLanguageEnum.English))
            detailsTableName += "_en";

        listData = SQliteHandle.LoadTableData<PuzzlesInfoBean>
            (
            CommonData.PuzzleInfoDB_Name,
            CommonData.PuzzleInfoDB_PuzzlesBase_Table,
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