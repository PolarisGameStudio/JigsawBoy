using UnityEngine;
using UnityEditor;

public interface IUserAchievementHandle 
{
    /// <summary>
    /// 用户完成拼图数量改变
    /// </summary>
    /// <param name="changeNumber"></param>
     void userCompleteNumberChange(int changeNumber);

    /// <summary>
    /// 清空所有成就
    /// </summary>
     void resetAllAchievement();
}