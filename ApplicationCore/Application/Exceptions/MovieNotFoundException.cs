namespace ApplicationCore.Application.Exceptions;

public class MovieNotFoundException(string? message) : Exception(message);