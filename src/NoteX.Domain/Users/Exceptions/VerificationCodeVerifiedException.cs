namespace NoteX.Domain.Users.Exceptions;

public class VerificationCodeVerifiedException() : Exception("This verification code has already been verified.");