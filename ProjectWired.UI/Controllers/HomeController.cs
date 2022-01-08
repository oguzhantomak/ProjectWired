using Microsoft.AspNetCore.Mvc;
using ProjectWired.UI.Models;
using System.Diagnostics;
using System.Security.Claims;
using AutoMapper;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectWired.Core.DTOs;
using ProjectWired.Core.DTOs.User;
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
        private readonly UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public HomeController(ILogger<HomeController> logger, IExamService examService, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _examService = examService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        public async Task<IActionResult> ExamList()
        {
            var list = await _examService.GetAllAsync();

            return View(list.OrderByDescending(x => x.CreatedDate));
        }

        [Authorize]
        public async Task<IActionResult> Exam(int id)
        {
            if (id != 0)
            {
                var exam = await _examService.GetWithQuestionsAndChoicesByIdAsync(id);
                return View(exam);
            }

            return View();

        }

        public async Task<IActionResult> DeleteExam(int id)
        {
            var entity = await _examService.GetWithQuestionsAndChoicesByIdAsync(id);
            _examService.Remove(entity);

            return RedirectToAction("ExamList","Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login(string? returnUrl)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            if (userId != 0)
            {
                return RedirectToAction("Index", "Home");
            }
            // Kullanıcı login olduktan sonra hangi urlde kaldıysa oraya geri yönlendirilecek url
            TempData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        if (TempData["ReturnUrl"] != null)
                        {
                            return Redirect(TempData["ReturnUrl"].ToString());
                        }
                        TempData["status"] = "success";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz email veya şifre.");
                }
            }

            return View(model);
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}