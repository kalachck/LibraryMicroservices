namespace BusinessLogic
{
    public class Result<T>
    {
        public T Value { get; set; }

        public string Message { get; set; }

        public string ExceptionMessage { get; set; }

        public bool HassException()
        {
            return ExceptionMessage != null;
        }

        public static implicit operator bool(Result<T> result)
        {
            return result.HassException();
        }
    }
}
