using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Techan.Models.LoginRegister;
using Techan.ViewModels.Account;

namespace Techan.Controllers
{
	public class AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager) : Controller
	{
		public async Task<IActionResult> Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM vm)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			AppUser user = new AppUser
			{
				Email = vm.Email,
				UserName = vm.UserName,
				Fullname = vm.UserName,
			};
			var result = await _userManager.CreateAsync(user, vm.Password);
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
				return View();
			}
			return RedirectToAction(nameof(Login));
		}
		public async Task<IActionResult> Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginVM vm)
		{
			if (!ModelState.IsValid)
				return View();
			AppUser? user = null;
			if (vm.UsernameOrEmail.Contains('@'))
				user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
			else
				user = await _userManager.FindByNameAsync(vm.UsernameOrEmail);
			if (user == null)
			{
				ModelState.AddModelError("", "Username or password is not true");
				return View();
			}
			//var passResult=await _userManager.CheckPasswordAsync(user, vm.Password);
			//if (!passResult)
			//{
			//	ModelState.AddModelError("", "Username or password is not true");
			//	return View();
			//}
			//await _signInManager.SignInAsync(user, vm.RememberMe);
			var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);
			if (!result.Succeeded)
			{
				if (result.IsLockedOut)
				{
					ModelState.AddModelError("", "You reached max attemp count. Wait untill" + user.LockoutEnd);
				}
				else if (result.IsNotAllowed)
				{
					ModelState.AddModelError("", "You caanot sign in. Contact with admin please");
				}
				else
				{
					ModelState.AddModelError("", "Username or password is incorrect");
				}
				return View();
			}
			return RedirectToAction("Index","Home");
		}
	}
}
