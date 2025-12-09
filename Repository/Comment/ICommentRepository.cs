using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace FinShark.Repository
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
    }
}