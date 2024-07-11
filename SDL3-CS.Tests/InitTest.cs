using SDL3;

namespace SDL3_CS.Tests;

[TestFixture]
public class InitTest
{
    [SetUp]
    public void Setup()
    {
    }
    
    [TearDown]
    public void TearDown() => SDL.Quit();

    [Test]
    [TestCase(SDL.InitFlags.Audio)]
    [TestCase(SDL.InitFlags.Video)]
    [TestCase(SDL.InitFlags.Camera)]
    [TestCase(SDL.InitFlags.Joystick)]
    [TestCase(SDL.InitFlags.Events)]
    [TestCase(SDL.InitFlags.Gamepad)]
    [TestCase(SDL.InitFlags.Haptic)]
    [TestCase(SDL.InitFlags.Sensor)]
    [TestCase(SDL.InitFlags.Timer)]
    public void Init(SDL.InitFlags flags)
    {
        var init = SDL.Init(flags);
        
        Assert.That(init, Is.EqualTo(0), $"Init({flags}) returned {init}");
    }
}