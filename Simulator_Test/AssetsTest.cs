using Simulator_Lib;

namespace Simulator_Test;

[TestClass]
public class AssetsTest
{
    [TestMethod]
    public void LibExists()
    {
        Assert.IsTrue(IsExists.Exists());
    }
}