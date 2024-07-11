using SDL3;

namespace SDL3_CS.Tests;

[TestFixture]
public class HintsTest
{
    [SetUp]
    public void Setup()
    {
        SDL.Init(SDL.InitFlags.Video);
    }
    
    [TearDown]
    public void TearDown() => SDL.Quit();

    [Test]
    [TestCase($"{SDL.HintLogging}", "2")]
    [TestCase($"{SDL.HintRenderDriver}", "3")]
    public void SetHint(string name, string value)
    {
        var result = SDL.SetHint(name, value);
        var hint = SDL.GetHint(name);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(true), $"SetHint({name}, {value}) returned {result}");
            Assert.That(hint, Is.EqualTo(value), $"SetHint({name}, {value}) did not set the hint (result: {hint}).");
        });
    }

    [Test]
    [TestCase($"{SDL.HintLogging}")]
    public void ResetHint(string name)
    {
        SDL.SetHint(name, "2");
        
        var result = SDL.ResetHint(name);
        
        Assert.That(result, Is.EqualTo(true), $"ResetHint({name}) returned {result}");
    }
}