

namespace SecondAssignment.Application.Core
{
   public class Result<TData> where TData : class
    {
        public Result()
        {
            IsSucces = true;
        }
        public TData? Data { get; set; }
        public bool IsSucces { get; set; }
        public string? Message { get; set; }
    }
}
