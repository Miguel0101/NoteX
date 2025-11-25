using NoteX.Application.Common.Interfaces;
using NoteX.Application.Common.Results;
using NoteX.Application.Common.Results.Enums;
using NoteX.Application.Common.Results.Mappers;
using NoteX.Application.Notes.DTOs.Requests;
using NoteX.Application.Notes.DTOs.Responses;
using NoteX.Domain.Notes.Entities;
using NoteX.Domain.Notes.Interfaces;
using NoteX.Domain.Notes.ValueObjects;

namespace NoteX.Application.Notes.Services;

public class NoteService : INoteService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INoteRepository _noteRepository;
    private readonly IUserContext _userContext;

    public NoteService(IUnitOfWork unitOfWork, INoteRepository noteRepository, IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _noteRepository = noteRepository;
        _userContext = userContext;
    }

    public async Task<Result> GetAllAsync()
    {
        try
        {
            Guid userId = _userContext.GetUserId();

            IReadOnlyList<Note> notes = await _noteRepository.GetAllAsync(userId);
            List<NoteResponse> noteResponses = [.. notes.Select(n => new NoteResponse(n.Id, n.Title.Value, n.Content.Value))];

            return Result<List<NoteResponse>>.Success(ResultCode.Success, noteResponses);
        }
        catch (Exception e)
        {
            return e.ToResult();
        }
    }

    public async Task<Result> GetByIdAsync(Guid id)
    {
        try
        {
            Guid userId = _userContext.GetUserId();

            Note? note = await _noteRepository.GetByIdAsync(userId, id);

            if (note == null)
            {
                return Result.Failure(ResultCode.NoteNotFoundError);
            }

            NoteResponse noteResponse = new(note.Id, note.Title.Value, note.Content.Value);

            return Result<NoteResponse>.Success(ResultCode.Success, noteResponse);
        }
        catch (Exception e)
        {
            return e.ToResult();
        }
    }

    public async Task<Result> AddAsync(CreateNoteRequest createNoteRequest)
    {
        try
        {
            Guid userId = _userContext.GetUserId();

            Title title = Title.Create(createNoteRequest.Title);
            Content content = Content.Create(createNoteRequest.Content);

            bool noteExists = await _noteRepository.GetByTitleAsync(userId, title) != null;

            if (noteExists)
            {
                return Result.Failure(ResultCode.NoteAlreadyExistsError);
            }

            Note note = Note.Create(userId, title, content);

            await _noteRepository.AddAsync(note);
            await _unitOfWork.SaveChangesAsync();

            NoteResponse noteResponse = new(note.Id, note.Title.Value, note.Content.Value);

            return Result<NoteResponse>.Success(ResultCode.Success, noteResponse);
        }
        catch (Exception e)
        {
            return e.ToResult();
        }
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateNoteRequest updateNoteRequest)
    {
        try
        {
            Guid userId = _userContext.GetUserId();

            Note? note = await _noteRepository.GetByIdAsync(userId, id);

            if (note == null)
            {
                return Result.Failure(ResultCode.NoteNotFoundError);
            }

            Title title = Title.Create(updateNoteRequest.Title);
            Content content = Content.Create(updateNoteRequest.Content);

            note.UpdateTitle(title);
            note.UpdateContent(content);

            await _unitOfWork.SaveChangesAsync();

            NoteResponse noteResponse = new(note.Id, note.Title.Value, note.Content.Value);

            return Result<NoteResponse>.Success(ResultCode.Success, noteResponse);
        }
        catch (Exception e)
        {
            return e.ToResult();
        }
    }

    public async Task<Result> UpdateContentAsync(Guid id, UpdateContentRequest updateContentRequest)
    {
        try
        {
            Guid userId = _userContext.GetUserId();

            Note? note = await _noteRepository.GetByIdAsync(userId, id);

            if (note == null)
            {
                return Result.Failure(ResultCode.NoteNotFoundError);
            }

            Content content = Content.Create(updateContentRequest.Content);

            note.UpdateContent(content);

            await _unitOfWork.SaveChangesAsync();

            UpdateContentResponse contentResponse = new(note.Content.Value);

            return Result<UpdateContentResponse>.Success(ResultCode.Success, contentResponse);
        }
        catch (Exception e)
        {
            return e.ToResult();
        }
    }

    public async Task<Result> UpdateTitleAsync(Guid id, UpdateTitleRequest updateTitleRequest)
    {
        try
        {
            Guid userId = _userContext.GetUserId();

            Note? note = await _noteRepository.GetByIdAsync(userId, id);

            if (note == null)
            {
                return Result.Failure(ResultCode.NoteNotFoundError);
            }

            Title title = Title.Create(updateTitleRequest.Title);

            note.UpdateTitle(title);

            await _unitOfWork.SaveChangesAsync();

            UpdateTitleResponse titleResponse = new(note.Title.Value);

            return Result<UpdateTitleResponse>.Success(ResultCode.Success, titleResponse);
        }
        catch (Exception e)
        {
            return e.ToResult();
        }
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        try
        {
            Guid userId = _userContext.GetUserId();

            Note? note = await _noteRepository.GetByIdAsync(userId, id);

            if (note == null)
            {
                return Result.Failure(ResultCode.NoteNotFoundError);
            }

            _noteRepository.Delete(note);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success(ResultCode.Success);
        }
        catch (Exception e)
        {
            return e.ToResult();
        }
    }
}