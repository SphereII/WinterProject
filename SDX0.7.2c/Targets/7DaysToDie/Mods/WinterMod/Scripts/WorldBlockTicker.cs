using System;
using System.Linq;

public class WorldBlockTickerFixed : WorldBlockTicker
{

    // Token: 0x06003C24 RID: 15396 RVA: 0x001D35C8 File Offset: 0x001D17C8
    public WorldBlockTickerFixed(World _world)
    {
        this.HZ = _world;
        this.GZ = new Random().Next();
    }

    public void ProtectException( Chunk chunk, Random rnd)
    {
        if (chunk == null)
        {
            return;
        }
        if (chunk.NeedsLightCalculation)
        {
            return;
        }
        if (GameTimer.Instance.ticks - chunk.LastTimeRandomTicked < 1200UL)
        {
            return;
        }
        ulong ticksIfLoaded = GameTimer.Instance.ticks - chunk.LastTimeRandomTicked;
        chunk.LastTimeRandomTicked = GameTimer.Instance.ticks;
        DictionaryKeyList<Vector3i, int> tickedBlocks = chunk.GetTickedBlocks();
        DictionaryKeyList<Vector3i, int> obj = tickedBlocks;
        lock (obj)
        {
            for (int i = tickedBlocks.list.Count - 1; i >= 0; i--)
            {
                try
                {
                    Vector3i vector3i = tickedBlocks.list[i];
                    BlockValue block = chunk.GetBlock(World.toBlockXZ(vector3i.x), vector3i.y, World.toBlockXZ(vector3i.z));
                    if (this.WZ.Count == 0 || !this.WZ.ContainsKey(WorldBlockTickerEntry.ToHashCode(chunk.ClrIdx, vector3i, block.type)))
                    {
                        Block.list[block.type].UpdateTick(this.HZ, chunk.ClrIdx, vector3i, block, true, ticksIfLoaded, rnd);
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                }
            }
        }
    }
}

