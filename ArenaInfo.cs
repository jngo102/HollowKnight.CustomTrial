namespace CustomTrial
{
    internal class ArenaInfo
    {
        public const float DefaultLeftX = 86.6f;
        public const float DefaultRightX = 118.3f;
        public const float DefaultTopY = 28.6f;
        public const float DefaultBottomY = 6.4f;
        public const float DefaultCenterX = DefaultLeftX + (DefaultLeftX + DefaultRightX) / 2.0f;
        public const float DefaultCenterY = DefaultBottomY + (DefaultTopY + DefaultBottomY) / 2.0f;


        public static float LeftX = DefaultLeftX;
        public static float RightX = DefaultRightX;
        public static float TopY = DefaultTopY;
        public static float BottomY = DefaultBottomY;
        public static float CenterX = DefaultCenterX;
        public static float CenterY = DefaultCenterY;
    }
}