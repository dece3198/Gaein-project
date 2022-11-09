using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGuestState
{
    public void Enter(Guest guest);
    public void Update(Guest guest);
    public void Exit(Guest guest);
}