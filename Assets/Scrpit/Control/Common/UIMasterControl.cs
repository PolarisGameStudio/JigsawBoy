

using System.Windows.Forms;

public class UIMasterControl : BaseMonoBehaviour
{
    private void Start()
    {
        OpenFileDialog ofd = new OpenFileDialog();   //new一个方法
        ofd.InitialDirectory = "file://" + UnityEngine.Application.dataPath;  //定义打开的默认文件夹位置//定义打开的默认文件夹位置
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            LogUtil.log(ofd.FileName);
        }
    }
}

