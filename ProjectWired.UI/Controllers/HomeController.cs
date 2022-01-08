using Microsoft.AspNetCore.Mvc;
using ProjectWired.UI.Models;
using System.Diagnostics;
using AutoMapper;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectWired.Core.DTOs;
using ProjectWired.Core.Models;
using ProjectWired.Core.Services;
using Page = ProjectWired.UI.Models.Page;

namespace ProjectWired.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExamService _examService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IExamService examService, IMapper mapper)
        {
            _logger = logger;
            _examService = examService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www.wired.com/most-recent/");

            var headers = doc.DocumentNode.SelectNodes("//h2[@class='archive-item-component__title']").Take(5);

            var details = doc.DocumentNode.SelectNodes("//p[@class='archive-item-component__desc']").Take(5);

            var list = new List<Page>();
            for (int i = 0; i < 5; i++)
            {
                var page = new Page()
                {
                    Header = headers.ToList()[i].InnerText,
                    Detail = details.ToList()[i].InnerText
                };
                list.Add(page);
            }

            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExam([FromBody] CreateExamDto model)
        {
            var entity = new Exam()
            {
                ExamHeader = model.ExamHeader,
                ExamDetail = model.ExamDetail,
                Questions = model.Questions
            };

            var createdEntity = await _examService.AddAsync(entity);
            foreach (var entityQuestion in createdEntity.Questions)
            {
                var choiceIndex = Convert.ToInt32(entityQuestion.CorrectChoiceKey);
                var list = entityQuestion.Choices.ToList();
                entityQuestion.CorrectAnswerId = list[choiceIndex].Id;
            }
            _examService.Update(entity);

            return Ok();
        }

        public async Task<IActionResult> ExamList()
        {
            var list = await _examService.GetAllAsync();

            return View(list.OrderByDescending(x => x.CreatedDate));
        }

        public async Task<IActionResult> Exam(int id)
        {
            if (id != 0)
            {
                var exam = await _examService.GetWithQuestionsAndChoicesByIdAsync(id);
                return View(exam);
            }

            return View();

        }


        //[HttpPost]
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}