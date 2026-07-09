# Mini CMS / Blog Platform

A simple Content Management System (CMS) built using ASP.NET Core MVC, Entity Framework Core, and SQL Server. The application provides a public blog where visitors can read published posts and an admin panel for managing blog content, categories, tags, and comments.


# Technologies Used

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Bootstrap 5
- Razor Views
- Raw SQL (FromSqlRaw)
- Repository Pattern
- Service Layer Architecture

---

# Features

## Authentication

- ASP.NET Core Identity
- Secure Login
- Role-based Authorization
- Admin Role

---

## Dashboard

Displays:

- Total Posts
- Published Posts
- Draft Posts
- Categories
- Tags
- Pending Comments
- Most Commented Posts (Raw SQL Report)

---

## Category Management

- Create Category
- Edit Category
- Delete Category
- List Categories

---

## Tag Management

- Create Tag
- Edit Tag
- Delete Tag
- List Tags

---

## Post Management

- Create Post
- Edit Post
- Soft Delete Post
- Restore Deleted Posts
- Upload Featured Image
- Category Selection
- Multiple Tag Selection
- Publish / Draft Status
- SEO Friendly Slug Generation

---

## Public Blog

- List Published Posts
- Search Posts
- Filter by Category
- Filter by Tag
- Sort by:
  - Newest First
  - Oldest First
  - Title A-Z
  - Title Z-A
- Pagination
- SEO Friendly URLs

Example:

## Comments

Visitors can:

- View Approved Comments
- Submit New Comments

Admin can:

- Approve Comments
- Reject/Delete Comments

New comments remain **Pending** until approved.

---

## Soft Delete

Posts are **not physically deleted**.

Instead,

is set.

Soft deleted posts:

- Do not appear in Admin Post List
- Do not appear in Public Blog
- Can be restored from Deleted Posts page

Implementation:


## Image Upload

Uploaded images are stored in

```
wwwroot/uploads/posts
```

Only image path is stored in the database.


---

# Raw SQL

The project contains a Raw SQL report using FromSqlRaw().

Report:

**Most Commented Posts**

Example SQL:

```sql
SELECT
    P.PostId,
    P.Title,
    COUNT(C.CommentId) AS TotalComments
FROM Posts P
LEFT JOIN Comments C
    ON P.PostId = C.PostId
    AND C.IsApproved = 1
    AND C.IsDeleted = 0
WHERE
    P.IsDeleted = 0
    AND P.IsPublished = 1
GROUP BY
    P.PostId,
    P.Title
ORDER BY
    TotalComments DESC;
```

Purpose:

Displays the posts having the highest number of approved comments.

---

# Database

SQL Server

Tables:

- Posts
- Categories
- Tags
- PostTags
- Comments
- AspNetUsers
- AspNetRoles

---

# Setup Instructions

## 1 Clone Repository

```bash
git clone <repository-url>
```

---

## 2 Update Connection String

Update

```
appsettings.json
```

## 3 Apply Migrations



## 4 Run Application


# Default Admin Login

Email

```
admin@gmail.com
```

Password

```
Admin@123
```

---

# Design Pattern

- Repository Pattern
- Service Layer Pattern
- Dependency Injection

---

# Assumptions

- Only Admin users can manage content.
- Visitors can submit comments.
- Comments require approval before becoming visible.
- Deleted posts are recoverable.
- Slugs are generated automatically.







Mini CMS / Blog Platform

Final Assessment Assignment
