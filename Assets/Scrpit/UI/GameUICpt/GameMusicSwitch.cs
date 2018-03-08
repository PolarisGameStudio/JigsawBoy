using UnityEngine;
using UnityEngine.UI;
public class GameMusicSwitch : BaseMonoBehaviour
{
    private Toggle[] toggleList;
    private IRadioButtonCallBack<Toggle,long> radioButtonCallBack;
    private void Start()
    {
        toggleList = transform.GetComponentsInChildren<Toggle>();

        if (toggleList != null && toggleList.Length > 0)
        {
            int toggleSize = toggleList.Length;
            for (int i = 0; i < toggleSize; i++)
            {
                Toggle itemToggle = toggleList[i];
                itemToggle.onValueChanged.AddListener((bool value) => OnToggleClick(itemToggle, value));
            }
        }
    }

    /// <summary>
    /// 设置监听
    /// </summary>
    /// <param name="radioButtonCallBack"></param>
    public void addRadioButtonCallBack(IRadioButtonCallBack<Toggle, long> radioButtonCallBack)
    {
        this.radioButtonCallBack = radioButtonCallBack;
    }

    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="toggle"></param>
    /// <param name="value"></param>
    public void OnToggleClick(Toggle toggle, bool value)
    {
        radioButtonCallBack.radioBTOnClick(toggle, value, 0);
    }
}

