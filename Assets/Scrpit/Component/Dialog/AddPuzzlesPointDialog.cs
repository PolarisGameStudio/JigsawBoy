using UnityEngine;
using UnityEditor;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class AddPuzzlesPointDialog : BaseMonoBehaviour
{
    private Image mPuzzlesPointIcon;
    private Text mPuzzelsPointContent;

    private CanvasGroup mPuzzlesPointAdd;
    private Image mPuzzlesPointAddIcon;
    private Text mPuzzlesPointAddContent;

    private int mAddPoint;
    private long mCurrentPoint;
    private float mAddAnimTime;//增加动画持续时间

    void Awake()
    {
        mAddAnimTime = 8f;
        UserInfoBean userInfo = DataStorageManage.getUserInfoDSHandle().getData(0);
        if (userInfo != null)
            mCurrentPoint = userInfo.puzzlesPoint;
    }

    void Start()
    {
        mPuzzlesPointIcon = CptUtil.getCptFormParentByName<Transform, Image>(transform, "PuzzlesPointIcon");
        mPuzzelsPointContent = CptUtil.getCptFormParentByName<Transform, Text>(transform, "PuzzlesPointContent");

        mPuzzlesPointAdd = CptUtil.getCptFormParentByName<Transform, CanvasGroup>(transform, "PuzzlesPointAdd");
        mPuzzlesPointAddIcon = CptUtil.getCptFormParentByName<Transform, Image>(transform, "PuzzlesPointAddIcon");
        mPuzzlesPointAddContent = CptUtil.getCptFormParentByName<Transform, Text>(transform, "PuzzlesPointAddContent");

        initData();
        startAdd();

    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    private void initData()
    {
        if (mPuzzlesPointAddContent != null)
            mPuzzlesPointAddContent.text = mAddPoint + "PP";
        if (mPuzzelsPointContent != null)
            mPuzzelsPointContent.text = "x "+ mCurrentPoint + " PP";
    }

    /// <summary>
    /// 开始增加
    /// </summary>
    private void startAdd()
    {
        ((UserInfoDSHandle)DataStorageManage.getUserInfoDSHandle()).increaseUserPuzzlesPoint(mAddPoint);
        DOTween.To(() => mCurrentPoint,
            newPoint => {
                int newPointInt = (int)newPoint;
                mPuzzelsPointContent.text ="x "+ newPointInt + " PP";
            },
            mAddPoint + mCurrentPoint,
            mAddAnimTime);

        mPuzzlesPointAdd.DOFade(1, mAddAnimTime / 2f).OnComplete(()=> {
            mPuzzlesPointAdd.DOFade(0, mAddAnimTime / 2f);
        });
        mPuzzlesPointAdd.transform.DOLocalMoveY(20, mAddAnimTime).OnComplete(()=> {
            Destroy(gameObject);
        });
    }

    /// <summary>
    /// 设置增加的PP
    /// </summary>
    /// <param name="addPoint"></param>
    /// <returns></returns>
    public AddPuzzlesPointDialog setAddPoint(int addPoint)
    {
        mAddPoint = addPoint;
        return this;
    }
}