warning: in the working copy of '.gitignore', LF will be replaced by CRLF the next time Git touches it
[1mdiff --git a/.gitignore b/.gitignore[m
[1mindex add57be..39c9242 100644[m
[1m--- a/.gitignore[m
[1m+++ b/.gitignore[m
[36m@@ -2,4 +2,5 @@[m [mbin/[m
 obj/[m
 /packages/[m
 riderModule.iml[m
[31m-/_ReSharper.Caches/[m
\ No newline at end of file[m
[32m+[m[32m/_ReSharper.Caches/[m
[32m+[m[32m.idea/[m
[1mdiff --git a/DNPAssignment2026.sln b/DNPAssignment2026.sln[m
[1mindex a55ff74..883c147 100644[m
[1m--- a/DNPAssignment2026.sln[m
[1m+++ b/DNPAssignment2026.sln[m
[36m@@ -1,8 +1,35 @@[m
 ﻿[m
 Microsoft Visual Studio Solution File, Format Version 12.00[m
[32m+[m[32mProject("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "Server", "Server", "{FBD2F07A-70F1-49A4-AC1E-A20FAA62506B}"[m
[32m+[m[32mEndProject[m
[32m+[m[32mProject("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "Entities", "Server\Entities\Entities.csproj", "{976F3F2A-7B04-493A-86A7-6925E2120D41}"[m
[32m+[m[32mEndProject[m
[32m+[m[32mProject("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "RepositoryContracts", "Server\RepositoryContracts\RepositoryContracts.csproj", "{6127657C-B497-44FE-A6B1-2211C735FA90}"[m
[32m+[m[32mEndProject[m
[32m+[m[32mProject("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "InMemoryRepositories", "Server\InMemoryRepositories\InMemoryRepositories.csproj", "{0F925209-4BDE-49B1-867F-E93981715259}"[m
[32m+[m[32mEndProject[m
 Global[m
 	GlobalSection(SolutionConfigurationPlatforms) = preSolution[m
 		Debug|Any CPU = Debug|Any CPU[m
 		Release|Any CPU = Release|Any CPU[m
 	EndGlobalSection[m
[32m+[m	[32mGlobalSection(NestedProjects) = preSolution[m
[32m+[m		[32m{976F3F2A-7B04-493A-86A7-6925E2120D41} = {FBD2F07A-70F1-49A4-AC1E-A20FAA62506B}[m
[32m+[m		[32m{6127657C-B497-44FE-A6B1-2211C735FA90} = {FBD2F07A-70F1-49A4-AC1E-A20FAA62506B}[m
[32m+[m		[32m{0F925209-4BDE-49B1-867F-E93981715259} = {FBD2F07A-70F1-49A4-AC1E-A20FAA62506B}[m
[32m+[m	[32mEndGlobalSection[m
[32m+[m	[32mGlobalSection(ProjectConfigurationPlatforms) = postSolution[m
[32m+[m		[32m{976F3F2A-7B04-493A-86A7-6925E2120D41}.Debug|Any CPU.ActiveCfg = Debug|Any CPU[m
[32m+[m		[32m{976F3F2A-7B04-493A-86A7-6925E2120D41}.Debug|Any CPU.Build.0 = Debug|Any CPU[m
[32m+[m		[32m{976F3F2A-7B04-493A-86A7-6925E2120D41}.Release|Any CPU.ActiveCfg = Release|Any CPU[m
[32m+[m		[32m{976F3F2A-7B04-493A-86A7-6925E2120D41}.Release|Any CPU.Build.0 = Release|Any CPU[m
[32m+[m		[32m{6127657C-B497-44FE-A6B1-2211C735FA90}.Debug|Any CPU.ActiveCfg = Debug|Any CPU[m
[32m+[m		[32m{6127657C-B497-44FE-A6B1-2211C735FA90}.Debug|Any CPU.Build.0 = Debug|Any CPU[m
[32m+[m		[32m{6127657C-B497-44FE-A6B1-2211C735FA90}.Release|Any CPU.ActiveCfg = Release|Any CPU[m
[32m+[m		[32m{6127657C-B497-44FE-A6B1-2211C735FA90}.Release|Any CPU.Build.0 = Release|Any CPU[m
[32m+[m		[32m{0F925209-4BDE-49B1-867F-E93981715259}.Debug|Any CPU.ActiveCfg = Debug|Any CPU[m
[32m+[m		[32m{0F925209-4BDE-49B1-867F-E93981715259}.Debug|Any CPU.Build.0 = Debug|Any CPU[m
[32m+[m		[32m{0F925209-4BDE-49B1-867F-E93981715259}.Release|Any CPU.ActiveCfg = Release|Any CPU[m
[32m+[m		[32m{0F925209-4BDE-49B1-867F-E93981715259}.Release|Any CPU.Build.0 = Release|Any CPU[m
[32m+[m	[32mEndGlobalSection[m
 EndGlobal[m
[1mdiff --git a/Server/Entities/Comment.cs b/Server/Entities/Comment.cs[m
[1mindex 992221a..2ff2b77 100644[m
[1m--- a/Server/Entities/Comment.cs[m
[1m+++ b/Server/Entities/Comment.cs[m
[36m@@ -2,5 +2,9 @@[m
 [m
 public class Comment[m
 {[m
[32m+[m[32m    public int id { get; set; }[m
[32m+[m[32m    public int userId { get; set; }[m
[32m+[m[32m    public int postId { get; set; }[m
[32m+[m[32m    public string content { get; set; }[m
     [m
 }[m
\ No newline at end of file[m
[1mdiff --git a/Server/Entities/Post.cs b/Server/Entities/Post.cs[m
[1mindex 896669a..5eccb69 100644[m
[1m--- a/Server/Entities/Post.cs[m
[1m+++ b/Server/Entities/Post.cs[m
[36m@@ -2,5 +2,10 @@[m
 [m
 public class Post[m
 {[m
[32m+[m[32m    public int id { get; set; }[m
[32m+[m[32m    public string title { get; set; }[m
[32m+[m[32m    public string content { get; set; }[m
[32m+[m[32m    public int userId { get; set; }[m
[32m+[m[41m   [m
     [m
 }[m
\ No newline at end of file[m
[1mdiff --git a/Server/Entities/User.cs b/Server/Entities/User.cs[m
[1mindex 13d2bef..6108615 100644[m
[1m--- a/Server/Entities/User.cs[m
[1m+++ b/Server/Entities/User.cs[m
[36m@@ -2,5 +2,8 @@[m
 [m
 public class User[m
 {[m
[32m+[m[32m    public int id { get; set; }[m
[32m+[m[32m    public string username { get; set; }[m
[32m+[m[32m    public string password { get; set; }[m
     [m
 }[m
\ No newline at end of file[m
[1mdiff --git a/Server/InMemoryRepositories/CommentInMemoryRepository.cs b/Server/InMemoryRepositories/CommentInMemoryRepository.cs[m
[1mindex 8b59659..c7a8d20 100644[m
[1m--- a/Server/InMemoryRepositories/CommentInMemoryRepository.cs[m
[1m+++ b/Server/InMemoryRepositories/CommentInMemoryRepository.cs[m
[36m@@ -1,6 +1,58 @@[m
[31m-﻿namespace InMemoryRepositories;[m
[32m+[m[32m﻿using Entities;[m
[32m+[m[32musing RepositoryContracts;[m
 [m
[31m-public class CommentInMemoryRepository[m
[32m+[m
[32m+[m[32mnamespace InMemoryRepositories;[m
[32m+[m
[32m+[m[32mpublic class CommentInMemoryRepository : ICommentRepository[m
 {[m
[31m-    [m
[32m+[m[32m    private readonly List<Comment> _comments = new();[m
[32m+[m
[32m+[m[32m    public Task<Comment> AddAsync(Comment comment)[m
[32m+[m[32m    {[m
[32m+[m[32m        comment.id = _comments.Any() ? _comments.Max(c => c.id) + 1 : 1;[m
[32m+[m[32m        _comments.Add(comment);[m
[32m+[m[32m        return Task.FromResult(comment);[m
[32m+[m[32m    }[m
[32m+[m
[32m+[m[32m    public Task UpdateAsync(Comment comment)[m
[32m+[m[32m    {[m
[32m+[m[32m        Comment? existingComment = _comments.SingleOrDefault(c => c.id == comment.id);[m
[32m+[m[32m        if (existingComment is null)[m
[32m+[m[32m        {[m
[32m+[m[32m            throw new InvalidOperationException($"Comment with ID '{comment.id}' not found");[m
[32m+[m[32m        }[m
[32m+[m[41m        [m
[32m+[m[32m        _comments.Remove(existingComment);[m
[32m+[m[32m        _comments.Add(comment);[m
[32m+[m[32m        return Task.CompletedTask;[m
[32m+[m[32m    }[m
[32m+[m
[32m+[m[32m    public Task DeleteAsync(int id)[m
[32m+[m[32m    {[m
[32m+[m[32m        Comment? commentToRemove = _comments.SingleOrDefault(c => c.id == id);[m
[32m+[m[32m        if (commentToRemove is null)[m
[32m+[m[32m        {[m
[32m+[m[32m            throw new InvalidOperationException($"Comment with ID '{id}' not found");[m
[32m+[m[32m        }[m
[32m+[m[41m        [m
[32m+[m[32m        _comments.Remove(commentToRemove);[m
[32m+[m[32m        return Task.CompletedTask;[m
[32m+[m[32m    }[m
[32m+[m
[32m+[m[32m    public Task<Comment> GetSingleAsync(int id)[m
[32m+[m[32m    {[m
[32m+[m[32m        Comment? comment = _comments.SingleOrDefault(c => c.id == id);[m
[32m+[m[32m        if (comment is null)[m
[32m+[m[32m        {[m
[32m+[m[32m            throw new InvalidOperationException($"Comment with ID '{id}' not found");[m
[32m+[m[32m        }[m
[32m+[m[41m        [m
[32m+[m[32m        return Task.FromResult(comment);[m
[32m+[m[32m    }[m
[32m+[m
[32m+[m[32m    public IQueryable<Comment> GetMany()[m
[32m+[m[32m    {[m
[32m+[m[32m        return _comments.AsQueryable();[m
[32m+[m[32m    }[m
 }[m
\ No newline at end of file[m
[1mdiff --git a/Server/InMemoryRepositories/PostInMemoryRepository.cs b/Server/InMemoryRepositories/PostInMemoryRepository.cs[m
[1mindex cc31d69..456708e 100644[m
[1m--- a/Server/InMemoryRepositories/PostInMemoryRepository.cs[m
[1m+++ b/Server/InMemoryRepositories/PostInMemoryRepository.cs[m
[36m@@ -1,6 +1,53 @@[m
[31m-﻿namespace InMemoryRepositories;[m
[32m+[m[32m�