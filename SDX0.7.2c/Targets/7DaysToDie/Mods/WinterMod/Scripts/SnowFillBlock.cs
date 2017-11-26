using System;

// Token: 0x02000003 RID: 3
public class BlockSnowFill : BlockPlant
{
    // Token: 0x06000040 RID: 64
    public override void Init()
    {
        base.Init();
        if (this.Properties.Values.ContainsKey("LightLevelStay"))
        {
            int.TryParse(this.Properties.Values["LightLevelStay"], out this.lightLevelStay);
        }
    }

    // Token: 0x06000041 RID: 65
    public override bool CanPlantStay(WorldBase _world, int _clrIdx, Vector3i _blockPos, BlockValue _blockValue)
    {
        return _world.GetBlockLightValue(_clrIdx, _blockPos) >= this.lightLevelStay;
    }

    // Token: 0x06000044 RID: 68
    public override bool CheckPlantAlive(WorldBase _world, int _clrIdx, Vector3i _blockPos, BlockValue _blockValue)
    {
        this.Counter++;
        if (GameManager.Instance.IsEditMode())
        {
            return true;
        }
        if (!this.CanPlantStay(_world, _clrIdx, _blockPos, _blockValue))
        {
            _world.SetBlockRPC(_clrIdx, _blockPos, BlockValue.Air);
            return false;
        }

        return true;
    }

    // Token: 0x04000017 RID: 23
    private float nextCheck = 1f;

    // Token: 0x04000018 RID: 24
    private byte lightLevel;

    // Token: 0x04000019 RID: 25
    public static byte LightThreshold = 10;

    // Token: 0x0400001A RID: 26
    public static float CheckDelay = 1f;

    // Token: 0x0400001B RID: 27
    private bool blCanStay;

    // Token: 0x0400001C RID: 28
    private bool blFirstCheck = true;

    // Token: 0x0400001D RID: 29
    public new int lightLevelStay = 10;

    // Token: 0x0400001E RID: 30
    public int MaxChecks = 10;

    // Token: 0x0400001F RID: 31
    public int Counter;
}
