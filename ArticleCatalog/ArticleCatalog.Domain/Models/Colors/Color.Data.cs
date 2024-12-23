public class ColorData: IInitialData
{
    public Type EntityType => typeof(Color);

    public IEnumerable<object> GetData()
    {
        var red = new Color("Red");
        var blue = new Color("Blue");
        var black = new Color("Black");
        var pink = new Color("Pink");
        var purple = new Color("Purple");

        return [red, blue, black, pink, purple];
    }
}
