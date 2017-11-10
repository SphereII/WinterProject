using System;
using UnityEngine;

public class BlockSnowFill : BlockPlantGrowing
{
  
    // overriding the initialization function.
    public override void Init()
    {
        base.Init();
        

    }

  

    // Token: 0x0600178A RID: 6026 RVA: 0x000AA3E0 File Offset: 0x000A85E0
    public override bool CanPlantStay(WorldBase _world, int _clrIdx, Vector3i _blockPos, BlockValue _blockValue)
    {
        return  _world.GetBlockLightValue(_clrIdx, _blockPos) >= this.lightLevelStay ;
    }


}