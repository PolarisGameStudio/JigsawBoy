using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{

    public List<Sprite> mListStatus;
    public int mStatus;
    private List<GameObject> mListSwitchItem;
    private CallBack mCallBack;

    private void Awake()
    {
        initData();
    }

    /// <summary>
    /// 设置 状态
    /// </summary>
    /// <param name="status"></param>
    public void setStatus(int status)
    {
        mStatus = status;
        if(mListSwitchItem!=null)
        for (int i = 0; i < mListSwitchItem.Count; i++)
        {
            if (i == status)
                mListSwitchItem[i].SetActive(true);
            else
                mListSwitchItem[i].SetActive(false);
        }
        if (mCallBack != null)
        {
            mCallBack.onSwitchChange(gameObject, status);
        }
    }

    /// <summary>
    /// 设置回调
    /// </summary>
    /// <param name="mCallBack"></param>
    public void setCallBack(CallBack mCallBack)
    {
        this.mCallBack = mCallBack;
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    private void initData()
    {
        if (mListStatus == null)
            return;
        Transform switchItem = CptUtil.getCptFormParentByName<Transform, Transform>(transform, "SwitchImage");
        Button switchBT = transform.GetComponent<Button>();
        if (switchItem == null)
            return;
        mListSwitchItem = new List<GameObject>();
        for (int i = 0; i < mListStatus.Count; i++)
        {
            GameObject itemObj = createSwitchImage(switchItem, i);
            mListSwitchItem.Add(itemObj);
        }
        Destroy(switchItem.gameObject);
        if (switchBT != null)
            switchBT.onClick.AddListener(switchClick);
    }

    /// <summary>
    /// 创建切换按钮
    /// </summary>
    /// <param name="demon"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    private GameObject createSwitchImage(Transform demon, int position)
    {
        Transform itemView = Instantiate(demon, demon);
        itemView.parent = transform;

        Image itemImage = itemView.GetComponent<Image>();
        itemView.name = "switch_" + position;
        itemImage.sprite = mListStatus[position];
        itemImage.type = Image.Type.Simple;

        if (mStatus == position)
            itemView.gameObject.SetActive(true);
        else
            itemView.gameObject.SetActive(false);

        return itemView.gameObject;
    }

    /// <summary>
    /// 点击事件
    /// </summary>
    private void switchClick()
    {
        mStatus++;
        if (mStatus >= mListStatus.Count) {
            mStatus = 0;
        }
        setStatus(mStatus);
    }


    /// <summary>
    /// 回调
    /// </summary>
    public interface CallBack
    {
        void onSwitchChange(GameObject view, int status);
    }


}
