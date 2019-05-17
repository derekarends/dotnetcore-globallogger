using Newtonsoft.Json;

namespace Api.Middleware
{
  public class ErrorResult
  {
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }
}