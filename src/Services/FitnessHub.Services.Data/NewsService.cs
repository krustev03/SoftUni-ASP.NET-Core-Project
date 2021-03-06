﻿namespace FitnessHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessHub.Data.Common.Repositories;
    using FitnessHub.Data.Models;
    using FitnessHub.Services.Mapping;
    using FitnessHub.Web.ViewModels.News;

    public class NewsService : INewsService
    {
        private readonly IDeletableEntityRepository<News> newsRepository;

        public NewsService(IDeletableEntityRepository<News> newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public async Task AddNewsAsync(NewsInputModel serviceInputModel)
        {
            var news = new News()
            {
                Title = serviceInputModel.Title,
                Content = serviceInputModel.Content,
            };

            await this.newsRepository.AddAsync(news);
            await this.newsRepository.SaveChangesAsync();
        }

        public async Task EditNews(int newsId, NewsInputModel model)
        {
            var news = this.newsRepository.All().Where(x => x.Id == newsId).FirstOrDefault();

            news.Title = model.Title;
            news.Content = model.Content;

            this.newsRepository.Update(news);
            await this.newsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllForPaging<T>(int page, int itemsPerPage = 3)
        {
            var news = this.newsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return news;
        }

        public T GetNewsById<T>(int newsId)
        {
            var news = this.newsRepository.AllAsNoTracking()
                .Where(x => x.Id == newsId)
                .To<T>().FirstOrDefault();
            return news;
        }

        public IEnumerable<T> GetAllWithFilterForPaging<T>(int page, string searchString, int itemsPerPage = 3)
        {
            return this.newsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Where(x => x.Title.ToLower().Contains(searchString.ToLower()))
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
        }

        public int GetCount()
        {
            return this.newsRepository.All().Count();
        }

        public int GetFilteredCount(string searchString)
        {
            return this.newsRepository.All()
                .Where(x => x.Title.ToLower().Contains(searchString.ToLower()))
                .Count();
        }

        public async Task DeleteNewsByIdAsync(int newsId)
        {
            var news = this.newsRepository.All().Where(x => x.Id == newsId).FirstOrDefault();
            this.newsRepository.Delete(news);
            await this.newsRepository.SaveChangesAsync();
        }
    }
}
