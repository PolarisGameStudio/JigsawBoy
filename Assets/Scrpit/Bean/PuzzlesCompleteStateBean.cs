using System;


[Serializable]
public class PuzzlesCompleteStateBean
{
    public long puzzleId;//拼图ID
    public JigsawUnlockEnum unlockState;//解锁状态
    public TimeBean completeTime;//完成时间
}

