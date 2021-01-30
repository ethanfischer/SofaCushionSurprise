using System.Linq;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    public int[] Distributions =
    {
        250,
        50,
        25,
        5
    };

    public string[] Items =
    {
        nameof(ItemType.Nothing),
        nameof(ItemType.Coin),
        nameof(ItemType.Battery),
        nameof(ItemType.Diamond),
        nameof(ItemType.ScriptedItem)
    };

    public string ChooseItem()
    {
        var total = Distributions.Sum();
        var randomNumber = Random.Range(1, total+1);
        for (var i = 0; i < Distributions.Length; i++)
        {
            if (randomNumber <= Distributions[i])
            {
                return Items[i];
            }
            else
            {
                randomNumber -= Distributions[i];
            }
        }

        return null; //we shouldn't ever get here
    }
}
