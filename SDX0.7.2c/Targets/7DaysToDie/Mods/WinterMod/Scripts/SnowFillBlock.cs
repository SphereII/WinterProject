using System;
using UnityEngine;

public class BlockSnowFill : BlockPlant
{
    // Stores when to do the next light check and what the current light level is
    private float nextCheck = 1;
    byte lightLevel;

    // Globa value for the light threshold in which zombies run in the dark
    public static byte LightThreshold = 10;

    // Frequency of check to determine the light level.
    public static float CheckDelay = 1f;

    bool blCanStay = false;

    bool blFirstCheck = true; 
    // default LightLevelStay value
    public new int lightLevelStay= 10;

    public static System.Random random = new System.Random();

    public override void Init()
    {
        base.Init();

        // we want to adjust the light level stay through XML, so we don't have to fuss around with code every time.
        if (this.Properties.Values.ContainsKey("LightLevelStay"))
            int.TryParse( this.Properties.Values["LightLevelStay"], out this.lightLevelStay);

    }

    // Token: 0x0600178A RID: 6026 RVA: 0x000AA3E0 File Offset: 0x000A85E0
    public override bool CanPlantStay(WorldBase _world, int _clrIdx, Vector3i _blockPos, BlockValue _blockValue)
    {
        // Do a simple check to see if the current block light value is correct or not
            bool blCanStay = ((_world.GetBlockLightValue(_clrIdx, _blockPos) >= this.lightLevelStay));
            if ( blCanStay )
            {
                Debug.Log("This plant can stay: Converting to Snow.");
                BlockValue newBlock = Block.GetBlockValue("snow");
                _world.SetBlockRPC(_clrIdx, _blockPos, newBlock);
            }
            else
            {
                Debug.Log("This plant cannot stay!");
            }
       
        return true;

    }

}