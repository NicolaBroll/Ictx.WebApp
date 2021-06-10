namespace Ictx.WebApp.BackGroundService.Common
{
    public interface IApplicationSettings
    {
        int ExecutionDelay { get; set; }
    }

    public class ApplicationSettings : IApplicationSettings
    {
        private int _executionDelay;

        private const int DEFAULT_EXECUTION_DELAY = 10;

        public int ExecutionDelay
        {
            get => _executionDelay <= 0 ? DEFAULT_EXECUTION_DELAY : this._executionDelay;
            set => this._executionDelay = value;
        }

        public string ElabExecutorPath { get; set; }

        public override string ToString()
        {
            return $"ExecutionDelay:{ExecutionDelay}";
        }
    }
}
