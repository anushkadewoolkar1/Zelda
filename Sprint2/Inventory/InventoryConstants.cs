using Microsoft.Xna.Framework;
using System.ComponentModel;

namespace MainGame.Display
{
    public static class InventoryConstants
    {
        // Sprite definition constants 
        public const float TopLeftScale = 1.5f;
        public static readonly Rectangle TopLeftSourceRect = new Rectangle(8, 20, 108, 78);

        public const float TopRightScale = 1.64f;
        public static readonly Rectangle TopRightSourceRect = new Rectangle(115, 50, 130, 48);

        public const float DungeonScale = 1.8f;
        public static readonly Rectangle DungeonSourceRect = new Rectangle(260, 114, 83, 84);

        public const float CompassScale = 2.5f;
        public static readonly Rectangle CompassSourceRect = new Rectangle(612, 157, 14, 14);

        public const float MapIconScale = 2.4f;
        public static readonly Rectangle MapIconSourceRect = new Rectangle(602, 157, 6, 14);

        public const float MiniMapScale = 1.8f;
        public static readonly Rectangle MiniMapSourceRect = new Rectangle(345, 114, 150, 84);

        public const float EmptyMapScale = 1.8f;
        public static readonly Rectangle EmptyMapSourceRect = new Rectangle(348, 214, 125, 80);

        public const float ArrowInvScale = 2.6f;
        public static readonly Rectangle ArrowInvSourceRect = new Rectangle(617, 138, 5, 13);

        public const float BombInvScale = 2.5f;
        public static readonly Rectangle BombInvSourceRect = new Rectangle(604, 138, 8, 14);

        public const float BoomerangInvScale = 2.5f;
        public static readonly Rectangle BoomerangInvSourceRect = new Rectangle(585, 138, 7, 14);

        public const float PinkIndicatorScale = 1.0f;
        public static readonly Rectangle PinkIndicatorSourceRect = new Rectangle(350, 70, 5, 5);

        public const float HudIndicatorScale = 0.5f;
        public static readonly Rectangle HudIndicatorSourceRect = new Rectangle(400, 245, 10, 10);

        public const float SelectedArrowScale = 2.3f;
        public static readonly Rectangle SelectedArrowSourceRect = new Rectangle(617, 138, 5, 13);

        public const float SelectedBombScale = 2.2f;
        public static readonly Rectangle SelectedBombSourceRect = new Rectangle(604, 138, 8, 14);

        public const float SelectedBoomerangScale = 2.4f;
        public static readonly Rectangle SelectedBoomerangSourceRect = new Rectangle(585, 140, 7, 12);

        public const int TopLeftDestX = 30;
        public const int TopLeftDestY = 5;

        public const int TopRightDestX = 240;
        public const int TopRightDestY = 53;

        public const int DungeonDestX = 25;
        public const int DungeonDestY = 165;

        public const int CompassDestX = 95;
        public const int CompassDestY = 265;

        public const int MapIconDestX = 105;
        public const int MapIconDestY = 195;

        public const int MiniMapDestX = 200;
        public const int MiniMapDestY = 180;

        public const int EmptyMapDestX = 200;
        public const int EmptyMapDestY = 180;

        public const int ArrowInvDestX = 273;
        public const int ArrowInvDestY = 75;

        public const int BombInvDestX = 310;
        public const int BombInvDestY = 75;

        public const int BoomerangInvDestX = 360;
        public const int BoomerangInvDestY = 79;

        
        public const int SelectedItemDestX = 121;
        public const int SelectedItemDestY = 59;

        
        public const int BackgroundExtraWidth = 33;
        public const int BackgroundExtraHeight = -8;

        // for green link indicator on minimap
        public const int BaseLX = 292;
        public const int LXOffset = 15;
        public const int BaseLY = 197;
        public const int LYOffset = 15;

        // hud minimap indicator
        public const int hBaseLX = 60;
        public const int hLXOffset = 18;
        public const int hBaseLY = 400;
        public const int hLYOffset = 8;


    };
}

