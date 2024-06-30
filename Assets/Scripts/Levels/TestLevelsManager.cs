#nullable enable
namespace Levels
{
    public class TestLevelsManager : ILevelsManager
    {
        private readonly TestLevel _level;
        
        public TestLevelsManager(TestLevel level)
        {
            _level = level;
        }

        public ILevel GetCurrentLevel()
        {
            return _level;
        }
    }
}