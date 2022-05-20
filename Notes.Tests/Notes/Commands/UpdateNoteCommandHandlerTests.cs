using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Tests.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.Notes.Commands
{
    public class UpdateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateNoteCommandHandler_Success()
        {
            // Arrange
            var hander = new UpdateNoteCommandHandler(Context);
            var updateTitle = "Title";

            // Act
            await hander.Handle(
                new UpdateNoteCommand
                {
                    Id = NotesContextFactory.NoteIdForUpdate,
                    Title = updateTitle,
                    UserId = NotesContextFactory.UserBId
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(x =>
            x.Id == NotesContextFactory.NoteIdForUpdate && x.UserId == NotesContextFactory.UserBId
            && x.Title == updateTitle));
        }

        [Fact]
        public async Task UpdateNoteCommandHandler_FailOnWrongNoteId()
        {
            // Arrange
            var handler = new UpdateNoteCommandHandler(Context);

            // Act


            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new UpdateNoteCommand
                {
                    Id = Guid.NewGuid(),
                    UserId = NotesContextFactory.UserAId
                },
                CancellationToken.None)
            );
        }

        [Fact]
        public async Task UpdateNoteCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new UpdateNoteCommandHandler(Context);

            // Act


            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new UpdateNoteCommand
                {
                    Id = NotesContextFactory.NoteIdForUpdate,
                    UserId = NotesContextFactory.UserAId
                },
                CancellationToken.None)
            );
        }
    }
}
