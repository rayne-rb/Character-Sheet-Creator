namespace CharacterSheetCreator.Shared;

public class AppError
{
    public AppError()
    {
    }
    
    public AppError(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
    
    public string ErrorMessage { get; set; }
}