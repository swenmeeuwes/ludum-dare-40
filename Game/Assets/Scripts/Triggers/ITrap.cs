using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrap
{
    bool Enabled { get; set; }
    void Activate();
}
