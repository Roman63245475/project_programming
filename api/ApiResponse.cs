namespace api;

public class ApiResponse {
    public string Message { get; init; }

    public ApiResponse(string message) {
        Message = message;
    }
}