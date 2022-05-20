using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.DeleteCommand;
using Notes.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.Notes.Commands
{
    public class DeleteNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteNoteCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteNoteCommandHandler(Context);

            // Act
            await handler.Handle(
                new DeleteNoteCommand 
                    {
                        Id = NotesContextFactory.NoteIdForDelete,
                        UserId = NotesContextFactory.UserAId
                    }, 
                CancellationToken.None);

            // Assert
            Assert.Null(Context.Notes.SingleOrDefaultAsync(x =>
            x.Id == NotesContextFactory.NoteIdForDelete));
        }

        [Fact]
        public async Task DeleteNoteCommandHandler_FailOnWrongNoteId()
        {
            // Arrange
            var handler = new DeleteNoteCommandHandler(Context);

            // Act


            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new DeleteNoteCommand
                {
                    Id = Guid.NewGuid(),
                    UserId = NotesContextFactory.UserAId
                },
                CancellationToken.None)
            );
        }

        [Fact]
        public async Task DeleteNoteCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new DeleteNoteCommandHandler(Context);

            // Act


            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new DeleteNoteCommand
                {
                    Id = NotesContextFactory.NoteIdForDelete,
                    UserId = Guid.NewGuid()
                },
                CancellationToken.None)
            );
        }
    }
}
