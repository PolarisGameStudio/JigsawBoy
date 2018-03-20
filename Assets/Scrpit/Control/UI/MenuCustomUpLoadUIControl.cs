using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class MenuCustomUpLoadUIControl : BaseUIControl
{

    private new void Awake()
    {
        base.Awake();
    }

    public override void openUI()
    {
        mUICanvas.enabled = true;
    }

    public override void closeUI()
    {
        mUICanvas.enabled = false;
    }

    public override void loadUIData()
    {
        throw new NotImplementedException();
    }


}

