using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json;
using PostBin.Models;
using PostBin.Repository;
using PostBin.Shared;

namespace PostBin.Services
{
    public interface IPostService
    {
        public Task<IEnumerable<Post>> GetAllAsync();
        public Task<Post> GetByIdAsync(int id);
        public Task<int> CreateAsync(Post post);
        public Task<Post> UpdateAsync(int id, Post post);
        public Task DeleteAsync(int id);
    }
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            var postsTable = await _postRepository.GetAsync();
            var posts = from row in postsTable.AsEnumerable()
                select new Post {
                    Id = row.Field<int>(Post.Columns.Id),
                    UserId = row.Field<int>(Post.Columns.UserId),
                    Title = row.Field<string>(Post.Columns.Title),
                    Body = row.Field<string>(Post.Columns.Body)
                };
            return posts;
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _postRepository.GetAsync(id);
        }

        public async Task<int> CreateAsync(Post post)
        {
            
            await _postRepository.CreateAsync(post);
            var postsTable = await _postRepository.GetAsync();
            int id = (from row in postsTable.AsEnumerable()
                select row.Field<int>("Id")).Max();
            return id;
        }

        public async Task<Post> UpdateAsync(int id, Post post)
        {
            await _postRepository.UpdateAsync(id, post);
            return await _postRepository.GetAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _postRepository.DeleteAsync(id);
        }
    }
}