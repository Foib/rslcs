using System.Diagnostics.CodeAnalysis;

namespace rslcs
{
    public class Result<T>
    {
        [MemberNotNullWhen(false, nameof(Error))]
        [MemberNotNullWhen(true, nameof(Value))]
        public bool IsSuccess { get; }
        public T? Value { get; }
        public Exception? Error { get; }

        private Result(T value)
        {
            Value = value;
            Error = null;
            IsSuccess = true;
        }

        private Result(Exception error)
        {
            Value = default;
            Error = error;
            IsSuccess = false;
        }

        public static Result<T> Ok(T value) => new(value);

        public static Result<T> Err(Exception error) => new(error);

        public void Deconstruct(out bool isSuccess, out T? value, out Exception? error)
        {
            isSuccess = IsSuccess;
            value = Value;
            error = Error;
        }
    }
}
