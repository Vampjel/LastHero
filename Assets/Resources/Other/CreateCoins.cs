using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCoins : MonoBehaviour
{
     List<GameObject> coins = new List<GameObject>();
    public void CreateCoinss(int total)
    {
        for (int i = 0; i < total; i++)
        {
            coins.Add(Instantiate(Resources.Load("Other/gold/gold") as GameObject, transform.position, Quaternion.identity));
            coins[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.RandomRange(-250, 250), Random.RandomRange(400f,600f)));
        }
    }
}
