using System;

[Serializable]
public class JigsawResInfoIntroduceBean 
{

    public string introductionContent;//作品介绍

    //------------------名画-----------------------------
    public string workOfName; //作品名称
    public string workOfCreator;//作品作者
    public string storageArea;//作品现在所在地
    public string specifications;//大小规格
    public string timeOfCreation;//创作时间
    //-----------------电影-----------------------------
    public string moveOfName;//电影名称
    public string moveOfDirector;//电影导演
    public string stars;//主演
    public string length;//片场
    public string releaseDate;//上映日期
    //-----------------名人------------------------------
    public string celebrityName;//名字
    public string bornAndDeath;//出生去世
    public string country;//国家
    public string knownFor;//职业
    public string works;//主要作品

}