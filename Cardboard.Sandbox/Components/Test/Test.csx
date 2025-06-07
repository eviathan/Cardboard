@Include("./Test.csstyle");

public class TestProperties
{
    public string Text { get; set; }
}

[Component]
public partial class Test : Component<PropertiesWithChildren<TestProperties>>
{
    public override Element Render() => (
        <StackPanel>
            <Button Text="Click Me" OnClick={HandleClick} />
            <TextBlock Text={Properties.Text} />
        </StackPanel>
    );
}