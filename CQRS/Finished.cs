namespace CQRS
{
    public struct Finished
    {
        private readonly bool _success;
        private readonly bool _error;

        public Finished(bool success, bool error)
        {
            _success = success;
            _error = error;
        }

        public bool InSuccess()
        {
            return _success && !_error;
        }

        public bool InFailure()
        {
            return !_success || _error;
        }
    }
}