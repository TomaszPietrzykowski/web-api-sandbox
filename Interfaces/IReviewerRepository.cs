﻿using WebApiSandbox.Models;

namespace WebApiSandbox.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int reviewerId);
        Reviewer GetReviewerByReviewId(int reviewId);
        ICollection<Review> GetReviewersReviews(int reviewerId);
        bool ReviewerExists(int reviewerId);
    }
}