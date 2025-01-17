﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    class PostCategoryMapDAO
    {
        private static PostCategoryMapDAO instance = null;
        private static readonly object instanceLock = new object();

        public static PostCategoryMapDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PostCategoryMapDAO();
                    }

                    return instance;
                }
            }
        }
        //--------Checked
        public IEnumerable<Post> FilterPostByCategory(int categoryId, int pageIndex)
        {
           
            var posts = new List<Post>();
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                SqlConnection con = (SqlConnection)context.Database.GetDbConnection();
                string SQL =
                    "SELECT * FROM Post WHERE PostId IN (SELECT PostId FROM PostCategoryMap WHERE CategoryId = @CategoryId)";
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int postID = reader.GetInt32(0);
                        string title = reader.GetString(1);
                        string desc = reader.GetString(2);
                        string fileURL = reader.GetString(3);
                        string uploaderUsername = reader.GetString(4);
                        DateTime date = reader.GetDateTime(5);
                        Post post = new Post
                        {
                            PostId = postID,
                            PostTitle = title,
                            PostDescription = desc,
                            FileUrl = fileURL,
                            UploadDate = date,
                            UploaderUsername = uploaderUsername
                        };
                        posts.Add(post);
                    }

                    reader.NextResult();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return posts;
        }
        //-----------Checked
        public void AddPostMap(PostCategoryMap map)
        {
            try
            {
                PostCategoryMap _map= GetPostMap(map.PostId, map.CategoryId);
                if (_map == null)
                {
                    using var context = new PRN211_OnlyFunds_CopyContext();
                    context.PostCategoryMaps.Add(map);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //-----------Checked
        public PostCategoryMap GetPostMap(int postId, int categoryId)
        {
            PostCategoryMap map = null;
            try
            {
                using var context = new PRN211_OnlyFunds_CopyContext();
                map = context.PostCategoryMaps.SingleOrDefault(m => m.CategoryId == categoryId && m.PostId == postId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return map;
        }

    }
}
