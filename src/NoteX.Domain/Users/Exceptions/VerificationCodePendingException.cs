namespace NoteX.Domain.Users.Exceptions;

public class VerificationCodePendingException() : Exception("There is already a pending verification code.");