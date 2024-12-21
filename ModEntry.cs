namespace ForcedSlopeBlocks
{
    using System;
    using System.Collections.Generic;
    using JumpKing.API;
    using JumpKing.Level;
    using JumpKing.Level.Sampler;
    using JumpKing.Mods;
    using JumpKing.Workshop;
    using Microsoft.Xna.Framework;

    [JumpKingMod("Zebra.ForcedSlopesBlock")]
    public static class ModEntry
    {
        /// <summary>
        /// Called by Jump King before the level loads
        /// </summary>
        [BeforeLevelLoad]
        public static void BeforeLevelLoad() => LevelManager.RegisterBlockFactory(new FactoryForcedSlope());
    }

    public class FactoryForcedSlope : IBlockFactory
    {
        public static readonly Color BLOCKCODE_TOP_LEFT = new Color(255, 1, 0);
        public static readonly Color BLOCKCODE_TOP_RIGHT = new Color(255, 0, 1);
        public static readonly Color BLOCKCODE_BOTTOM_LEFT = new Color(255, 2, 0);
        public static readonly Color BLOCKCODE_BOTTOM_RIGHT = new Color(255, 0, 2);

        private static readonly HashSet<Color> SupportedBlockCodes = new HashSet<Color>
        {
            BLOCKCODE_TOP_LEFT,
            BLOCKCODE_TOP_RIGHT,
            BLOCKCODE_BOTTOM_LEFT,
            BLOCKCODE_BOTTOM_RIGHT,
        };

        bool IBlockFactory.CanMakeBlock(Color blockCode, Level level) => SupportedBlockCodes.Contains(blockCode);

        bool IBlockFactory.IsSolidBlock(Color blockCode)
        {
            switch (blockCode)
            {
                case var _ when blockCode == BLOCKCODE_TOP_LEFT:
                case var _ when blockCode == BLOCKCODE_TOP_RIGHT:
                case var _ when blockCode == BLOCKCODE_BOTTOM_LEFT:
                case var _ when blockCode == BLOCKCODE_BOTTOM_RIGHT:
                    return true;
                default:
                    break;
            }
            return false;
        }

        IBlock IBlockFactory.GetBlock(Color blockCode, Rectangle blockRect, Level level, LevelTexture textureSrc, int currentScreen, int x, int y)
        {
            switch (blockCode)
            {
                case var _ when blockCode == BLOCKCODE_TOP_LEFT:
                    return new SlopeBlock(blockRect, SlopeType.TopLeft);
                case var _ when blockCode == BLOCKCODE_TOP_RIGHT:
                    return new SlopeBlock(blockRect, SlopeType.TopRight);
                case var _ when blockCode == BLOCKCODE_BOTTOM_LEFT:
                    return new SlopeBlock(blockRect, SlopeType.BottomLeft);
                case var _ when blockCode == BLOCKCODE_BOTTOM_RIGHT:
                    return new SlopeBlock(blockRect, SlopeType.BottomRight);
                default:
                    throw new InvalidOperationException($"{nameof(FactoryForcedSlope)} is unable to create a block of Color code ({blockCode.R}, {blockCode.G}, {blockCode.B})");
            }
        }
    }
}
