using SFML.System;

namespace PinPong
{
    static class Config
    {
        public static Vector2f projectilePosition = new(760, 450);

        public static Vector2f border1Position = new(0, 0);
        public static Vector2f border2Position = new(0, 880);

        public static Vector2f player1Position = new(35, 450);
        public static Vector2f player2Position = new(1550, 450);

        public static Vector2f out1Position = new(0, 0);
        public static Vector2f out2Position = new(1580, 0);
    }
}
