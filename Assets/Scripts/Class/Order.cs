using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactive.Engine;

public class Order
{
    public AlcoholColor color;
    public List<AlcoholAttribute> attribute = new List<AlcoholAttribute>();

    public Order()
    {
        //Shouldn't be call, an order without order is not an order....
    }

    public Order(AlcoholColor pColor, List<AlcoholAttribute> attributes)
    {
        color = pColor;
        for (int i = 0; i < attributes.Count; ++i)
        {
            attribute.Add(attributes[i]);
        }
    }
}
