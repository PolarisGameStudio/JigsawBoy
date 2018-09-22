using System.Collections.Generic;



public class UITextManager
{

    /// <summary>
    /// 获取所有的UI文本
    /// </summary>
    /// <returns></returns>
    public static Dictionary<long, UITextBean> LoadAllUIText()
    {
        List<UITextBean> listData = new List<UITextBean>();
        GameLanguageEnum language = CommonConfigure.GameLanguage;

        string detailsTableName = CommonDB.PuzzleInfoDB_UITextContent_Table;

        if (language.Equals(GameLanguageEnum.Chinese))
            detailsTableName += "_cn";
        else if (language.Equals(GameLanguageEnum.English))
            detailsTableName += "_en";
        else if (language.Equals(GameLanguageEnum.German))
            detailsTableName += "_gn";
        else if (language.Equals(GameLanguageEnum.Japanese))
            detailsTableName += "_jn";
        else if (language.Equals(GameLanguageEnum.Russian))
            detailsTableName += "_rn";
        else if (language.Equals(GameLanguageEnum.Polish))
            detailsTableName += "_pn";
        else if (language.Equals(GameLanguageEnum.French))
            detailsTableName += "_fn";

        listData = SQliteHandle.LoadTableData<UITextBean>
            (
            CommonDB.PuzzleInfoDB_Name,
            CommonDB.PuzzleInfoDB_PuzzlesBase_Table,
            new string[] { detailsTableName },
            "id",
            new string[] { "text_id" }
            );
        Dictionary<long, UITextBean> mapData = new Dictionary<long, UITextBean>();
        if (listData != null && listData.Count > 0)
        {
            int listDataSize = listData.Count;
            for (int i = 0; i < listDataSize; i++)
            {
                UITextBean itemData = listData[i];
                if (itemData.Text_id != 0)
                    mapData.Add(itemData.Text_id, itemData);
            }
        }
        return mapData;
    }
}

