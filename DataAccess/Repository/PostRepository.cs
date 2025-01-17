﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class PostRepository : IPostRepository
    {
        public IEnumerable<Post> GetPostByUser(User user, int pageIndex) =>
            PostDAO.Instance.GetPostsByUser(user, pageIndex);

        public int CountPostByUser(User user) => PostDAO.Instance.CountPostsByUser(user);
        public void InsertPost(Post post) => PostDAO.Instance.InsertPost(post);
        public void DeletePost(int postId) => PostDAO.Instance.DeletePost(postId);
        public Post GetPostById(int postId) => PostDAO.Instance.GetPostByID(postId);

        public IEnumerable<Post> SearchPostsByTitle(string title) => PostDAO.Instance.SearchPostByTitle(title);
    }
}
