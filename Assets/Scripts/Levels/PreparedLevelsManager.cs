#nullable enable
namespace Levels
{
    public class PreparedLevelsManager : ILevelsManager
    {
        private readonly ILevel _level;
        
        public PreparedLevelsManager(ILevel level)
        {
            _level = level;
        }
        public ILevel GetCurrentLevel()
        {
            return _level;
        }
    }
}