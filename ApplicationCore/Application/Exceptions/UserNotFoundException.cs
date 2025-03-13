namespace ApplicationCore.Application.Exceptions;

public class UserNotFoundException(string msg) : Exception(msg);