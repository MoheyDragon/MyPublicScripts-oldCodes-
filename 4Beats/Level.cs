using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Level", menuName = "newLevel")]
public class Level : ScriptableObject
{
    public Wave[] Waves;
}
[CreateAssetMenu(fileName = "Wave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    public int EnemiesCount;
    public float spwanRate;
    public int[] DirectionOrder;
    public float waitAfter;
}
