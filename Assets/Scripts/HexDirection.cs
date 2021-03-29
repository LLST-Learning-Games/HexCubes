public enum HexDirection
{
    NE,E,SE,SW,W,NW
}

public static class HexDirectionExtensions
{
    public static HexDirection Opposite (this HexDirection direction)
    {
        return (int)direction < 3 ? (direction + 3) : (direction - 3);
    }

    public static HexDirection Previous (this HexDirection d)
    {
        return d == HexDirection.NE ? HexDirection.NW : (d - 1);
    }

    public static HexDirection Next(this HexDirection d)
    {
        return d == HexDirection.NW ? HexDirection.NE : (d + 1);
    }
}