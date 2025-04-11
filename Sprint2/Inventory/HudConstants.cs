using Microsoft.Xna.Framework;

namespace MainGame.Display
{
    public static class HudConstants
    {
     
        public const float BottomBarScale = 1.8f;
        public static readonly Rectangle BottomBarSourceRect = new Rectangle(344, 17, 154, 44);
        public const int BottomBarDestX = 135;
        public const int BottomBarDestY = 360;

       
        public const float HeartIconScale = 2.5f;
        public static readonly Rectangle FullHeartSourceRect = new Rectangle(645, 117, 8, 8);
        public static readonly Rectangle EmptyHeartSourceRect = new Rectangle(627, 117, 8, 8);
        public const int HeartSpacing = 30;   
        public const int HeartStartX = 320;     
        public const int HeartY = 405;         


        public const float MiniMapScale = 2.0f;
        public static readonly Rectangle BottomMiniMapSourceRect_Map = new Rectangle(584, 0, 62, 38);
        public static readonly Rectangle BottomMiniMapSourceRect_Empty = new Rectangle(754, 0, 70, 50);
        public const int MiniMapDestX = 40;
        public const int MiniMapDestY = 370;

        // Selected item icons in HUD (the small icon at the bottom)
       
        public const float SelectedArrowScale = 2.3f;
        public static readonly Rectangle SelectedArrowSourceRect = new Rectangle(617, 138, 5, 13);
        public const int SelectedArrowDestX = 234;
        public const int SelectedArrowDestY = 393;
        
        public const float SelectedBombScale = 2.2f;
        public static readonly Rectangle SelectedBombSourceRect = new Rectangle(604, 138, 8, 14);
        public const int SelectedBombDestX = 231;
        public const int SelectedBombDestY = 393;
     
        public const float SelectedBoomerangScale = 2.4f;
        public static readonly Rectangle SelectedBoomerangSourceRect = new Rectangle(585, 138, 7, 14);
        public const int SelectedBoomerangDestX = 231;
        public const int SelectedBoomerangDestY = 390;
    }
}
