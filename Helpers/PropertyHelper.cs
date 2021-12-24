using ProjectTracker_API.Models.Abstracts;
using System;

namespace ProjectTracker_API.Helpers
{
    public static class PropertyHelper<TEntity> where TEntity : class, IEntity, new()
    {
        public static TEntity FillPropForSystem(TEntity entity)
        {
            entity.Status = 1;
            entity.CreateUserID = 0;
            entity.UpdateUserID = 0;
            entity.CreateDate = DateTime.Now;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }

        public static TEntity FillPropForUser(TEntity entity, bool isCreate, int createdUserId, int? updateUserId = null, int status = 1)
        {
            entity.Status = status;
            if (isCreate) entity.CreateUserID = createdUserId;
            entity.UpdateUserID = updateUserId is null ? createdUserId : updateUserId.Value;
            if (isCreate) entity.CreateDate = DateTime.Now;
            entity.UpdateDate = DateTime.Now;
            return entity;
        }
        public static TEntity CompareShareds(TEntity dbEntity, TEntity dtoEntity)
        {
            dtoEntity.Id = dbEntity.Id;
            dtoEntity.CreateUserID = dbEntity.CreateUserID;
            dtoEntity.UpdateUserID = dbEntity.UpdateUserID;
            dtoEntity.CreateDate = dbEntity.CreateDate;
            dtoEntity.UpdateDate = dbEntity.UpdateDate;
            dtoEntity.Status = dbEntity.Status;
            return dtoEntity;
        }

    }
}