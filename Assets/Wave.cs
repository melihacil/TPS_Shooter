using System.Threading;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyWave")]
public class Wave : ScriptableObject
{
    public GameObject[] enemyTypes;
    public int enemyCount;
    
    public void Initialize(int count = 10)
    {
        enemyCount = count;
    }

   


}