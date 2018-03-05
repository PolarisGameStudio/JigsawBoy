using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class BGMInfoBean
{
    private long id;
    private string name;// 名字
    private string author;//作者
    private string length;//长度
    private int valid;//有效值
    private string filePath;//文件路径

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

    public string Author
    {
        get
        {
            return author;
        }

        set
        {
            author = value;
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

    public string FilePath
    {
        get
        {
            return filePath;
        }

        set
        {
            filePath = value;
        }
    }
}

