namespace Zelda.Enums
{
    public enum GamePadButtonEnums
    {
        None,
        Down,
        Left,
        Right,
        Up,
        LeftBump,
        RightBump,
        Start,
        Select
    }
    public enum GamePadJoystickEnums
    {
        None,
        DownLJS,
        LeftLJS,
        RightLJS,
        UpLJS,
        PressLJS,
        DownRJS,
        LeftRJS,
        RightRJS,
        UpRJS,
        PressRJS
    }
    public enum GamePadTriggerEnums
    {
        None,
        Left,
        Right
    }
    public enum GamePadDPadEnums
    {
        None,
        Down,
        Left,
        Right,
        Up
    }
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum SwordType
    {
        None,
        WoodenSword,
        WhiteSword,
        MagicalSword
    }

    public enum ItemType
    {
        None,
        Arrow,
        Boomerang,
        Bomb,
        Fireball,
        Compass,
        WoodenSword,
        Key,
        Map,
        Triforce,
        Bow
    }

    public enum EnemyType
    {
        None,
        OldMan,
        Keese,
        Stalfos,
        Goriya,
        Gel,
        Zol,
        Trap,
        Wallmaster,
        Rope,
        Aquamentus,
        Dodongo
    }

    public enum CollisionSide
    {
        None,
        Left,
        Right,
        Top,
        Bottom
    }
}
