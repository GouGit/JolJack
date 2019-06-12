using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct SceneOption
{
    [Tooltip("플레이어와 전투를 할 몬스터 프리팹들을 넣습니다.")]
    public List<Monster> monsterList;
    public string testString;
}
