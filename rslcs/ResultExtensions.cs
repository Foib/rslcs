namespace rslcs
{
    public static class ResultExtensions
    {
        public static Result<TOut> Then<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> nextOperation)
        {
            if (!result.IsSuccess)
                return Result<TOut>.Err(result.Error);

            return nextOperation(result.Value);
        }
    }
}
