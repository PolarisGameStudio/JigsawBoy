﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class JigsawUnlockStateBean
{

    public long puzzleId;//拼图ID
    public JigsawResourcesEnum puzzleType;//拼图归类（名画，任务，或自定义）
    public JigsawUnlockEnum unlockState;//解锁状态
}

