using System;

[Serializable]
public class PuzzlesInfoBean : PuzzlesInfoDetailsBean
{
    private long id;//id
    private string mark_file_name;//标记文件名
    private int horizontal_number;//生成拼图的横向个数
    private int vertical_number;//生成拼图的纵向个数
    private int level;//拼图等级
    private string data_file_path;//拼图文件路径
    private int data_type;//拼图类型
    private int unlock_point;//解锁点数
    private int valid;//拼图有效值

    public long Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Mark_file_name
    {
        get
        {
            return mark_file_name;
        }

        set
        {
            mark_file_name = value;
        }
    }

    public int Horizontal_number
    {
        get
        {
            return horizontal_number;
        }

        set
        {
            horizontal_number = value;
        }
    }

    public int Vertical_number
    {
        get
        {
            return vertical_number;
        }

        set
        {
            vertical_number = value;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }

    public string Data_file_path
    {
        get
        {
            return data_file_path;
        }

        set
        {
            data_file_path = value;
        }
    }

    public int Data_type
    {
        get
        {
            return data_type;
        }

        set
        {
            data_type = value;
        }
    }

    public int Valid
    {
        get
        {
            return valid;
        }

        set
        {
            valid = value;
        }
    }

    public int Unlock_point
    {
        get
        {
            return unlock_point;
        }

        set
        {
            unlock_point = value;
        }
    }
}