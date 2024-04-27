public class Info : Akequ.Plugins.PluginInfo
{
    public override string Name => "Test Plugin";

    public override string Id => "com.akequ.test";

    public override string Version => "0.0.1";

    public override ushort BundleVersion => 1;

    public override string[] MustSpawnClasses => null;

    public Info() { }
}