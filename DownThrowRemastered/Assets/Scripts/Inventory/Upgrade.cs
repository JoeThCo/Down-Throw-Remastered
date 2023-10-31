using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    public Upgrade()
    {
        BuffAdded();
    }

    ~Upgrade()
    {
        BuffRemoved();
    }

    protected virtual void BuffAdded()
    {

    }

    protected virtual void BuffRemoved()
    {

    }
}