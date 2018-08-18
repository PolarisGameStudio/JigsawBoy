using System;

[Serializable]
public class PuzzlesInfoDetailsBean
{
    public string introduction_content;//作品介绍
    public string name;//名称

    //------------------名画-----------------------------
    public string work_creator;//作品作者
    public string storage_area;//作品现在所在地
    public string specifications;//大小规格
    public string time_creation;//创作时间
    //-----------------电影-----------------------------
    public string move_director;//电影导演
    public string stars;//主演
    public string length;//片场
    public string release_date;//上映日期
    //-----------------名人------------------------------
    public string born_death;//出生去世
    public string country;//国家
    public string known_for;//职业
    public string works;//主要作品
    

    public string Introduction_content
    {
        get
        {
            return introduction_content;
        }

        set
        {
            introduction_content = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string Work_creator
    {
        get
        {
            return work_creator;
        }

        set
        {
            work_creator = value;
        }
    }

    public string Storage_area
    {
        get
        {
            return storage_area;
        }

        set
        {
            storage_area = value;
        }
    }

    public string Specifications
    {
        get
        {
            return specifications;
        }

        set
        {
            specifications = value;
        }
    }

    public string Time_creation
    {
        get
        {
            return time_creation;
        }

        set
        {
            time_creation = value;
        }
    }

    public string Move_director
    {
        get
        {
            return move_director;
        }

        set
        {
            move_director = value;
        }
    }

    public string Stars
    {
        get
        {
            return stars;
        }

        set
        {
            stars = value;
        }
    }

    public string Length
    {
        get
        {
            return length;
        }

        set
        {
            length = value;
        }
    }

    public string Release_date
    {
        get
        {
            return release_date;
        }

        set
        {
            release_date = value;
        }
    }

    public string Born_death
    {
        get
        {
            return born_death;
        }

        set
        {
            born_death = value;
        }
    }

    public string Country
    {
        get
        {
            return country;
        }

        set
        {
            country = value;
        }
    }

    public string Known_for
    {
        get
        {
            return known_for;
        }

        set
        {
            known_for = value;
        }
    }

    public string Works
    {
        get
        {
            return works;
        }

        set
        {
            works = value;
        }
    }
}