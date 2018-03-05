using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class UITextBean
{
    private long id;
    private long text_id;
    private string content;
    private string name;
    private int valid;

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

    public long Text_id
    {
        get
        {
            return text_id;
        }

        set
        {
            text_id = value;
        }
    }

    public string Content
    {
        get
        {
            return content;
        }

        set
        {
            content = value;
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
}

