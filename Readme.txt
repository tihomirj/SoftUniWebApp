This is the project for the course Software Technologies - SoftUni August 2016.
Developed by TihomirJ

This data driven web application is a blog, where the registered users can post articles under various categories and make comments to each post. The posts can be tagged with various tags for easier search.  

Each category(topic) can have many posts, while a post only falls under one category.
A post can have many comments, while a comment can belong to only one post.
A post can have many tags and each tag can be attached to many posts.
A user can be the author of many posts and comments but each post and comment can only have one author.
Only logged in users can create posts and comments. Anonymous authors are not allowed to post neither articles nor comments to them.
Anonymous users can only browse and read the posts and the comments.
  
Features:
* There are 2 roles - administrators and users - that require log in. Anonymous users are not required to log in and they can only read posts and comments and can sort and search the posts. 

* Administrators can delete and edit all posts and comments. Only the administrators can create, delete and update categories(topics) and tags. The webapp requires authentication.

* The users can create posts and can delete and edit their own posts and comments. They are not allowed to edit and delete posts and comments from other authors. The actions that require authentication are not visible and also "hidden" in the respective controllers.

* The home page shows the 3 most recent and 3 most commented posts.

* The index page of all posts is with paging for easier browsing. 

* The user can sort in ascending and descending order the posts by category, title, data, number of comments and the name (full name or user name) of the author.

* The user can search the posts by title, author and tags. The user can write a list of tags to find all posts that have been tagged with these tags.

* There are notifications that notify the users for created, edited and deleted posts and comments.

* Automatic migrations are enabled. New data is not seeded.

* All texts are in Bulgarian.

* The buttons are in different colors (Red for delete actions, yellow for edit and blue for create).
   
