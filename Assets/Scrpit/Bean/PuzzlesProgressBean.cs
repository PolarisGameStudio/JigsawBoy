using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[Serializable]
public class PuzzlesProgressBean 
{
    public long puzzleId;
    public string markFileName;
    public TimeBean gameTime;
    public List<PuzzlesProgressItemBean> progress;

}