﻿using Microsoft.EntityFrameworkCore;
using LR2.Dto.Request.CreateTo;
using LR2.Dto.Request.UpdateTo;
using LR2.Dto.Response;
using LR2.Exceptions;
using LR2.Mappers;
using LR2.Services.Interfaces;
using LR2.Storage;

namespace LR2.Services.Implementations;

public sealed class CreatorService(AppDbContext context) : ICreatorService
{
    public async Task<CreatorResponseTo> GetCreatorById(long id)
    {
        return (await context.Creators.FindAsync(id))?.ToResponse()
               ?? throw new EntityNotFoundException($"Creator with id = {id} doesn't exist.");
    }

    public async Task<IEnumerable<CreatorResponseTo>> GetCreators()
    {
        return CreatorMapper.Map(await context.Creators.ToListAsync());
    }

    public async Task<CreatorResponseTo> CreateCreator(CreateCreatorRequestTo createCreatorRequestTo)
    {
        var creator = CreatorMapper.Map(createCreatorRequestTo);
        await context.Creators.AddAsync(creator);
        await context.SaveChangesAsync();
        return CreatorMapper.Map(creator);
    }

    public async Task DeleteCreator(long id)
    {
        var creator = await context.Creators.FindAsync(id);
        if (creator == null) throw new EntityNotFoundException($"Creator with id = {id} doesn't exist.");

        context.Creators.Remove(creator);
        await context.SaveChangesAsync();
    }

    public async Task<CreatorResponseTo> UpdateCreator(UpdateCreatorRequestTo modifiedCreator)
    {
        var creator = await context.Creators.FindAsync(modifiedCreator.Id);
        if (creator == null)
            throw new EntityNotFoundException($"Creator with id = {modifiedCreator.Id} doesn't exist.");

        context.Entry(creator).State = EntityState.Modified;

        creator.Id = modifiedCreator.Id;
        creator.FirstName = modifiedCreator.FirstName;
        creator.LastName = modifiedCreator.LastName;
        creator.Login = modifiedCreator.Login;
        creator.Password = modifiedCreator.Password;

        await context.SaveChangesAsync();
        return CreatorMapper.Map(creator);
    }
}