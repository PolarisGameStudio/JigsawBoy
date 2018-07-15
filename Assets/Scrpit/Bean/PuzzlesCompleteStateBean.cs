using System;


[Serializable]
public class PuzzlesCompleteStateBean
{
    public long puzzleId;//拼图ID
    public int puzzleType;//拼图类型
    public string puzzleName;//拼图名字
    public JigsawUnlockEnum unlockState;//解锁状态
    public TimeBean completeTime;//完成时间

}

