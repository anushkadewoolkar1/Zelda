namespace Zelda.Enums
{
    public enum UserInputs
    {
        None,
        NewGame,
        AttackMelee,
        UseItem,
        ToggleOptions,
        ToggleInventory,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        ToggleMute,
        LowerVolume,
        RaiseVolume,
        ToggleFullscreen,
        ResetLevel
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

    public enum GameState
    {
        Playing,
        StartMenu,
        MainMenu,
        NewGame,
        Credits,
        Paused,
        Death,
        GameOver,
        Win
    }
}
