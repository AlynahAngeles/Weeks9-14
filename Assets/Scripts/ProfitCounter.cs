using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfitCounter : MonoBehaviour
{
    public TargetCollision linkedCloth;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Sold);
    }

    void Sold()
    {
        if(linkedCloth != null)
        {
            linkedCloth.Sell();
        }
    }
}
